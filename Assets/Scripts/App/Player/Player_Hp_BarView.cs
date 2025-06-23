using Game.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.App.Player
{
    public sealed class Player_Hp_BarView : BaseBehavior
    {
        [SerializeField] private List<Color> _hpColorBarState;
        [SerializeField] private List<GameObject> _hpBarIndicators;

        public void ChangeHpBarState(int value)
        {
            _hpBarIndicators.ForEach(i => i.gameObject.SetActive(false));
            for (int i = 0; i < value; i++)
            {
                _hpBarIndicators[i].SetActive(true);
                Renderer rndr = _hpBarIndicators[i].GetComponent<Renderer>();
                rndr.material.color = _hpColorBarState[value - 1];
            }          
        }
    }
}
