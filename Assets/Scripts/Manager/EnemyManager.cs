using System.Collections.Generic;
using UnityEngine;

namespace TouhouPride.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { private set; get; }

        public List<ACharacter> Enemies { get; } = new();

        private void Awake()
        {
            Instance = this;
        }

        public void Register(ACharacter c)
        {
            Enemies.Add(c);
        }
    }
}