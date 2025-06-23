using Game.Core;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Game.App.Enemy
{
    public sealed class EnemyBulletSpawner : BaseBehavior
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _bulletPrefab;

        private bool _isShooting;
        private Vector3 _direction;
        private float timer = 0f;
        private float interval = 3f;

        public void Shoot(Vector3 direction, bool shootState)
        {
            _isShooting = shootState;
            _direction = direction;
        }

        public override void OnNetworkSpawn()
        {
            if (!IsHost)
            {
                enabled = false;
                return;
            }
        }

        protected override void OnUpdate()
        {
            if (_isShooting)
            {
                timer += Time.deltaTime;

                if (timer >= interval)
                {
                    timer = 0f;

                    CreateBullet();
                }
            }
        }

        private void CreateBullet()
        {
            GameObject bullet = Instantiate(_bulletPrefab, _spawnPoint.position, Quaternion.identity);
            NetworkObject netObject = bullet.GetComponent<NetworkObject>();
            netObject.Spawn(true);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(_direction * 5, ForceMode.Impulse);
            Destroy(bullet, 5);
        }
    }
}
