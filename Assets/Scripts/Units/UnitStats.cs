
using UnityEngine;

namespace Assets.Scripts.Units
{
    [System.Serializable]
    public class UnitStats
    {
        [SerializeField]
        public float AttackSpeed;
        [SerializeField]
        public float MovementSpeed;
        [SerializeField]
        public int Health;

        public UnitStats GetCopy()
        {
            return new UnitStats()
            {
                AttackSpeed = this.AttackSpeed,
                MovementSpeed = this.MovementSpeed,
                Health = this.Health
            };
        }
    }
}