using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Ctrl : MonoBehaviour
{

    public GameObject sound_cont;
    public GameObject Boss;

    public GameObject Spike1;  
    public GameObject Spike2;
    public GameObject bossHP;
    public GameObject bossHP_base;


    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        bossHP_base.gameObject.SetActive(false);
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("tag : " + collider.transform.tag);

        if (collider.transform.tag == "Player") {
            bossHP_base.gameObject.SetActive(true);
            sound_cont.GetComponent<sound_cont>().playBGM(1);

            if(Boss.GetComponent<Boss>().cleard != true) {
                Spike1.gameObject.SetActive(true);
                Spike2.gameObject.SetActive(true);
            }


        }
    }




    // Update is called once per frame
    void Update(){
        if (Boss.GetComponent<Boss>().cleard == true) {
            Spike1.gameObject.SetActive(false);
            Spike2.gameObject.SetActive(false);
            bossHP_base.gameObject.SetActive(false);
        }


        
        bossHP.GetComponent<RectTransform>().offsetMax = new Vector2(-(Boss.GetComponent<Boss>().HpOrigin - Boss.GetComponent<Boss>().Hp) * 182, 0);

    }
}
