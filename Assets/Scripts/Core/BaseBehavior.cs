using System;
using Unity.Netcode;
using UnityEngine;

namespace Game.Core
{
    public abstract class BaseBehavior : NetworkBehaviour, IDisposable
    {
        protected virtual void Initialise() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnDispose() { }

        private void Start()
        {
            Initialise();
        }

        private void Update()
        {
            OnUpdate();
        }

        public void Dispose()
        {
            OnDispose();
        }
    }
}
