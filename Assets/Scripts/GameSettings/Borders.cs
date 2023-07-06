
using UnityEngine;

namespace Assets.Scripts.GameSettings
{
    [System.Serializable]
    public class Borders
    {
        [SerializeField]
        public Vector2 BorderHorizontal = new Vector2(-5, 5); // x = min, y = max
        [SerializeField]
        public Vector2 BorderVertical = new Vector2(-5, 5); // x = min, y = max
    }
}