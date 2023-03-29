using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trap_mach : MonoBehaviour
{
    public Transform right;
    public Transform left;
    public Transform trap_range;

    

    public Vector2 boxSize;
    public Vector2 rangeSize;

    public string side = " ";

    public GameObject player;
    float blackAlpha;


    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(right.position, boxSize);
        Gizmos.DrawWireCube(left.position, boxSize);
        Gizmos.DrawWireCube(trap_range.position, rangeSize);
    }

    // Start is called before the first frame update
    void Start(){
        
        blackAlpha = GetComponentInParent<blackout_manage>().fadeV;
    }



    void FixedUpdate(){


        /*
        if(player.GetComponent<main_char>().black == true) {
            GetComponentInParent<blackout_manage>().FadeIn(true);

            if(GetComponentInParent<blackout_manage>().fadeV == 1) {
                player.GetComponent<main_char>().black = false;
            }

        }
        else {
            GetComponentInParent<blackout_manage>().FadeOut(true);
        }
        */

        

        //위치 저장
        Collider2D[] Col_right = Physics2D.OverlapBoxAll(right.position, boxSize, 0);
        foreach (Collider2D collider in Col_right) {
            if (collider.tag == "Player") {
                side = "right";
            }
        }

        Collider2D[] Col_left = Physics2D.OverlapBoxAll(left.position, boxSize, 0);
        foreach (Collider2D collider in Col_left) {
            if (collider.tag == "Player") {
                side = "left";
            }
        }




        //피격 판정 밑 리스폰
        Collider2D[] TrapActive = Physics2D.OverlapBoxAll(trap_range.position, rangeSize, 0);
        foreach (Collider2D collider in TrapActive) {
            if (collider.tag == "Player") {

                

                if (GetComponentInParent<blackout_manage>().fadeV < 0.98f) {
                    //GetComponentInParent<blackout_manage>().FadeOut(false);
                    player.GetComponent<main_char>().stuned = true;
                    
                    //암전
                    GetComponentInParent<blackout_manage>().FadeIn(true);

                }

                else{
                    //GetComponentInParent<blackout_manage>().playSE();

                    player.GetComponent<main_char>().PlayerDamaged(new Vector2(0, 0));

                    player.GetComponent<main_char>().stuned = false;

                    GetComponentInParent<blackout_manage>().FadeIn(false);
                    if (side == "right") {
                        //암전 해제
                        player.GetComponent<main_char>().transform.position = new Vector3(right.position.x, right.position.y, -2);
                        GetComponentInParent<blackout_manage>().FadeOut(true);

                    }

                    else if (side == "left") {
                        //암전 해제
                        player.GetComponent<main_char>().transform.position = new Vector3(left.position.x, left.position.y, -2);
                        GetComponentInParent<blackout_manage>().FadeOut(true);
                    }
                }

                
                   
            }
        }


    }





}
