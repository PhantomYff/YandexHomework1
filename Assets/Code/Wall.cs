using UnityEngine;

namespace Game
{
    public class Wall : MonoBehaviour
    {
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMovement player))
            {
                player.StopMovement();
            }
        }
    }
}
