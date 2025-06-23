using Game.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class UIView : BaseBehavior
    {
        [SerializeField] private Button StartHostButton;
        [SerializeField] private Button StartClientButton;
        [SerializeField] private Button CloseGameButton;
        [SerializeField] private GameObject Panel;

        public event Action OnStartHostPressed;
        public event Action OnStartClientPressed;
        public event Action OnCloseGameButtonPressed;

        private bool _panelIsActive;

        protected override void Initialise()
        {
            StartHostButton.onClick.AddListener(StartHost);
            StartClientButton.onClick.AddListener(StartClient);
            CloseGameButton.onClick.AddListener(EndGame);
        }

        public void HidePanel()
        {
            Panel.SetActive(false);
        }

        private void StartHost()
        {
            OnStartHostPressed?.Invoke();
            HidePanel();
        }

        private void StartClient()
        {
            OnStartClientPressed?.Invoke();
            HidePanel();
        }

        private void EndGame()
        {
            OnCloseGameButtonPressed?.Invoke();
        }

        protected override void OnDispose()
        {
            StartHostButton.onClick.RemoveListener(StartHost);
            StartClientButton.onClick.RemoveListener(StartClient);
            CloseGameButton.onClick.RemoveListener(EndGame);
        }
    }
}
