using MSCLoader;
using System;
using UnityEngine;

namespace WreckAPI
{
    /// <summary>
    /// Base class for synced properties. This class is abstract. For syncing custom types inherit this class to implement your own synced property.
    /// </summary>
    public abstract class SyncedProperty<T>
    {
        string _uniqueName;
        /// <summary>
        /// The unique name of the game event used to send updates.
        /// </summary>
        public string UniqueName => _uniqueName;
        /// <summary>
        /// Whether to send the event primarily over TCP. Default is false. Set to true if you only send the update once in a while and isn't very time sensitive
        /// </summary>
        public bool sendOverTCP = false;

        GameEvent gameEvent;

        protected T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (IsChanged(_value, value))
                {
                    if (currentCooldown > 0f)
                    {
                        hasChangeToSend = true;
                    }
                    else
                    {
                        SendUpdate();
                    }
                }
            }
        }

        /// <summary>
        /// Fired when received an update over the network and the value changes
        /// </summary>
        public event Action ReceivedUpdate;

        bool registeredTimer = false, hasChangeToSend = false;
        float _updateCooldown = 0f, currentCooldown = 0f;
        /// <summary>
        /// Cooldown for sending update packets in seconds. Default is 0
        /// </summary>
        public float UpdateCooldown
        {
            get { return _updateCooldown; }
            set
            {
                if (!registeredTimer)
                {
                    throw new InvalidOperationException("Can't set cooldown because the property is not registered in the timer");
                }
                _updateCooldown = value;
            }
        }

        /// <summary>
        /// Creates an instance
        /// </summary>
        /// <param name="uid">The unique name of the game event used to send updates.</param>
        /// <param name="registerTimer">Set to true if you're going to use UpdateCooldown, otherwise keep on false to improve performance. Only use UpdateCooldown if you change the value often</param>
        protected SyncedProperty(string uid, bool registerTimer)
        {
            _uniqueName = uid;

            if (WreckMPGlobals.IsMultiplayerSession)
            {
                gameEvent = new GameEvent(uid, OnGameEvent);
                if (WreckMPGlobals.IsHost)
                {
                    WreckMPGlobals.OnMemberReady(OnMemberReady);
                }

                if (registerTimer) mp_registerTimer();
            }
        }

        void mp_registerTimer()
        {
            registeredTimer = WreckMP.NetTimer.RegisterTimer(TimerUpdate);
            if (!registeredTimer)
                ModConsole.LogError($"Failed to register synced property '{_uniqueName}' because timer is not initialized");
        }

        void OnGameEvent(GameEventReader p)
        {
            Read(p);
            ReceivedUpdate?.Invoke();
        }

        void OnMemberReady(ulong target)
        {
            SendUpdate(target, true, true);
        }

        /// <summary>
        /// Send update event
        /// </summary>
        /// <param name="target">Target user. 0 sends to everyone</param>
        /// <param name="safe">If null, sendOverTCP field is used</param>
        /// <param name="initial">True if initial sync. This argument is only passed to the Write method</param>
        public void SendUpdate(ulong target = 0, bool? safe = null, bool initial = false)
        {
            using (var p = gameEvent.Writer())
            {
                Write(p, initial);
                p.Send(target, safe ?? sendOverTCP);
                currentCooldown = _updateCooldown;
            }
        }

        /// <summary>
        /// This function is called when the property Value is set to see whether the change should be sent to others.
        /// </summary>
        /// <param name="oldVal">Old value</param>
        /// <param name="newVal">New value</param>
        /// <returns>If true, the event is sent.</returns>
        protected abstract bool IsChanged(T oldVal, T newVal);

        /// <summary>
        /// Serialize the update data
        /// </summary>
        /// <param name="p"></param>
        protected abstract void Write(GameEventWriter p, bool initial);

        /// <summary>
        /// Deserialize the update data. WARNING!!! When writing to Value, write to _value instead to prevent another event being sent!
        /// </summary>
        /// <param name="p"></param>
        protected abstract void Read(GameEventReader p);

        /// <summary>
        /// DO NOT USE! This method is called internally to calculate cooldown timers.
        /// </summary>
        public void TimerUpdate()
        {
            if (currentCooldown > 0f)
            {
                currentCooldown -= Time.deltaTime;
            }
            else if (hasChangeToSend)
            {
                hasChangeToSend = false;
                SendUpdate();
            }
        }
    }
}
