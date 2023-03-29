using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBG : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;

    private MeshRenderer render;
    private float offset;
    public float speed;
    public float self;
    public int posZ;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }


    void FixedUpdate(){
        
        //Ä· À§Ä¡Á¶Á¤
        transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y, posZ);
        

        //¹è°æ ÀÌµ¿
        if (player.GetComponent<main_char>().rigid.velocity.x != 0) {

            if((player.GetComponent<main_char>().Player_dirc == false)) {
                speed = -0.1f;
            }
            else if ((player.GetComponent<main_char>().Player_dirc == true)) {
                speed = 0.1f;
            }
        }
        //Á¤Áö½Ã ¸ØÃã
        else {
            speed = 0;
        }


        offset += Time.deltaTime * speed * self;
        render.material.mainTextureOffset = new Vector2(offset, 2);

    }

}
