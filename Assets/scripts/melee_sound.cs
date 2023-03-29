using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_sound : MonoBehaviour
{
    AudioSource audioSource;

    Animator animator;

    public AudioClip PlayerParA;
    public AudioClip PlayerParB;
    public AudioClip PlayerSucPar;
    public AudioClip PlayerFB;
    public AudioClip PlayerHit;




    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    public void MeleeSound(string action) {
        switch (action) {

            case "PARA":
                audioSource.clip = PlayerParA;
                break;

            case "PARB":
                audioSource.clip = PlayerParB;
                break;

            case "FB":
                audioSource.clip = PlayerFB;
                break;

            case "SucPar":
                animator.SetTrigger("suc");
                audioSource.clip = PlayerSucPar;
                break;

            case "HIT":
                animator.SetTrigger("fail");
                audioSource.clip = PlayerHit;
                break;
        }
            audioSource.PlayOneShot(audioSource.clip);
    }



    void Update() {
    }
}
