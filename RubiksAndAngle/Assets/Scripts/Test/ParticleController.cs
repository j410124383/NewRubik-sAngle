using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

    ParticleSystem[] particles;
    GameObject[] particlesObjs;

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();


        if (particles == null) return;
        particlesObjs = new GameObject[particles.Length];
        for (int i = 0; i < particles.Length; i++)
        {
            particlesObjs[i] = particles[i].gameObject;
        }

    }

    public void Play()
    {
        if (particles == null) return;

        None();


        for (int i = 0; i < particles.Length; i++)
        {
            if (particlesObjs[i].activeSelf == false)
                particlesObjs[i].SetActive(true);

            particles[i].Stop();
            particles[i].Play();
        }

    }

    public void Stop()
    {
        if (particles == null) return;

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Stop();
            if (particlesObjs[i].activeSelf == true) ;
                particlesObjs[i].SetActive(false);

        }

        None();

    }


    public void None()
    {
        if (particlesObjs == null) return;

        for (int i = 0; i < particlesObjs.Length; i++)
        {
            if (particlesObjs[i].activeSelf)
                particlesObjs[i].SetActive(false);
        }
    }


}
