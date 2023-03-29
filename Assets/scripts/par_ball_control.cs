using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class par_ball_control : MonoBehaviour{

    public Sprite[] par_ball;
    

    //몬스터 스크립트
    public GameObject monster_script;

    SpriteRenderer par_ball_renderer;



    void Start(){
        par_ball_renderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    void FixedUpdate(){

        
        if (monster_script.GetComponent<enemys>().next_par == 2) {
            par_ball_renderer.sprite = par_ball[0];
        }
        else if (monster_script.GetComponent<enemys>().next_par == 1) {
            par_ball_renderer.sprite = par_ball[1];
        }



    }









}
