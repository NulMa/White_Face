using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_char : MonoBehaviour {

    //����Ŭ��
    public AudioClip PlayerWalk;
    public AudioClip PlayerJump;
    public AudioClip Player_GMed;
    
    public int UI_code;


    public float maxSpeed;
    public float maxJump;
    public float jumpTime;
    public bool canpar = false;
    public int PHP = 4; //���� ü��
    public int MaxHP = 4; //ü�� ����
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

        //����
        if (Input.GetButtonUp("Horizontal")) {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }


        //������ȯ
        if (rigid.velocity.x < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Player_dirc = false;
        }
        if (rigid.velocity.x > 0) {
            transform.rotation = Quaternion.identity;
            Player_dirc = true;
        }

        //���� ��¡
        if (Input.GetButton("Jump")) {
            if (jumpTime < maxJump)
                jumpTime += Time.deltaTime * 100;
            else
                jumpTime = maxJump;
        }

        //����
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

        //�и� �� ����

        if (curTime <= 0) {

            //��, 1
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






            //��, 2
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
















            //����
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

            //��ȣ�ۿ�
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
            //���� ������
            curTime -= Time.deltaTime;
        }











        //Ű �Է½� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        if(stuned == false && Gamemanager.BoxControler.gameObject.activeSelf == false) {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
            

        //�ȱ�
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

        //��, �� �ִ� �ӵ� ����
        if (rigid.velocity.x > maxSpeed) {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1)) {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        //���� ����
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

    //�÷��̾� �ǰ�
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
�⺻ �̵�
- �¿� �̵�
- Ư�� �ൿ(�и�, ����)�� �ϸ� �̵��� ������ ����


Ű�� ������ �ð��� ����� ������
- Ű�� ���� �������� �������� ����(���� ����)


�и� A, B
- �Ӹ� �� ������ ���� ���� �ٸ� Ű �Է�
- �и� �� �� ���� ���ϵ� ����

UI
- �÷��̾� ü��
- �Ǿ�ǰ �ִ� �������� ���� ������
- ���� ���� ��
- ���� �� ����� ���� ü�� ǥ��
- �޴����� ���۹� ����
- NPC ��ȭ
- ���� ��� ����

�������� 
- �������� ü���� ����
- ��ó ������������ ������
- ȿ���� ���
���
- ��� �̹����� �÷��̾ �����
- �ٰ��ϼ��� õõ��, �����ϼ��� ���� �������� ���ٰ� �ο�

��ȣ�ۿ� �� ����
- ���� �׷α� ����(����)�� ������ �� �� ����Ű�� �̿��� ü���� ���� �� ����
- �Ǵ� ���������� ��ȣ�ۿ� ������ ��ü(�μ����� ��, ���� ����)�� ��� ����

��ȣ�ۿ� 
- �μ����� ��
	- ��ȣ�ۿ� �� ȿ������ �Բ� ��¥ �� ���̵� �ƿ�
	- ������ �������� ������ �ְų� ���� ������ ����(�ļ�)�� ��
	
- ��� ��
	- ����ִ� ���� ��� �׳� ��ȣ�ۿ�� ���� ���� �Ҹ��� ���, ������ �� ����
	- ������ ������ ���� �� �ִ� Ű ���� �� ��ȣ�ۿ�� ���� ������ �ִϸ��̼� ��� �� ������ �� ����
	
- �޽����� 
	- ��ں��� ����� �� ������Ʈ�� ����
	- ��ȣ�ۿ�� ���� �ٰ� �����Ѵٴ� ���� '��' ���
	- Ȱ��ȭ�� ������ Ÿ�� �Ҹ� ���
	- �� �Ҹ��� �÷��̾��� ��ġ�� ��ȣ�ۿ��� �ָ� �������� �۾����ٰ� �鸮�� ����
	- �÷��̾ ��������, �ֱٿ� ��ȣ�ۿ��� �޽��������� ��Ȱ��
	- �÷��̾��� ü���� �ִ밡 �ƴϰų� �Ҹ�ǰ�� �ִ밡 �ƴ� �� ��ȣ�ۿ�� ������ ����� ��

- NPC
	- ��ȣ�ۿ� Ű�� ���� �̾߱⸦ ���� �� ����
	- ���� �÷��̿� ������ �� �� �ִ� �̾߱⸦ ��( ex : ���� ���Ľ� ������ �ִ� ������ ����)
	- ���� NPC�� ȹ���ߴ� ������ �̿��� �ŷ��� �� �� ����
	- �ŷ��� ���� ������ �ִ� ������ ���� ����
	- ������ ���� UI�� ��� �������� �̸�, ����, ȿ�� ���� ǥ��
	- ���� �� ���� ���� ȿ���� ��� �� ���� ���� ���ݸ�ŭ ����, ������ �������� ȸ��ó���ؼ� ���� ��ư ��Ȱ��ȭ
	- ���� ���� ������ ������ �� ���� ��� ����� ���

- ������ ȹ��
 	- �ʵ忡�� �߰� ������ ���ڿ� ��ȣ�ۿ�� ȹ���� �������� ������ ȿ�� ���
	- �ִ� ü�� ������ �Ҹ�ǰ ���� ������ ���� ���� ȿ�� ����
	- �ʵ忡�� ȹ���߰ų� ���ο��� ������ �������� ESCŰ�� ���� �޴�â���� Ȯ�ΰ���

- �Ҹ�ǰ(�Ǿ�ǰ)
	- ���� ������Ʈ(����, �Ǿ�ǰ �ִ�ġ ���� ������)

�Ϲ� ����
- �ϳ��� ���� ���
- �� �� ������ ���� ����
- óġ�� �ҷ��� ���� ���


����
- 3������ ���� (�и� ���� 2, �Ұ��� 1)
- ���� ���� ���� ��ǿ� ���� �ٸ� ���� �ο�(1���� ������ 3���� ���� ����� ������ ����)
- �� ����� ��ֹ� ����, ����� ���� // ������ ��ų� �������� ������ Ż�� X
- ������ ������ �μ����� ���� ���� ��ȣ�ۿ� ������ ������ �ߵ��ϸ� ������ ü���� �̸� ��Ƴ��� �� ����
- ���� ���� �� ����� ����ġ, ������ ����� ���� ����




 * 
 */