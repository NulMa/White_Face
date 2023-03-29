using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_boss : MonoBehaviour
{

    public int HpOrigin;
    public int stg_point_Origin;
    ////////////////////////////////////
    public int Hp = 20;
    public int stg_point = 5;

    public bool cancounter = false;


    public Transform pos;
    public Vector2 boxSize;
    public Transform atk;
    public Vector2 attackSize;

    public int speed = 1;

    public int coins;

    public int rand;

    //움직일 방향
    public int nextMove = 0;
    public int next_par;


    public GameObject playerScript;





    AudioSource audioSource;
    Rigidbody2D rigid;
    Animator anim;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Think();
        next_par = Random.Range(1, 3);
        startroutine();
    }

    // Update is called once per frame
    void FixedUpdate(){

        rigid.velocity = new Vector2(nextMove * 2 * speed, rigid.velocity.y);



        //색상 변경
        if (cancounter == true) {
            for (int i = 0; i < 6; i++)
                GetComponentsInChildren<SpriteRenderer>()[i].color = new Color(5, 0, 0);
        }
        else {
            for (int i = 0; i < 6; i++)
                GetComponentsInChildren<SpriteRenderer>()[i].color = new Color(125, 125, 125);
        }

    }

    //추적, 공격, 절벽인식 루틴 시작
    void startroutine() {

        StartCoroutine("Direction");
        StartCoroutine("SetAttack");
        StartCoroutine("Cliff");
        StartCoroutine("chase");

    }

    //추적, 공격, 절벽인식 루틴 정지
    void stoproutine() {

        StopCoroutine("Direction");
        StopCoroutine("SetAttack");
        StopCoroutine("Cliff");
        StopCoroutine("chase");

    }








    IEnumerator chase() {
        yield return new WaitForEndOfFrame();
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y + 1);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        //사망
        if (Hp < 0) {
            gameObject.SetActive(false);
        }

        foreach (Collider2D collider in collider2Ds) {
            if (collider.tag == "Player" && rayhit.collider != null && anim.GetBool("isAttack3") != true) {
                //BallOn();


                if (playerScript.transform.position.x < transform.position.x) {
                    CancelInvoke();
                    nextMove = -1;
                    Invoke("Think", 2);
                }

                else if (playerScript.transform.position.x > transform.position.x) {
                    CancelInvoke();
                    nextMove = 1;
                    Invoke("Think", 2);
                }
            }
        }
    }

    //절벽 인식
    IEnumerator Cliff() {
        yield return new WaitForEndOfFrame();
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y + 1);
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));



        if (rayhit.collider == null) {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 2);
        }
    }

    //공격 범위
    IEnumerator SetAttack() {
        yield return new WaitForEndOfFrame();
        Collider2D[] attackbox = Physics2D.OverlapBoxAll(atk.position, attackSize, 0);
        foreach (Collider2D collider in attackbox) {
            Debug.Log("enter");
            stoproutine();
            if (collider.tag == "Player"
            && anim.GetBool("isAttack1") == false
            && anim.GetBool("isAttack2") == false
            && anim.GetBool("isAttack3") == false) {
                rand = Random.Range(1, 4);
                Debug.Log("Rand :" + rand);
                switch (rand) {

                    case 1:
                        anim.SetBool("isAttack1", true);
                        Debug.Log("A1");
                        break;

                    case 2:
                        anim.SetBool("isAttack2", true);
                        Debug.Log("A2");
                        break;

                    case 3:
                        anim.SetBool("isAttack3", true);
                        speed = 3;
                        Debug.Log("A3");
                        break;
                }  
            }
        }
    }



    //스프라이트 방향전환
    IEnumerator Direction() {
        yield return new WaitForEndOfFrame();
        if (nextMove == 1) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("isWalking", true);
        }
        else if (nextMove == -1) {
            transform.rotation = Quaternion.identity;
            anim.SetBool("isWalking", true);
        }
        else if (nextMove == 0) {
            anim.SetBool("isWalking", false);
        }
    }

    public void Think() {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }























}
