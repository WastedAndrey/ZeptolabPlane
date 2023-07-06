
using UnityEngine;

namespace Assets.Scripts.Units
{
    public interface IDamagable
    {
        Team Team { get; }
        void ReceiveDamage(int damage);
        GameObject GameObject { get; }
    }
}