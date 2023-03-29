using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public GameManager manager;

    public AudioClip stuck;
    public AudioClip unlock;
    public AudioClip open;

    public Sprite opened;

    public bool done = false;

    AudioSource audio;
    Animator anim;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start(){
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        if(done == true) {
            spriteRenderer.sprite = opened;
        }
    }



    //잠김
    public void stuckGate() {
        audio.clip = stuck;
        audio.Play();
    }



    //잠금 해제
    public void openGate() {
        audio.clip = unlock;
        audio.Play();
        anim.SetBool("is_open", true);
    }




    //열림
    public void playOpen() {
        audio.clip = open;
        audio.Play();
    }

    //이동 가능 판정
    public void Pass() {
        
        gameObject.layer = 15;
        anim.SetBool("is_open", false);
        anim.WriteDefaultValues();
        anim.SetBool("is_pass", true);
    }

}
