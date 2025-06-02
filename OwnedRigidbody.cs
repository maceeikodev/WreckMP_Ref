using System;
using UnityEngine;

namespace WreckAPI
{
    /// <summary>
    /// Helper class for a rigidbody with an owner.
    /// </summary>
    public class OwnedRigidbody
    {
        Rigidbody singleplayer_rigidbody;
        object mp_ref;

        /// <summary>
        /// The transform of the rigidbody.
        /// </summary>
        public Transform OwnerTransform
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession)
                {
                    return singleplayer_rigidbody == null ? null : singleplayer_rigidbody.transform;
                }
                return mp_ownertf();
            }
        }

        /// <summary>
        /// The Steam ID of the rigidbody owner. The owner is overriding the rigidbody's properties for other players when they update.
        /// </summary>
        public ulong OwnerID
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return 0;
                return mp_owner();
            }
        }

        /// <summary>
        /// The rigidbody object. Can be null, as this may be sometimes destroyed and later recreated again. OwnerTransform keeps reference to the object even if the rigidbody is destroyed.
        /// </summary>
        public Rigidbody Rigidbody
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return singleplayer_rigidbody;
                return mp_rb();
            }
        }

        internal OwnedRigidbody(object mp_orb)
        {
            mp_ref = mp_orb;
        }

        internal OwnedRigidbody(Rigidbody sp_rigidbody)
        {
            singleplayer_rigidbody = sp_rigidbody;
        }

        Transform mp_ownertf()
        {
            var mp_orb = mp_ref as WreckMP.OwnedRigidbody;
            return mp_orb.transform;
        }

        ulong mp_owner()
        {
            var mp_orb = mp_ref as WreckMP.OwnedRigidbody;
            return mp_orb.OwnerID;
        }

        Rigidbody mp_rb()
        {
            var mp_orb = mp_ref as WreckMP.OwnedRigidbody;
            return mp_orb.Rigidbody;
        }
    }
}
