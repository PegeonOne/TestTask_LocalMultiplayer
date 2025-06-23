using Game.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.App.Player
{
    public sealed class PlayerHP_BarController : BaseBehavior
    {
        [SerializeField] private Player_Hp_BarView View;

        public void ChangeHPBarState(int value)
        {
            int converted = value / 20;
            View.ChangeHpBarState(converted);
        }
    }
}
