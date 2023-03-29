using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_drop : MonoBehaviour
{

    public GameManager manager;
    public GameObject boss;
    AudioSource audio;
    Collider2D collider;
    public bool droped = false;


    // Start is called before the first frame update
    void Start(){
        audio = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < 20) {
            collider.isTrigger = false;
            if(droped == false) {
                audio.Play();
                droped = true;
                boss.GetComponent<Boss>().Hp -= 3;
            }
        }
    }



    public void disableTrigger() {

        
        collider.isTrigger = true;
    }






}
