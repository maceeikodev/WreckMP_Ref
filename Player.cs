using System;

namespace WreckMP_Ref
{
    public class Player
    {
        object mp_player;

        internal Player() { }
        internal void ApplyRef(object mp_player)
        {
            this.mp_player = mp_player;
        }
    }
}
