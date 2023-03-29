using UnityEngine;

namespace Game
{
    public class Wall : MonoBehaviour
    {
        [field: SerializeField] public bool AllowNoClip { get; private set; } = true;

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMovement player))
            {
                player.StopMovement();
            }
        }
    }
}
