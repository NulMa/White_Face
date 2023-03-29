using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_par : MonoBehaviour {

    public Sprite[] par_ball;
    public int[] pat_arr;

    //���� ��ũ��Ʈ
    public GameObject monster_script;

    SpriteRenderer par_ball_renderer;



    void Start() {
        par_ball_renderer = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    void FixedUpdate() {


        if (monster_script.GetComponent<Boss>().next_par == 2) {
            par_ball_renderer.sprite = par_ball[0];
        }
        else if (monster_script.GetComponent<Boss>().next_par == 1) {
            par_ball_renderer.sprite = par_ball[1];
        }

        
    }


    //��1 ��2
    public void patt_ctrl() {
        

        //patt�� ���� ��ȣ
        //int patt = monster_script.GetComponent<Boss>().rand;

        //if(patt != 3 && patt == 0) {
        //pat_arr[]�� ���� �迭

        for (int i = 0; i < pat_arr.Length; i++) {
                //par_rand�� ���� ���� ����
                int par_rand = Random.Range(1, 3);


                transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = par_ball[par_rand];
                pat_arr[i] = par_rand;

            }
        //}



    }


    public void cantGuard() {

        for (int i = 0; i < pat_arr.Length; i++) {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = par_ball[0];
            pat_arr[i] = 2;

        }
        //}




    }








}
