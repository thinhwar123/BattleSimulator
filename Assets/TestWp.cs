using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWp : MonoBehaviour
{
    [SerializeField] private ParticleSystem theParticleSystem;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var particles = new ParticleSystem.Particle[theParticleSystem.main.maxParticles];
            var currentAmount = theParticleSystem.GetParticles(particles);

            for (int i = 0; i < currentAmount; i++)
            {
                particles[i].position = Vector3.zero;
            }

            // Apply the particle changes to the Particle System
            theParticleSystem.SetParticles(particles, currentAmount);
        }
    }

}
