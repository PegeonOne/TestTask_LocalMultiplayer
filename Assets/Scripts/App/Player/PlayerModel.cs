using NUnit.Framework;
using UnityEngine;

namespace Game.App.Player
{
    public sealed class PlayerModel
    {
        public int Health;

        public PlayerModel(int baseHealth) 
        {
            Health = baseHealth;
        }
    }
}
