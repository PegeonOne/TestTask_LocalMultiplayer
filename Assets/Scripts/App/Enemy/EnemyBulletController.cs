using Game.App.Player;
using Game.Core;
using Unity.Netcode;
using UnityEngine;

namespace Game.App.Enemy
{
    public sealed class EnemyBulletController : BaseBehavior
    {
        [SerializeField] private float detectionRadius = 0.3f;
        [SerializeField] private LayerMask playerLayer;

        protected override void OnUpdate()
        {
            DetectPlayer();
        }

        private void DetectPlayer()
        {
            var hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
            if (hits.Length == 0) return;

            PlayerController player = hits[0].gameObject.GetComponent<PlayerController>();
            player.DecreaseHealth();
            gameObject.SetActive(false);
        }

        protected override void OnDispose()
        {
            GetComponent<NetworkObject>().Despawn();
        }
    }
}
