using Game.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Game.App.Enemy
{
    public sealed class EnemyMoveController : BaseBehavior
    {
        private NavMeshAgent _navMeshAgent;
        private float _walkRadius = 10f;

        protected override void Initialise()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            MoveToRandomPoint();
        }

        public void ApplyEnemyMove(bool isLokedPlayer, Transform closestPlayer)
        {
            if (!isLokedPlayer)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    MoveToRandomPoint();
                }
            }
            else
            {
                Vector3 dir = (closestPlayer.position - transform.position).normalized;
                dir.y = 0f;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
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
