using System;
using System.IO;
using UnityEngine;

namespace WreckAPI
{
    /// <summary>
    /// An object used to read data from the event body
    /// </summary>
    public class GameEventReader : BinaryReader
    {
        ulong sender;
        int length;

        /// <summary>
        /// Steam ID of the sender.
        /// </summary>
        public ulong Sender => sender;
        /// <summary>
        /// The total length of the data in bytes.
        /// </summary>
        public int Length => length;

        /// <summary>
        /// Read Vector3 from the current stream position.
        /// </summary>
        /// <returns>The parsed Vector3.</returns>
        public Vector3 ReadVector3()
        {
            var x = ReadSingle();
            var y = ReadSingle();
            var z = ReadSingle();
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Calculates the unread bytes in the stream.
        /// </summary>
        /// <returns>Total bytes that haven't been read yet.</returns>
        public int UnreadLength()
        {
            return (int)(length - BaseStream.Position);
        }

        internal GameEventReader(ulong sender, byte[] packet) : base(new MemoryStream(packet))
        {
            this.sender = sender;
            length = packet.Length;
        }
    }
}
