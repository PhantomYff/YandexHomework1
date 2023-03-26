using UnityEngine;

namespace Game
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _targetToFollow;
        [SerializeField] private float _followingLerp;

        protected void FixedUpdate()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 position = transform.position;
            float x = Mathf.Lerp(position.x, _targetToFollow.position.x, _followingLerp * Time.fixedDeltaTime);

            position.x = x;
            transform.position = position;
        }
    }
}
