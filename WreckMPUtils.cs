using System;
using UnityEngine;

namespace WreckAPI
{
    /// <summary>
    /// Useful helper methods
    /// </summary>
    public static class WreckMPUtils
    {
        /// <summary>
        /// Create a synced sleep trigger. Does NOT work in singleplayer!
        /// </summary>
        /// <param name="makeCollider">If set to true, creates a trigger sphere collider of radius 0.4m on the sleepTriggerObj.</param>
        /// <param name="gameObject">The root game object of the pickable bed, if it's able to be picked up, otherwise set to null.</param>
        /// <param name="sleepTriggerObj">The pivot on which the sleep trigger will be created.</param>
        /// <param name="pivotPos">Sleeping pose pivot position.</param>
        /// <param name="pivotRot">Sleeping pose pivot euler angles.</param>
        public static void CreateSleepTrigger(bool makeCollider, GameObject gameObject, GameObject sleepTriggerObj, Vector3 pivotPos, Vector3 pivotRot)
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return;
            mp_CreateSleepTrigger(makeCollider, gameObject, sleepTriggerObj, pivotPos, pivotRot);
        }

        /// <summary>
        /// Create synced tow hook trigger on the specified pivot. Does NOT work in singleplayer!
        /// </summary>
        /// <param name="pivot">The pivot to create tow hook on. The pivot's parent has to be the root vehicle transform, or other unique named object.</param>
        public static void CreateTowHookTrigger(GameObject pivot)
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return;
            mp_CreateTowHookTrigger(pivot);
        }

        static void mp_CreateSleepTrigger(bool makeCollider, GameObject gameObject, GameObject sleepTriggerObj, Vector3 pivotPos, Vector3 pivotRot)
        {
            WreckMP.NetSleepingManager.CreateSleepTrigger(makeCollider, gameObject, sleepTriggerObj, pivotPos, pivotRot);
        }

        static void mp_CreateTowHookTrigger(GameObject pivot)
        {
            WreckMP.NetVehicleManager.CreateTowHookTrigger(pivot);
        }
    }
}
