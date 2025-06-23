using Game.App;
using Game.Core;
using System;

namespace Game.UI
{
    public sealed class UIController : BaseController
    {
        private UIView View;
        private AppController _app;

        public override void Init(AppController app)
        {
            _app = app;
            View = GetComponent<UIView>();
            View.OnStartHostPressed += StartHost;
            View.OnStartClientPressed += StartClient;
            View.OnCloseGameButtonPressed += CloseGame;
        }

        private void StartHost()
        {
            _app.StartHost();
        }

        private void StartClient()
        {
            _app.StartClient();
        }

        private void CloseGame()
        {
            _app.CloseGame();
        }

        protected override void OnDispose()
        {
            _app = null;
            View = null;

            View.OnStartHostPressed -= StartHost;
            View.OnStartClientPressed -= StartClient;
            View.OnCloseGameButtonPressed -= CloseGame;
        }
    }
}
