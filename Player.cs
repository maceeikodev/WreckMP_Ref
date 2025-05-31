using System;
using UnityEngine;

namespace WreckMP_Ref
{
    public class Player
    {
        ulong _steamID;
        /// <summary>
        /// Steam ID of the player.
        /// </summary>
        public ulong SteamID => _steamID;

        string pname;
        /// <summary>
        /// The Steam name of the player.
        /// </summary>
        public string PlayerName => pname;

        GameObject player;
        /// <summary>
        /// The root of the player model.
        /// </summary>
        public GameObject PlayerModel => player;

        /// <summary>
        /// The player's head position.
        /// </summary>
        public Vector3 HeadPos
        {
            get
            {
                var player = mp_player as WreckMP.Player;
                return player.HeadPos;
            }
        }
        /// <summary>
        /// Set overriding inverse kinematics target pivot - useful when you want to make the player hold custom objects
        /// </summary>
        public Transform LeftHandOverrideIK
        {
            get
            {
                var player = mp_player as WreckMP.Player;
                return player.LeftHandOverrideIK;
            }
            set 
            {
                var player = mp_player as WreckMP.Player;
                player.LeftHandOverrideIK = value;
            }
        }
        /// <summary>
        /// Set overriding inverse kinematics target pivot - useful when you want to make the player hold custom objects
        /// </summary>
        public Transform RightHandOverrideIK
        {
            get
            {
                var player = mp_player as WreckMP.Player;
                return player.RightHandOverrideIK;
            }
            set
            {
                var player = mp_player as WreckMP.Player;
                player.RightHandOverrideIK = value;
            }
        }

        object mp_player;

        internal Player() { }
        internal void ApplyRef(object mp_player)
        {
            this.mp_player = mp_player;
            var player = mp_player as WreckMP.Player;

            _steamID = player.SteamID;
            pname = player.PlayerName;
            this.player = player.player;
        }
    }
}
