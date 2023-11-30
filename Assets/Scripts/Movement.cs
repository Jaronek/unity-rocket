using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource thrustSound;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem firstThrusterParticles;
    [SerializeField] ParticleSystem secondThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] AudioClip sideEngine;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ThrustSound();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {   
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ThrustSound() {
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            MainEngineSound();
        }
        else {
             thrustSound.Stop();
        }
    }
    void StartThrusting()
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            
            if (!firstThrusterParticles.isPlaying)
            {
                firstThrusterParticles.Play();
            }
            if (!secondThrusterParticles.isPlaying)
            {
                secondThrusterParticles.Play();
            }
        }
    void StopThrusting()
    {
        firstThrusterParticles.Stop();
        secondThrusterParticles.Stop();
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }
    void RotateLeft()
    {
        ApplyRotation(rotateThrust);
        if (!leftThrusterParticles.isPlaying )
        {
            leftThrusterParticles.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotateThrust);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }
     void StopRotate()
    {   
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

     void MainEngineSound()
    {
        if (!thrustSound.isPlaying)
            {
                thrustSound.PlayOneShot(mainEngine);
            }
    }
   
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }
}