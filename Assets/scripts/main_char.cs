using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_char : MonoBehaviour {

    //사운드클립
    public AudioClip PlayerWalk;
    public AudioClip PlayerJump;
    public AudioClip Player_GMed;
    
    public int UI_code;


    public float maxSpeed;
    public float maxJump;
    public float jumpTime;
    public bool canpar = false;
    public int PHP = 4; //현재 체력
    public int MaxHP = 4; //체력 상한
    public int my_par = 0;
    public bool damaged = false;
    public bool Player_dirc;
    public bool stuned = false;

    public bool black = false;

    public Transform pos;
    public Vector2 boxSize;

    private float curTime;
    public float coolTime = 0.7f;

    public GameManager Gamemanager;
    public GameObject MSound;
    public Rigidbody2D rigid;

    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;
    

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void backspd() {
        maxSpeed = 5;
    }

    void PlayerSound(string action) {
        switch (action) {
            case "WALK":
                audioSource.clip = PlayerWalk;
                break;

            case "JUMP":
                audioSource.clip = PlayerJump;
                break;

            case "Get_med":
                audioSource.clip = Player_GMed;
                break;

        }

        if (audioSource.isPlaying)
            return;
        else
            audioSource.PlayOneShot(audioSource.clip);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Gamemanager.items[UI_code].GetComponent<Chest_cont>().chestclose();

    }


    void Update() {

        //정지
        if (Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }


        //방향전환
        if (rigid.velocity.x < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Player_dirc = false;
        }
        if (rigid.velocity.x > 0) {
            transform.rotation = Quaternion.identity;
            Player_dirc = true;
        }

        //점프 차징
        if (Input.GetButton("Jump")) {
            if (jumpTime < maxJump)
                jumpTime += Time.deltaTime * 100;
            else
                jumpTime = maxJump;
        }

        //점프
        if (Input.GetButtonUp("Jump") && !anim.GetBool("isJumping") && Gamemanager.BoxControler.gameObject.activeSelf == false) {
            audioSource.Stop();
            PlayerSound("JUMP");
            anim.SetBool("isJumping", true);
            rigid.AddForce(Vector2.up * jumpTime, ForceMode2D.Impulse);
            jumpTime = 0;
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);

    }

    public void CanPar() {
        canpar = true;
    }
    public void NotPar() {
        canpar = false;
    }

    void FixedUpdate() {

        //패링 및 공격

        if (curTime <= 0) {

            //흰, 1
            if (Input.GetButton("Fire1") && damaged == false && Gamemanager.BoxControler.gameObject.activeSelf == false) {
                my_par = 1;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds) {
                    if (collider.tag == "Enemy" && collider.GetComponent<enemys>().stg_point > 0) {
                        collider.GetComponent<enemys>().TakeDamage();
                    }
                    if (collider.tag == "Boss" && collider.GetComponent<Boss>().stg_point > 0) {
                        collider.GetComponent<Boss>().par_succ();
                    }



                }
                audioSource.Stop();
                MSound.GetComponent<melee_sound>().MeleeSound("PARA");
                maxSpeed = 0;
                anim.SetTrigger("ParA");

                curTime = coolTime;
                Invoke("backspd", 0.3f);
            }

            /*
            else if (Input.GetButton("Fire1") && Gamemanager.BoxControler.gameObject.activeSelf == true) {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D collider in collider2Ds) {
                    if (collider.tag == "NPC" && Gamemanager.BoxControler.gameObject.activeSelf == true) {
                        collider.gameObject.GetComponent<NPC_manager>().arrMinus();
                        Gamemanager.Talk(collider.gameObject);

                    }
                }
            }
            */






            //검, 2
            else if (Input.GetButton("Fire2") && damaged == false && Gamemanager.BoxControler.gameObject.activeSelf == false) {
                my_par = 2;
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds) {
                    if (collider.tag == "Enemy" && collider.GetComponent<enemys>().stg_point > 0) {
                        collider.GetComponent<enemys>().TakeDamage();
                    }
                    if (collider.tag == "Boss" && collider.GetComponent<Boss>().stg_point > 0) {
                        collider.GetComponent<Boss>().par_succ();
                    }
                }
                audioSource.Stop();
                MSound.GetComponent<melee_sound>().MeleeSound("PARB");
                maxSpeed = 0;
                anim.SetTrigger("ParB");
                curTime = coolTime;

                Invoke("backspd", 0.3f);
            }



            /*
            else if (Input.GetButton("Fire2") && Gamemanager.BoxControler.gameObject.activeSelf == true) {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds) {
                    if (collider.tag == "NPC" && Gamemanager.BoxControler.gameObject.activeSelf == true) {
                        collider.gameObject.GetComponent<NPC_manager>().arrPlus();
                        Gamemanager.Talk(collider.gameObject);

                    }
                }
            }
            */
















            //마격
            else if (Input.GetButton("Fire3") && damaged == false && Gamemanager.BoxControler.gameObject.activeSelf == false) {
                my_par = 0;
                audioSource.Stop();
                MSound.GetComponent<melee_sound>().MeleeSound("FB");
                maxSpeed = 0;
                anim.SetTrigger("FB");
                curTime = coolTime;
                Invoke("backspd", 0.3f);


                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds) {

                    if (collider.tag == "Enemy") {
                        collider.GetComponent<enemys>().checkFB();
                    }
                    else if (collider.tag == "Boss") {
                        if (collider.GetComponent<Boss>().stg_point <= 0) {
                            collider.GetComponent<Boss>().Hp--;
                        }
                        else {
                            collider.GetComponent<Boss>().par_succ();
                        }

                    }

                    else if (collider.tag == "hidden") {
                        collider.GetComponent<hidden_wall>().breakwall();
                    }
                    else if (collider.tag == "drop") {
                        Debug.Log(collider.tag + "  ");
                        collider.GetComponent<Spike_drop>().disableTrigger();
                    }



                }

            }

            //상호작용
            else if (Input.GetButton("Fire4")) {

                curTime = coolTime;

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds) {


                    if (collider.tag == "NPC") {
                        Gamemanager.Talk(collider.gameObject);
                    }


                    if (collider.tag == "save_Point") {
                        collider.GetComponent<save_Manage>().triggerOn();
                    }

                    if (collider.tag == "Items") {
                        collider.GetComponent<Chest_cont>().chestopen();
                    }

                    if (collider.tag == "Herb") {
                        if (Gamemanager.nowmeds < Gamemanager.maxmeds) {
                            collider.GetComponent<Herb_con>().fill_med();
                            audioSource.Stop();
                            PlayerSound("Get_med");
                        }

                    }

                    if(collider.tag == "Gate") {
                        if(Gamemanager.key == true) {
                            collider.GetComponent<Gate>().openGate();
                        }
                        else if (Gamemanager.key == false) {
                            collider.GetComponent<Gate>().stuckGate();
                        }

                    }



                }

                maxSpeed = 0;
                Invoke("backspd", 0.3f);
            }


        }
        else {
            //공격 딜레이
            curTime -= Time.deltaTime;
        }











        //키 입력시 이동
        float h = Input.GetAxisRaw("Horizontal");
        if(stuned == false && Gamemanager.BoxControler.gameObject.activeSelf == false) {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
            

        //걷기
        if (Mathf.Abs(rigid.velocity.x) < 0.1f) {
            anim.SetBool("isWalking", false);
            if (audioSource.clip == PlayerWalk)
                audioSource.Stop();
            else return;
        }
        else {
            anim.SetBool("isWalking", true);
            if(anim.GetBool("isJumping") == false) {
                PlayerSound("WALK");
            }
            
        }

        //좌, 우 최대 속도 지정
        if (rigid.velocity.x > maxSpeed) {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1)) {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //착지 판정
        if (rigid.velocity.y <= 0) {
            Debug.DrawRay(rigid.position - new Vector2(0.4f, 1), new Vector3(0.8f, 0, 0), new Color(1, 0, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position - new Vector2(0.4f, 1), new Vector3(0.8f, 0, 0), 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) {
                if (rayHit.distance < 0.5f) {
                    anim.SetBool("isJumping", false);
                }
            }
        }

        

    }

    void delayed_damaged() {
        gameObject.tag = "Player";
        spriteRenderer.color = new Color(1, 1, 1, 1);
        damaged = false;
    }

    //플레이어 피격
    public void PlayerDamaged(Vector2 targetPos) {
        PHP = PHP - 1;
        damaged = true;
        Debug.Log("P_damaged");
        maxSpeed = 5;
        audioSource.Stop();
        MSound.GetComponent<melee_sound>().MeleeSound("HIT");
        gameObject.tag = "Damaged_Player";
        spriteRenderer.color = new Color(1, 1, 1, 0.7f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;


        rigid.AddForce(Vector2.right * dirc, ForceMode2D.Impulse);


        Invoke("delayed_damaged", 0.8f);

    }

}


/*
기본 이동
- 좌우 이동
- 특정 행동(패링, 공격)을 하면 이동을 강제로 멈춤


키를 누르는 시간에 비례한 점프력
- 키를 오래 누를수록 점프력이 증가(상한 존재)


패링 A, B
- 머리 위 구슬의 색상에 따라 다른 키 입력
- 패링 할 수 없는 패턴도 존재

UI
- 플레이어 체력
- 의약품 최대 소지량과 현재 소지량
- 소지 동전 수
- 보스 맵 입장시 보스 체력 표기
- 메뉴에서 조작법 설명
- NPC 대화
- 상점 기능 구현

함정구현 
- 떨어지면 체력이 닳음
- 근처 안전지역에서 리스폰
- 효과음 재생
배경
- 배경 이미지가 플레이어를 따라옴
- 근경일수록 천천히, 원경일수록 빨리 움직여서 원근감 부여

상호작용 및 공격
- 적을 그로기 상태(기절)에 빠지게 한 후 공격키를 이용해 체력을 깎을 수 있음
- 또는 물리적으로 상호작용 가능한 물체(부서지는 벽, 나무 함정)의 기능 실행

상호작용 
- 부서지는 벽
	- 상호작용 시 효과음과 함께 가짜 벽 페이드 아웃
	- 유용한 아이템이 숨겨져 있거나 보스 공략에 도움(후술)을 줌
	
- 잠긴 문
	- 잠겨있는 문의 경우 그냥 상호작용시 문이 막힌 소리만 출력, 지나갈 수 없음
	- 보스를 격파해 얻을 수 있는 키 습득 후 상호작용시 문이 열리는 애니매이션 출력 후 지나갈 수 있음
	
- 휴식지점 
	- 모닥불의 모습을 한 오브젝트로 존재
	- 상호작용시 불이 붙고 저장한다는 뜻의 '記' 출력
	- 활성화시 장작이 타는 소리 재생
	- 위 소리는 플레이어의 위치와 상호작용해 멀리 떨어지면 작아지다가 들리지 않음
	- 플레이어가 쓰러지면, 최근에 상호작용한 휴식지점에서 부활함
	- 플레이어의 체력이 최대가 아니거나 소모품이 최대가 아닐 때 상호작용시 재충전 기능을 함

- NPC
	- 상호작용 키를 통해 이야기를 들을 수 있음
	- 종종 플레이에 도움을 줄 수 있는 이야기를 함( ex : 보스 격파시 도움을 주는 함정의 정보)
	- 상인 NPC는 획득했던 동전을 이용해 거래를 할 수 있음
	- 거래를 통해 도움을 주는 아이탬 구매 가능
	- 아이탬 구매 UI의 경우 아이탬의 이름, 가격, 효과 등을 표기
	- 구매 시 구매 성공 효과음 재생 및 동전 수가 가격만큼 차감, 구매한 아이탬은 회색처리해서 구매 버튼 비활성화
	- 가진 돈이 부족해 구매할 수 없을 경우 경고음 재생

- 아이템 획득
 	- 필드에서 발견 가능한 상자에 상호작용시 획득한 아이템의 정보와 효과 출력
	- 최대 체력 증가나 소모품 소지 상한의 증가 등의 효과 제공
	- 필드에서 획득했거나 상인에게 구매한 아이탬은 ESC키를 눌러 메뉴창에서 확인가능

- 소모품(의약품)
	- 관련 오브젝트(약초, 의약품 최대치 증가 아이템)

일반 몬스터
- 하나의 공격 모션
- 매 번 랜덤한 구슬 색상
- 처치시 소량의 동전 드롭


보스
- 3가지의 패턴 (패링 가능 2, 불가능 1)
- 패턴 안의 공격 모션에 각기 다른 구슬 부여(1개의 패턴은 3개의 공격 모션을 가지고 있음)
- 맵 입장시 장애물 생성, 배경음 변경 // 보스를 잡거나 쓰러지기 전까지 탈출 X
- 위에서 서술한 부서지는 벽을 통해 상호작용 가능한 함정을 발동하면 보스의 체력을 미리 깎아놓을 수 있음
- 보스 격파 시 배경음 원위치, 동전을 드롭해 보상 제공




 * 
 */