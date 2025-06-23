using Game.App.Enemy;
using Game.App.Network;
using Game.Core;
using Game.UI;
using System;
using UnityEngine;

namespace Game.App
{
    public sealed class AppController : BaseBehavior
    {
        [SerializeField] private UIController _ui;
        [SerializeField] private NetworkController _network;
        [SerializeField] private EnemiesManager _enemySpawnController;
        [SerializeField] private AppConfig _config;

        protected override void Initialise()
        {
            _ui.Init(this);
            _network.Init(_config);
            _enemySpawnController.Init(_config);
        }

        public void StartHost()
        {
            _network.StartHost();
        }

        public void StartClient()
        {
            _network.StartClient();
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}
