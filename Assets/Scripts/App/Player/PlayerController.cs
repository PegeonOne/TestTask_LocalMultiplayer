using Game.Core;
using UnityEngine;

namespace Game.App.Player
{
    public sealed class PlayerController : BaseController
    {
        [SerializeField] private PlayerMoveController _moveController;
        [SerializeField] private PlayerHP_BarController _barController;

        private int _playerHealth = 100;

        public void DecreaseHealth()
        {
            _playerHealth -= 20;
            _barController.ChangeHPBarState(_playerHealth);
        }
    }
}
