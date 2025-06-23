using Game.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Game.App.Enemy
{
    public sealed class EnemyController : BaseBehavior
    {
        [SerializeField] private LayerMask _playerLayer;

        private NavMeshAgent _navMeshAgent;
        private float _walkRadius = 10f;
        private bool _isLockedPlayer = false;
        private float _searchRadius = 10f;
        private Transform _closestPlayer;


        protected override void Initialise()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            MoveToRandomPoint();
        }

        protected override void OnUpdate()
        {
            SearchPlayer();

            if (!_isLockedPlayer)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    MoveToRandomPoint();
                }
            }
            else
            {
                Vector3 dir = (_closestPlayer.position - transform.position).normalized;
                dir.y = 0f;

                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        private void SearchPlayer()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius, _playerLayer);

            if (hits.Length == 0)
            {
                _isLockedPlayer = false;
                return;
            }

            var closest = hits
                .OrderBy(h => Vector3.SqrMagnitude(h.transform.position - transform.position))
                .First();
            _closestPlayer = closest.transform;
            _isLockedPlayer = true;
        }

        private void MoveToRandomPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * _walkRadius;
            randomDirection += transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _walkRadius, NavMesh.AllAreas))
            {
                _navMeshAgent.SetDestination(hit.position);
            }
        }
    }
}
