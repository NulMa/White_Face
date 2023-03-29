using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_manager : MonoBehaviour
{

    public string NpcName;
    public int talk_arr_num;
    public GameManager manager;

    public string[] talk = {};

    public bool A;
    public bool S;



    // Start is called before the first frame update
    void Start()
    {
        talk_arr_num = 0;
    }





    public void arrPlus() {

        if(talk_arr_num < talk.Length - 1) {
            talk_arr_num++;
            manager.TalkBox.text = talk[talk_arr_num];
        }


    }

    public void arrMinus() {

        if (talk_arr_num > 0) {
            talk_arr_num--;
            manager.TalkBox.text = talk[talk_arr_num];
        }



    }

    // Update is called once per frame
    void Update(){
        if (manager.BoxControler.gameObject.activeSelf == true && NpcName == manager.NameBox.text) {

            if (Input.GetButtonUp("Fire1")) {
                arrMinus();
            }

            else if (Input.GetButtonUp("Fire2")) {
                arrPlus();
            }
        }


        /*

        if (talk_arr_num == talk.Length - 1) {
            S = false;
        }
        else {
            S = true;
        }
        if (talk_arr_num == 0) {
            A = false;
        }
        else {
            A = true;
        }





        if (S == false) {
            manager.S.gameObject.SetActive(false);
        }
        else if(S == true) {
            manager.S.gameObject.SetActive(true);
        }

        if(A == false) {
            manager.A.gameObject.SetActive(false);
        }

        else if(A == true){
            manager.A.gameObject.SetActive(true);
        }

        */






        if (Input.GetButtonDown("Cancel")) {
            manager.BoxControler.gameObject.SetActive(false);
            talk_arr_num = 0;
        }
    }
}
