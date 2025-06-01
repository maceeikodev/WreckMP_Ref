using System;
using UnityEngine;

namespace WreckMP_Ref
{
    /// <summary>
    /// Class used to store a vehicle object whose physics and controls are synced.
    /// </summary>
    public class NetVehicle
    {
        /// <summary>
        /// Root transform of the vehicle.
        /// </summary>
        public Transform transform;

        /// <summary>
        /// Class containing all pivot points for driver animation.
        /// </summary>
        public NetVehicleDriverPivots DriverPivots => driverPivots;

        /// <summary>
        /// Unique hash of the vehicle used for identification.
        /// </summary>
        public int Hash => hash;

        /// <summary>
        /// Steam ID of the vehicle's rigidbody's owner. In singleplayer returns 0.
        /// </summary>
        public ulong Owner
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return 0;
                return mp_owner();
            }
        }

        /// <summary>
        /// Steam ID of the user who's overriding the controls. The controls aren't overriden if Driver is equal to 0 or WreckMPGlobals.UserID. In singleplayer returns 0.
        /// </summary>
        public ulong Driver
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return 0;
                return mp_driver();
            }
        }

        /// <summary>
        /// Whether the driver seat is taken.
        /// </summary>
        public bool DriverSeatTaken => Driver != 0;

        int hash;
        NetVehicleDriverPivots driverPivots;
        object mp_ref;

        /// <summary>
        /// Creates an instance of NetVehicle class.
        /// </summary>
        /// <param name="transform">The root transform of the vehicle.</param>
        public NetVehicle(Transform transform)
        {
            this.transform = transform;
            if (WreckMPGlobals.IsMultiplayerSession)
            {
                mp_ctor();
            }
            else
            {
                driverPivots = new NetVehicleDriverPivots(null);
            }
        }

        /// <summary>
        /// Adds a passenger seat to the vehicle. Does NOT work in singleplayer!
        /// </summary>
        /// <param name="triggerOffset">Passenger seat offset from the root transform.</param>
        /// <param name="headPivotOffset">Head offset of the passenger mode.</param>
        /// <returns>Root transform of the newly created passenger seat.</returns>
        public Transform AddPassengerSeat(Vector3 triggerOffset, Vector3 headPivotOffset)
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return null;
            return mp_AddPassengerSeat(triggerOffset, headPivotOffset);
        }

        /// <summary>
        /// Make car controls update from local player for everyone else and apply driver animation
        /// </summary>
        public void SendEnterDrivingMode()
        {
            if (!WreckMPGlobals.IsMultiplayerSession || DriverSeatTaken) return;
            mp_enterDriver();
        }

        /// <summary>
        /// Stop controls from being updated from local player.
        /// </summary>
        public void SendExitDrivingMode()
        {
            if (!WreckMPGlobals.IsMultiplayerSession) return;
            mp_exitDriver();
        }

        void mp_ctor()
        {
            var mp_vehicle = new WreckMP.NetVehicle(transform);
            
            mp_ref = mp_vehicle;
            hash = mp_vehicle.Hash;
            mp_vehicle.driverPivots = new WreckMP.NetVehicleDriverPivots();
            driverPivots = new NetVehicleDriverPivots(mp_vehicle.driverPivots);

            WreckMP.NetVehicleManager.RegisterNetVehicle(mp_vehicle);
        }

        ulong mp_owner()
        {
            var mp_vehicle = mp_ref as WreckMP.NetVehicle;
            return mp_vehicle.Owner;
        }

        ulong mp_driver()
        {
            var mp_vehicle = mp_ref as WreckMP.NetVehicle;
            return mp_vehicle.Driver;
        }

        Transform mp_AddPassengerSeat(Vector3 triggerOffset, Vector3 headPivotOffset)
        {
            var mp_vehicle = mp_ref as WreckMP.NetVehicle;
            return mp_vehicle.AddPassengerSeat(triggerOffset, headPivotOffset);
        }

        void mp_enterDriver()
        {
            var mp_vehicle = mp_ref as WreckMP.NetVehicle;
            mp_vehicle.SendEnterDrivingMode();
        }

        void mp_exitDriver()
        {
            var mp_vehicle = mp_ref as WreckMP.NetVehicle;
            mp_vehicle.SendExitDrivingMode();
        }
    }
}
