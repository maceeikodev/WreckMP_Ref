using System;
using UnityEngine;

namespace WreckMP_Ref
{
    public enum GameScene
    {
        MainMenu = 0,
        Game = 1,
        Unknown = 2
    }

    /// <summary>
    /// Core class for sending data between players. Can be created in singleplayer session too for ease of programming, but sending won't do anything and the callback will never be called
    /// </summary>
    public class GameEvent
    {
        int hash;
        string name;
        GameScene targetScene;
        Action<GameEventReader> callback;

        internal object mp_ref;

        /// <summary>
        /// The unique hash of the game event computed from the name.
        /// </summary>
        public int Hash => hash;

        /// <summary>
        /// The unique name of the game event assigned in constructor.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The target game scene of the game event assigned in constructor.
        /// </summary>
        public GameScene TargetScene => targetScene;

        /// <summary>
        /// Creates an instance of GameEvent.
        /// </summary>
        /// <param name="name">Unique name of the event that will be used for identification.</param>
        /// <param name="callback">An action to call when the event is received via network.</param>
        /// <param name="targetScene">Target scene to receive the event in. If it's received when different scene is loaded, the callback won't be called.</param>
        public GameEvent(string name, Action<GameEventReader> callback, GameScene targetScene = GameScene.Game)
        {
            this.name = name;
            this.callback = callback;
            this.targetScene = targetScene;
            if (WreckMPGlobals.IsMultiplayerSession) mp_ctor();
        }

        /// <summary>
        /// Get a data writer to send the event with body
        /// </summary>
        /// <returns>An instance of GameEventWriter</returns>
        public GameEventWriter Writer()
        {
            return new GameEventWriter(this);
        }

        /// <summary>
        /// Send the game event with no data.
        /// </summary>
        /// <param name="target">Steam ID of the event recipient. Setting this to 0 will send the event to all connected players.</param>
        /// <param name="safe">True means use TCP (safe but slower), false means use UDP (unsafe but faster, max packet size is 1024 bytes!!)</param>
        public void SendEmpty(ulong target = 0, bool safe = false)
        {
            if (WreckMPGlobals.IsMultiplayerSession) mp_sendEmpty(target, safe);
        }

        /// <summary>
        /// Unregisters the event. It will no longer be received.
        /// </summary>
        public void Unregister()
        {
            if (WreckMPGlobals.IsMultiplayerSession) mp_unregister();
        }

        void mp_ctor()
        {
            var targetScene_int = (int)targetScene;
            var mp_targetScene = (WreckMP.GameScene)targetScene_int;

            var mp_event = new WreckMP.GameEvent(name, mp_callback, mp_targetScene);
            hash = mp_event.Hash;
            mp_ref = mp_event;
        }

        void mp_callback(object reader)
        {
            var mp_reader = reader as WreckMP.GameEventReader;
            var bytes = mp_reader.ReadBytes((int)(mp_reader.Length - mp_reader.BaseStream.Position));
            
            var ref_reader = new GameEventReader(mp_reader.sender, bytes);
            callback?.Invoke(ref_reader);
        }

        void mp_sendEmpty(ulong target, bool safe)
        {
            var mp_event = mp_ref as WreckMP.GameEvent;
            mp_event.SendEmpty(target, safe);
        }

        void mp_unregister()
        {
            var mp_event = mp_ref as WreckMP.GameEvent;
            mp_event.Unregister();
        }
    }
}
