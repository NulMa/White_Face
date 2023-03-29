using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 스턴 상태에서 패링키 입력시 피격되는 버그 수정, 일반 몬스터 패링 성공시 피격버그 수정
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    //식별 및 공격 범위
    public Transform pos;
    public Vector2 boxSize;
    public Transform atk;
    public Vector2 attackSize;
    public ObjectMAnager ObjectMAnager;

    public AudioClip slash;
    public AudioClip punch;

    

    public int HpOrigin;
    public int stg_point_Origin;
    public int Hp = 2;
    public int stg_point = 3;
    public int nextMove = 0;
    public int next_par;
    public int coins;
    public int speed = 1;
    public int rand;
    public int attack_count = 0;


    public bool charging;
    public bool cancounter = false;
    public bool pared;
    public bool cleard;
    public bool guard;

    //다른 스크립드 접근
    public GameObject par_ball_script;
    public GameObject playerScript;
    public GameObject axe;
    public GameObject fist;


    AudioSource audioSource;
    Rigidbody2D rigid;
    Animator anim;



    void PlaySound(string action) {
        audioSource.Stop();
        switch (action) {
            case "Axe":
                audioSource.clip = slash;
                break;

            case "Fist":
                audioSource.clip = punch;
                break;

        }

        if (audioSource.isPlaying)
            return;
        else
            audioSource.PlayOneShot(audioSource.clip);
    }




    public void axeSound() {
        audioSource.Stop();
        audioSource.clip = slash;
        if (audioSource.isPlaying)
            return;
        else
            audioSource.PlayOneShot(audioSource.clip);
    }

    public void fistSound() {
        audioSource.Stop();
        audioSource.clip = punch;
        if (audioSource.isPlaying)
            return;
        else
            audioSource.PlayOneShot(audioSource.clip);
    }








    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Think();
        next_par = Random.Range(1, 3);
    }


    public void par_succ() {
        if (cancounter == true && playerScript.GetComponent<main_char>().my_par == par_ball_script.GetComponent<Boss_par>().pat_arr[attack_count - 1]) {
            Debug.Log("attack_count :" + attack_count);
            //par_ball_script.GetComponent<Boss_par>().pat_arr[attack_count] = 3;
            playerScript.GetComponent<main_char>().MSound.GetComponent<melee_sound>().MeleeSound("SucPar");
            stg_point--;
            guard = true;
        }



        else if (cancounter == false || true && playerScript.GetComponent<main_char>().my_par != par_ball_script.GetComponent<Boss_par>().pat_arr[attack_count-1]) {
            if(stg_point <= 0) {
                
            }
            else {
                Debug.Log("damaged :" + attack_count);
                playerScript.GetComponent<main_char>().MSound.GetComponent<melee_sound>().MeleeSound("HIT");
                playerScript.GetComponent<main_char>().PHP--;
                guard = true;
            }

        }


        //스턴 판정
        if (stg_point <= 0) {
            anim.SetTrigger("stun");
        }
    }



    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player" && charging == true) {
            playerScript.GetComponent<main_char>().MSound.GetComponent<melee_sound>().MeleeSound("HIT");
            playerScript.GetComponent<main_char>().PHP--;
        }
    }


    public void chargeOn() {
        charging = true;
    }
    public void chargeOff() {
        charging = false;
    }



    public void FixedUpdate() {

        //Debug.Log("speed : " + speed + " \tVelocity" + rigid.velocity);
        //움직임
        rigid.velocity = new Vector2((nextMove * 2) * speed, rigid.velocity.y);


        if (rand != 0) {
            par_ball_script.GetComponent<Boss_par>().gameObject.SetActive(true);
        }
        else {
            par_ball_script.GetComponent<Boss_par>().gameObject.SetActive(false);
        }


        Collider2D[] axeCol = Physics2D.OverlapBoxAll(axe.transform.position, new Vector2(2,2), 0);
        foreach (Collider2D collider in axeCol) {
            if (collider.tag == "Player") {
                //Debug.Log("axe");
            }
        }


        //색상 변경
        if(cancounter == true) {
            for(int i = 0; i < 6; i++)
                GetComponentsInChildren<SpriteRenderer>()[i].color = new Color(5, 0, 0);

        }
        else {
            for (int i = 0; i < 6; i++)
                GetComponentsInChildren<SpriteRenderer>()[i].color = new Color(125, 125, 125);
        }




        startroutine();

        //사망
        if (Hp < 0) {
            gameObject.SetActive(false);
            cleard = true;
        }




    }

    private void OnDisable() {
        //Debug.Log("coins");
        for(int i = 0; i < 40; i++) {
            GameObject coin = ObjectMAnager.MakeObj("Coin");
            coin.transform.position = pos.position - new Vector3(0, 0.5f);
        }
        GetComponentInParent<Boss_Ctrl>().sound_cont.GetComponent<sound_cont>().playBGM(0);
        
    }


    void balls() {
        next_par = Random.Range(1, 3);
    }
    

    //무력화 해제
    void stg_recover(){
        stg_point = stg_point_Origin;
        anim.ResetTrigger("stun");
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



        foreach (Collider2D collider in collider2Ds) {
            if (collider.tag == "Player" && rayhit.collider != null && anim.GetBool("isAttack3") != true) {
                //BallOn();

                //Debug.Log( " distance" + Mathf.Abs(playerScript.transform.position.x - transform.position.x) + " / " + Mathf.Abs(4));
                
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

                

                else if ((Mathf.Abs(playerScript.transform.position.x - transform.position.x) < Mathf.Abs(2)) && anim.GetBool("isAttack3") == false) {
                    CancelInvoke();
                    nextMove = 0;
                    Invoke("Think", 2);
                    Debug.Log("nextMove = 0");
                }
            }
        }
    }


    //절벽 인식
    IEnumerator Cliff() {

        yield return new WaitForEndOfFrame();

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y + 1);
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));


        
        if (rayhit.collider == null){
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 2);
        }
    }



    //공격 범위
    IEnumerator SetAttack() {
        Collider2D[] attackbox = Physics2D.OverlapBoxAll(atk.position, attackSize, 0);
        foreach (Collider2D collider in attackbox) {
            if((anim.GetBool("isAttack1") == false && anim.GetBool("isAttack2") == false && anim.GetBool("isAttack3") == false)) {
                if (collider.tag == "Player") {
                    stoproutine();
                    rand = Random.Range(1, 4);
                    //Debug.Log("Rand :" + rand);


                    switch (rand) {
                        case 1:
                            anim.SetBool("isAttack1", true);
                            speed = 0;
                            par_ball_script.GetComponent<Boss_par>().patt_ctrl();
                            break;


                        case 2:
                            anim.SetBool("isAttack2", true);
                            speed = 0;
                            par_ball_script.GetComponent<Boss_par>().patt_ctrl();
                            break;


                        case 3:
                            anim.SetBool("isAttack3", true);
                            par_ball_script.GetComponent <Boss_par>().cantGuard();
                            StopCoroutine("chase");
                            speed = 3;
                            break;
                    }
                }
            }
        }
        yield return new WaitForEndOfFrame();
    }



    //스프라이트 방향전환
    IEnumerator Direction() {
        yield return new WaitForEndOfFrame();
        if (nextMove == 1) {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            par_ball_script.GetComponent<Boss_par>().transform.GetChild(0).transform.localPosition = new Vector3(0.5f, 0, 0);
            par_ball_script.GetComponent<Boss_par>().transform.GetChild(2).transform.localPosition = new Vector3(-0.5f, 0, 0);

            anim.SetBool("isWalking", true);
        }
        else if (nextMove == -1) {
            transform.rotation = Quaternion.identity;
            par_ball_script.GetComponent<Boss_par>().transform.GetChild(0).transform.localPosition = new Vector3(-0.5f, 0, 0);
            par_ball_script.GetComponent<Boss_par>().transform.GetChild(2).transform.localPosition = new Vector3(0.5f, 0, 0);
            anim.SetBool("isWalking", true);
            
            
        }
        else if (nextMove == 0) {
            anim.SetBool("isWalking", false);
        }
    }



    //패링 가능 상태
    void OnCounter(){
        pared = false;
        cancounter = true;
        attack_count++;
        //Debug.Log(par_ball_script.GetComponent<Boss_par>().pat_arr[attack_count-1]);

    }
    void OffCounter(){
        cancounter = false;
        if (pared == false) {
            GetComponent<main_char>().PlayerDamaged(transform.position);
            //playerdamage
        }
        
    }



    //범위 박스 표기
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atk.position, attackSize);
    }

    //AI 이동 선택지
    public void Think() {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }


    //패링 볼 on / off

    public void BallOn() {
        par_ball_script.gameObject.SetActive(true);
    }
    public void BallOff() {
        par_ball_script.gameObject.SetActive(false);
    }






    //공격 완료 상태
    public void offattack(){
        //Debug.Log("offattack");

        anim.SetBool("isAttack1", false);
        anim.SetBool("isAttack2", false);
        anim.SetBool("isAttack3", false);

        attack_count = 0;
        par_ball_script.GetComponent<Boss_par>().gameObject.SetActive(false);
        speed = 1;
    }


    public void stop() {
        speed = 0;
    }

    public void speedset() {
        speed = 3;
        //Debug.Log("speed up " + speed);
    }
    public void offhit() {
        balls();
    }




    //패링 키 2개로 나눠서 인식 받아야함



    //take damage 내부를 2가지로 나누고
    //패링 가능이 아닐시 반응도 만들면 될 것

    //패링 피격 및 수치 조정


    public void TakeDamage() {
        Debug.Log("next_par & my_par " + next_par + ", " + playerScript.GetComponent<main_char>().my_par);
        if (cancounter == true && next_par == playerScript.GetComponent<main_char>().my_par) {
            stg_point = stg_point - 1;
            if(stg_point > 0) {
                playerScript.GetComponentInChildren<melee_sound>().GetComponent<Animator>().SetTrigger("suc");
            }
            PlaySound("Attacked");
        }

        //플레이어 피격
        else {
            Collider2D[] attackbox = Physics2D.OverlapBoxAll(atk.position, attackSize, 0);
            foreach (Collider2D collider in attackbox) {
                if (collider.tag == "Player") {
                    collider.GetComponent<main_char>().PlayerDamaged(transform.position);
                }
            }
        }
    }

    public void checkFB() {
        if(stg_point <= 0) {
            
            Hp = Hp - 1;
            TakeDamage();
            PlaySound("Down");
        }
        else {
            playerScript.GetComponent<main_char>().PlayerDamaged(transform.position);
        }
    }


    public void atk_end() {

            if (guard == false) {
                playerScript.GetComponent<main_char>().PlayerDamaged(transform.position);
            }
            else if (guard == true) {
                guard = false;
            }

    }

}
