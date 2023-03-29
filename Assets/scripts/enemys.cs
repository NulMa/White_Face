using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemys : MonoBehaviour {
    
    
    //공격 패턴은 첫 모션을 보고 뒤의 동작또한 예측 가능해야함
    //그런고로, 첫 모션은 랜덤을 돌려서 어레이에 넣고
    //그것을 기반으로 뒤의 패턴을 할당하는 것이 좋다.
    //일단 기본 몬스터는 모션 하나로 하고
    //보스에서 패턴에 투자를 좀 더 하는 것으로


    //공격 패턴의 흐름 
    // 플레이어 인식
    // 시동기 설정
    // 시동기에 따른 공격 패턴 2~3개 연속
    // 공격 패턴의 개수에 따라 패링볼 할당




    //식별 및 공격 범위
    public Transform pos;
    public Vector2 boxSize;
    public Transform atk;
    public Vector2 attackSize;
    public ObjectMAnager ObjectMAnager;

    public AudioClip EnemyAttacked;
    public AudioClip EnemyDown;


    public int HpOrigin;
    public int stg_point_Origin;


    public int Hp = 2;
    public int stg_point = 3;

    
    public int nextMove = 0;
    public int next_par;
    public bool cancounter = false;
    public int coins;

    public bool guard;

    //다른 스크립드 접근
    public GameObject par_ball_script;
    public GameObject playerScript;
    
    AudioSource audioSource;
    MeshRenderer renderer;
    Rigidbody2D rigid;
    Animator anim;

    

    void PlaySound(string action) {
        audioSource.Stop();
        switch (action) {
            case "Attacked":
                audioSource.clip = EnemyAttacked;
                break;

            case "Down":
                audioSource.clip = EnemyDown;
                break;

        }

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

    public void FixedUpdate() {
        //움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //플레이어 추적
        void Chase() {
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y + 1);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);


            //사망
            if(Hp < 0) {
                /////////////////////////////////////////
                gameObject.SetActive(false);

            }


            foreach (Collider2D collider in collider2Ds) {
                if (collider.tag == "Player") {
                    BallOn();


                    if (playerScript.transform.position.x < transform.position.x && rayhit.collider != null) {
                        CancelInvoke();
                        nextMove = -1;
                        Invoke("Think", 2);
                    }

                    else if (playerScript.transform.position.x > transform.position.x && rayhit.collider != null) {
                        CancelInvoke();
                        nextMove = 1;
                        Invoke("Think", 2);
                    }
                }
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



        //스턴 판정
        if (stg_point <= 0 && anim.GetBool("isHit") == true) {
            anim.SetTrigger("stun");
            anim.SetBool("isHit", false);
        }



        Chase();
        startroutine();

    }

    private void OnDisable() {
        //Debug.Log("coins");
        GameObject coin = ObjectMAnager.MakeObj("Coin");
        coin.transform.position = pos.position - new Vector3(0,0.5f);
    }


    void balls() {
        next_par = Random.Range(1, 3);
    }


    //무력화 해제
    void stg_recover(){
        stg_point = 3;
        anim.ResetTrigger("stun");
    }


    //추적, 공격, 절벽인식 루틴 시작
    void startroutine() {

        StartCoroutine("Direction");
        StartCoroutine("SetAttack");
        StartCoroutine("Cliff");
    }

    //추적, 공격, 절벽인식 루틴 정지
    void stoproutine() {

        StopCoroutine("Direction");
        StopCoroutine("SetAttack");
        StopCoroutine("Cliff");

    }




    //이동 정지
    void freezeon() {
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
    //정지 해제
    void freezeoff() {
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        yield return new WaitForEndOfFrame();

        Collider2D[] attackbox = Physics2D.OverlapBoxAll(atk.position, attackSize, 0);
        foreach (Collider2D collider in attackbox) {

                if (collider.tag == "Player" && anim.GetBool("isAttack") == false) {
                anim.SetBool("isAttack", true);
                stoproutine();
            }
            else {
            }
            
        }
    }



    //스프라이트 방향전환
    IEnumerator Direction() {
        yield return new WaitForEndOfFrame();
        if (nextMove == -1 && anim.GetBool("isHit") != true) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("isWalking", true);
        }
        else if (nextMove == 1 && anim.GetBool("isHit") != true) {
            transform.rotation = Quaternion.identity;
            anim.SetBool("isWalking", true);
        }
        else if (nextMove == 0) {
            anim.SetBool("isWalking", false);
        }
    }



    //패링 가능 상태
    void OnCounter(){
        cancounter = true;
        offattack();
    }
    void OffCounter(){
        cancounter = false;
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
        anim.SetBool("isAttack", false);
    }

    public void offhit() {
        anim.SetBool("isHit", false);
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
                guard = true;
            }
            
            PlaySound("Attacked");

            //밀려나기
            if (playerScript.transform.position.x < transform.position.x) {
                stoproutine();
                rigid.velocity = new Vector2(2, 2);
                anim.SetBool("isAttack", false);
                anim.SetBool("isHit", true);

            }
            else if (playerScript.transform.position.x > transform.position.x) {
                stoproutine();
                rigid.velocity = new Vector2(2, 2);
                anim.SetBool("isAttack", false);
                anim.SetBool("isHit", true);
            }
        }

        //플레이어 피격
        else  {
            if (stg_point <= 0) {

            }

            else {
                Collider2D[] attackbox = Physics2D.OverlapBoxAll(atk.position, attackSize, 0);
                foreach (Collider2D collider in attackbox) {
                    if (collider.tag == "Player") {
                        collider.GetComponent<main_char>().PlayerDamaged(transform.position);
                        guard = true;
                    }
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
            Debug.Log("CheckFB");
            playerScript.GetComponent<main_char>().PlayerDamaged(transform.position);
        }
    }


    public void atk_end() {


        Collider2D[] attackbox = Physics2D.OverlapBoxAll(atk.position, attackSize, 0);
        foreach (Collider2D collider in attackbox) {
            if (collider.tag == "Player" && guard == false) {
                collider.GetComponent<main_char>().PlayerDamaged(transform.position);
            }
            else if(guard == true) {
                guard = false;
            }
        }
    }


}
