using Game.Core;
using Game.App.Utils;
using Unity.Netcode;
using UnityEngine;
using static Unity.Netcode.NetworkManager;

namespace Game.App.Network
{
    public sealed class NetworkController : BaseController
    {
        [SerializeField] private NetworkManager _netManager;
        private VectorRandomiser _vectorRandomiser;

        public override void Init(AppConfig config)
        {
            _netManager.ConnectionApprovalCallback += ApproveClientConnection;
            _vectorRandomiser = new VectorRandomiser(config.SpawnBoxArea.x, config.SpawnBoxArea.y);
        }

        public void StartHost()
        {
            _netManager.StartHost();
        }

        public void StartClient()
        {
            _netManager.StartClient();
        }

        private void ApproveClientConnection(ConnectionApprovalRequest request, ConnectionApprovalResponse response)
        {
            response.Approved = true;
            response.CreatePlayerObject = true;
            response.Position = _vectorRandomiser.GetRandomSpawnPosition();
            response.Rotation = Quaternion.identity;
        }
    }
}
