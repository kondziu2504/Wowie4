using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie4
{
    public class ShootingRobotParticles : MonoBehaviour
    {
        [SerializeField] ParticleSystem[] sparks;
        [SerializeField] ParticleSystem[] smokes;

        public void PlayDamage()
        {
            PlayRandom(sparks);
            PlayRandom(smokes);
        }

        private void PlayRandom(ParticleSystem[] particles)
        {
            for (int i = 0; i < particles.Length; i++)
                if (Random.value > 0.4f)
                    particles[i].Play();
        }
    }
}
