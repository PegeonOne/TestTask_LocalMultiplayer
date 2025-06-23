using Game.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game.App.Enemy
{
    public sealed class EnemyController : BaseBehavior
    {
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private EnemyMoveController _moveController;
        [SerializeField] private EnemyBulletSpawner _bulletSpawner;

        private bool _isLockedPlayer = false;
        private float _searchRadius = 10f;
        private Transform _closestPlayer;

        protected override void OnUpdate()
        {
            SearchPlayer();
            _moveController.ApplyEnemyMove(_isLockedPlayer, _closestPlayer);
            _bulletSpawner.Shoot(transform.forward, _isLockedPlayer);
        }

        private void SearchPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius, _playerLayer);

            if (hits.Length == 0)
            {
                _isLockedPlayer = false;
                _closestPlayer = null;
                return;
            }

            var closest = hits
                .OrderBy(h => Vector3.SqrMagnitude(h.transform.position - transform.position))
                .First();
            _closestPlayer = closest.transform;
            _isLockedPlayer = true;
        }       
    }
}
