using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class CollisionHandler : MonoBehaviour
{      

    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTansitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        CheatDisableCollisions();
        CheatWithForLetterL();
    }

    void OnCollisionEnter(Collision other) 
    {
      if(isTansitioning || collisionDisabled) {return;}

      switch(other.gameObject.tag)
      {
        case "Friendly":
            Debug.Log("pierwszy case");
            break;
        case "Finish":
            FinishLevel();
            break;
        default:
            StartCrashSequence();
            break;
      }
    }

    void StartCrashSequence() 
    { 
        audioSource.Stop();
        isTansitioning = true;
        crashParticles.Play();
        audioSource.PlayOneShot(explosion);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }

    void FinishLevel()
    {
         isTansitioning = true;
         audioSource.Stop();
         successParticles.Play();
         audioSource.PlayOneShot(success);
         GetComponent<Movement>().enabled = false;
         Invoke("NextLevel", delayTime);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel ()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void CheatDisableCollisions() 
        {
            if (Input.GetKey(KeyCode.C))
                {
                    collisionDisabled = !collisionDisabled;
                }
        }
    void CheatWithForLetterL()
        {
            if (Input.GetKey(KeyCode.L))
            {
                NextLevel();
            }
        }

}
