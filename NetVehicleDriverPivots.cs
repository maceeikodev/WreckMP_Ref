using System;
using UnityEngine;

namespace WreckAPI
{
    /// <summary>
    /// Class containing all pivot points for driver animation.
    /// </summary>
    public class NetVehicleDriverPivots
    {
        Transform sp_throttlePedal;
        public Transform ThrottlePedal
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return sp_throttlePedal;
                return mp_throttlePedal(true, null);
            }
            set
            {
                sp_throttlePedal = value;
                if (!WreckMPGlobals.IsMultiplayerSession) return;
                mp_throttlePedal(false, value);
            }
        }
        Transform mp_throttlePedal(bool get, Transform value)
        {
            var mp_driverPivots = mp_ref as WreckMP.NetVehicleDriverPivots;
            if (get) return mp_driverPivots.throttlePedal;
            mp_driverPivots.throttlePedal = value;
            return null;
        }

        Transform sp_brakePedal;
        public Transform BrakePedal
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return sp_brakePedal;
                return mp_brakePedal(true, null);
            }
            set
            {
                sp_brakePedal = value;
                if (!WreckMPGlobals.IsMultiplayerSession) return;
                mp_brakePedal(false, value);
            }
        }
        Transform mp_brakePedal(bool get, Transform value)
        {
            var mp_driverPivots = mp_ref as WreckMP.NetVehicleDriverPivots;
            if (get) return mp_driverPivots.brakePedal;
            mp_driverPivots.brakePedal = value;
            return null;
        }

        Transform sp_clutchPedal;
        public Transform ClutchPedal
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return sp_clutchPedal;
                return mp_clutchPedal(true, null);
            }
            set
            {
                sp_clutchPedal = value;
                if (!WreckMPGlobals.IsMultiplayerSession) return;
                mp_clutchPedal(false, value);
            }
        }
        Transform mp_clutchPedal(bool get, Transform value)
        {
            var mp_driverPivots = mp_ref as WreckMP.NetVehicleDriverPivots;
            if (get) return mp_driverPivots.clutchPedal;
            mp_driverPivots.clutchPedal = value;
            return null;
        }

        Transform sp_steeringWheel;
        public Transform SteeringWheel
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return sp_steeringWheel;
                return mp_steeringWheel(true, null);
            }
            set
            {
                sp_steeringWheel = value;
                if (!WreckMPGlobals.IsMultiplayerSession) return;
                mp_steeringWheel(false, value);
            }
        }
        Transform mp_steeringWheel(bool get, Transform value)
        {
            var mp_driverPivots = mp_ref as WreckMP.NetVehicleDriverPivots;
            if (get) return mp_driverPivots.steeringWheel;
            mp_driverPivots.steeringWheel = value;
            return null;
        }

        Transform sp_gearStick;
        public Transform GearStick
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return sp_gearStick;
                return mp_gearStick(true, null);
            }
            set
            {
                sp_gearStick = value;
                if (!WreckMPGlobals.IsMultiplayerSession) return;
                mp_gearStick(false, value);
            }
        }
        Transform mp_gearStick(bool get, Transform value)
        {
            var mp_driverPivots = mp_ref as WreckMP.NetVehicleDriverPivots;
            if (get) return mp_driverPivots.gearStick;
            mp_driverPivots.gearStick = value;
            return null;
        }

        Transform sp_driverParent;
        public Transform DriverParent
        {
            get
            {
                if (!WreckMPGlobals.IsMultiplayerSession) return sp_driverParent;
                return mp_driverParent(true, null);
            }
            set
            {
                sp_driverParent = value;
                if (!WreckMPGlobals.IsMultiplayerSession) return;
                mp_driverParent(false, value);
            }
        }
        Transform mp_driverParent(bool get, Transform value)
        {
            var mp_driverPivots = mp_ref as WreckMP.NetVehicleDriverPivots;
            if (get) return mp_driverPivots.driverParent;
            mp_driverPivots.driverParent = value;
            return null;
        }

        object mp_ref;
        internal NetVehicleDriverPivots(object mp_ref)
        {
            this.mp_ref = mp_ref;
        }
    }
}
