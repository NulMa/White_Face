using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader_sys : MonoBehaviour
{


    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager.shopUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (gameObject.GetComponent<NPC_manager>().talk_arr_num == 0  && gameObject.GetComponent<NPC_manager>().manager.NameBox.text == "º¸ºÎ»ó") {
            gameObject.GetComponent<NPC_manager>().manager.Shop_Btn.gameObject.SetActive(true);
        }
        else {
            gameObject.GetComponent<NPC_manager>().manager.Shop_Btn.gameObject.SetActive(false);
        }


        if(gameObject.GetComponent<NPC_manager>().manager.BoxControler.activeSelf == false) {
            manager.shopUI.gameObject.SetActive(false);
        }

        

}


    public void shopopen() {
        if(manager.shopUI.activeSelf == true) {
            manager.shopUI.gameObject.SetActive(false);
        }
        else {
            manager.shopUI.gameObject.SetActive(true);
        }
        

    }
}
