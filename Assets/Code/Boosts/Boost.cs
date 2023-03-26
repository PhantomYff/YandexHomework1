using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Boost : MonoBehaviour
    {
        [field: SerializeField] public BoostType Type { get; private set; }
    }
}
