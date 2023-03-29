using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class BGParticlesBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _chasingWall;
        [SerializeField] private Transform _player;

        [Header("Handling distance between chasing wall and player")]
        [SerializeField] private float _maxDistanceToHandle;
        [SerializeField] private AnimationCurve _particlesCount;
        [SerializeField] private Gradient _color;
        [SerializeField] private AnimationCurve _noise;

        private ParticleSystem _particles;

        protected void Awake()
        {
            _particles = GetComponent<ParticleSystem>();
        }

        protected void Update()
        {
            float distance = _player.position.x - _chasingWall.position.x;

            if (distance <= _maxDistanceToHandle)
            {
                ParticleSystem.EmissionModule emission = _particles.emission;
                ParticleSystem.MainModule main = _particles.main;
                ParticleSystem.NoiseModule noise = _particles.noise;

                emission.rateOverTime = _particlesCount.Evaluate(distance);
                main.startColor = _color.Evaluate(distance / _maxDistanceToHandle);
                noise.strength = _noise.Evaluate(distance);
            }
        }
    }
}
