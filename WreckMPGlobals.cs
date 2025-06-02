using System;
using System.Collections.Generic;
using UnityEngine;

namespace WreckAPI
{
    /// <summary>
    /// Set of global properties and events.
    /// </summary>
    public static class WreckMPGlobals
    {
        /// <summary>
        /// Returns true if WreckMP is loaded
        /// </summary>
        public static bool IsMultiplayerSession => Environment.GetEnvironmentVariable("WreckMP-Present") != null;
        /// <summary>
        /// Specifies whether the local player is the host of the session. In singleplayer returns true
        /// </summary>
        public static bool IsHost
        {
            get
            {
                if (!IsMultiplayerSession) return true;
                return mp_IsHost();
            }
        }
        /// <summary>
        /// Get the Steam ID of the session host. In singleplayer returns 0
        /// </summary>
        public static ulong HostID
        {
            get
            {
                if (!IsMultiplayerSession) return 0;
                return mp_HostID();
            }
        }
        /// <summary>
        /// Get the Steam ID of the local player. In singleplayer returns 0
        /// </summary>
        public static ulong UserID
        {
            get
            {
                if (!IsMultiplayerSession) return 0;
                return mp_UserID();
            }
        }
        /// <summary>
        /// Get all players by their Steam ID. In singleplayer returns null
        /// </summary>
        public static Dictionary<ulong, Player> Players
        {
            get
            {
                if (!IsMultiplayerSession) return null;
                return mp_Players();
            }
        }

        /// <summary>
        /// Invoked when a user establishes connection.
        /// </summary>
        /// <param name="callback"></param>
        public static void OnMemberJoin(Action<ulong> callback)
        {
            if (!IsMultiplayerSession) return;
            mp_onMemberJoin(callback);
        }

        static void mp_onMemberJoin(Action<ulong> callback)
        {
            WreckMP.WreckMPGlobals.OnMemberJoin += callback;
        }

        /// <summary>
        /// Invoked when a user finishes loading game and WreckMP and is ready to receive events.
        /// </summary>
        /// <param name="callback"></param>
        public static void OnMemberReady(Action<ulong> callback)
        {
            if (!IsMultiplayerSession) return;
            mp_onMemberReady(callback);
        }

        static void mp_onMemberReady(Action<ulong> callback)
        {
            WreckMP.WreckMPGlobals.OnMemberReady.Add(callback);
        }

        /// <summary>
        /// Invoked when a user leaves the session.
        /// </summary>
        /// <param name="callback"></param>
        public static void OnMemberExit(Action<ulong> callback)
        {
            if (!IsMultiplayerSession) return;
            mp_onMemberExit(callback);
        }

        static void mp_onMemberExit(Action<ulong> callback)
        {
            WreckMP.WreckMPGlobals.OnMemberExit += callback;
        }

        /// <summary>
        /// Invoked when a player model is created.
        /// </summary>
        /// <param name="callback"></param>
        public static void OnPlayerModelCreated(Action<GameObject> callback)
        {
            if (!IsMultiplayerSession) return;
            mp_onPlayerModelCreated(callback);
        }

        static void mp_onPlayerModelCreated(Action<GameObject> callback)
        {
            WreckMP.WreckMPGlobals.OnPlayerModelCreated += callback;
        }

        static bool mp_IsHost()
        {
            return WreckMP.WreckMPGlobals.IsHost;
        }

        static ulong mp_HostID()
        {
            return WreckMP.WreckMPGlobals.HostID;
        }

        static ulong mp_UserID()
        {
            return WreckMP.WreckMPGlobals.UserID;
        }

        static Dictionary<ulong, Player> mp_Players()
        {
            var mp_dict = WreckMP.WreckMPGlobals.Players;
            var dict = new Dictionary<ulong, Player>();
            foreach (var mp_p in mp_dict)
            {
                var p = new Player();
                p.ApplyRef(mp_p.Value);
                dict.Add(mp_p.Key, p);
            }
            return dict;
        }
    }
}
