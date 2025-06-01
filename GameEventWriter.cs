using System;
using System.IO;
using UnityEngine;

namespace WreckMP_Ref
{
    public class GameEventWriter : BinaryWriter
    {
        GameEvent gameEvent;
        /// <summary>
        /// The game event who created this writer
        /// </summary>
        public GameEvent OwnerGameEvent => gameEvent;

        public void Write(Vector3 vector3)
        {
            Write(vector3.x);
            Write(vector3.y);
            Write(vector3.z);
        }

        /// <summary>
        /// Send the packet with current data.
        /// </summary>
        /// <param name="target">Steam ID of the event recipient. Setting this to 0 will send the event to all connected players.</param>
        /// <param name="safe">True means use TCP (safe but slower), false means use UDP (unsafe but faster, max packet size is 1024 bytes!!)</param>
        public void Send(ulong target = 0, bool safe = false)
        {
            if (WreckMPGlobals.IsMultiplayerSession) mp_send(target, safe);
        }

        internal GameEventWriter(GameEvent gameEvent) : base(new MemoryStream())
        {
            this.gameEvent = gameEvent;
        }

        void mp_send(ulong target, bool safe)
        {
            var mp_event = gameEvent.mp_ref as WreckMP.GameEvent;
            var ms = OutStream as MemoryStream;
            var bytes = ms.ToArray();

            using (var writer = mp_event.Writer())
            {
                writer.Write(bytes);
                writer.Send(target, safe);
            }
        }
    }
}
