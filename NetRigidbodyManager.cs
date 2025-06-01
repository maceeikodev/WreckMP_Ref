using System;
using UnityEngine;

namespace WreckMP_Ref
{
    public static class NetRigidbodyManager
    {
        /// <summary>
        /// Register a rigidbody. Do NOT register rigidbodies created in PreLoad / OnLoad / PostLoad! They are registered automatically.
        /// </summary>
        /// <param name="rb">The rigidbody to register.</param>
        /// <param name="hash">Unique hash to identify the rigibody.</param>
        /// <returns>The newly created and registered OwnedRigidbody object.</returns>
        public static OwnedRigidbody RegisterRigidbody(this Rigidbody rb, int hash)
        {
            
        }

        /// <summary>
        /// Get OwnedRigidbody by its hash.
        /// </summary>
        /// <param name="hash"> The hash of the requested rigidbody.</param>
        /// <returns>The requested OwnedRigidbody, if found, otherwise null.</returns>
        public static OwnedRigidbody GetOwnedRigidbody(int hash)
        {

        }

        /// <summary>
        /// Get a hash of a rigidbody.
        /// </summary>
        /// <param name="rb">The rigidbody to look for.</param>
        /// <returns>The hash of the requested rigidbody, if found, otherwise 0.</returns>
        public static int GetRigidbodyHash(this Rigidbody rb)
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return 0;
            return mp_GetRigidbodyHash(rb);
        }

        /// <summary>
        /// Set a rigidbody's owner to local player.
        /// </summary>
        /// <param name="rigidbody">The rigidbody to change owner of.</param>
        public static void RequestOwnership(this Rigidbody rigidbody)
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return;
            mp_RequestOwnership(rigidbody);
        }

        /// <summary>
        /// Set a rigidbody's owner to local player.
        /// </summary>
        /// <param name="rigidbody">The rigidbody to change owner of.</param>
        public static void RequestOwnership(this OwnedRigidbody rigidbody)
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return;
            finish
        }

        static OwnedRigidbody mp_RegisterRigidbody(Rigidbody rb, int hash)
        {

        }

        static OwnedRigidbody mp_GetOwnedRigidbody(int hash)
        {

        }

        static int mp_GetRigidbodyHash(Rigidbody rb)
        {
            return WreckMP.NetRigidbodyManager.GetRigidbodyHash(rb);
        }

        static void mp_RequestOwnership(Rigidbody rigidbody)
        {
            WreckMP.NetRigidbodyManager.RequestOwnership(rigidbody);
        }
    }
}
