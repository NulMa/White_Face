using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemys : MonoBehaviour {
    
    
    //���� ������ ù ����� ���� ���� ���۶��� ���� �����ؾ���
    //�׷����, ù ����� ������ ������ ��̿� �ְ�
    //�װ��� ������� ���� ������ �Ҵ��ϴ� ���� ����.
    //�ϴ� �⺻ ���ʹ� ��� �ϳ��� �ϰ�
    //�������� ���Ͽ� ���ڸ� �� �� �ϴ� ������


    //���� ������ �帧 
    // �÷��̾� �ν�
    // �õ��� ����
    // �õ��⿡ ���� ���� ���� 2~3�� ����
    // ���� ������ ������ ���� �и��� �Ҵ�




    //�ĺ� �� ���� ����
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

    //�ٸ� ��ũ���� ����
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
        //������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //�÷��̾� ����
        void Chase() {
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y + 1);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);


            //���
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


        //���� ����
        if(cancounter == true) {
            for(int i = 0; i < 6; i++)
                GetComponentsInChildren<SpriteRenderer>()[i].color = new Color(5, 0, 0);

        }
        else {
            for (int i = 0; i < 6; i++)
                GetComponentsInChildren<SpriteRenderer>()[i].color = new Color(125, 125, 125);
        }



        //���� ����
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


    //����ȭ ����
    void stg_recover(){
        stg_point = 3;
        anim.ResetTrigger("stun");
    }


    //����, ����, �����ν� ��ƾ ����
    void startroutine() {

        StartCoroutine("Direction");
        StartCoroutine("SetAttack");
        StartCoroutine("Cliff");
    }

    //����, ����, �����ν� ��ƾ ����
    void stoproutine() {

        StopCoroutine("Direction");
        StopCoroutine("SetAttack");
        StopCoroutine("Cliff");

    }




    //�̵� ����
    void freezeon() {
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
    //���� ����
    void freezeoff() {
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    



    //���� �ν�
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

    //���� ����
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



    //��������Ʈ ������ȯ
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



    //�и� ���� ����
    void OnCounter(){
        cancounter = true;
        offattack();
    }
    void OffCounter(){
        cancounter = false;
    }



    //���� �ڽ� ǥ��
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atk.position, attackSize);
    }

    //AI �̵� ������
    public void Think() {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }


    //�и� �� on / off

    public void BallOn() {
        par_ball_script.gameObject.SetActive(true);
    }


    public void BallOff() {
        par_ball_script.gameObject.SetActive(false);
    }






    //���� �Ϸ� ����
    public void offattack(){
        anim.SetBool("isAttack", false);
    }

    public void offhit() {
        anim.SetBool("isHit", false);
        balls();
    }




    //�и� Ű 2���� ������ �ν� �޾ƾ���



    //take damage ���θ� 2������ ������
    //�и� ������ �ƴҽ� ������ ����� �� ��

    //�и� �ǰ� �� ��ġ ����
    public void TakeDamage() {
        Debug.Log("next_par & my_par " + next_par + ", " + playerScript.GetComponent<main_char>().my_par);

        if (cancounter == true && next_par == playerScript.GetComponent<main_char>().my_par) {
            stg_point = stg_point - 1;
            if(stg_point > 0) {
                playerScript.GetComponentInChildren<melee_sound>().GetComponent<Animator>().SetTrigger("suc");
                guard = true;
            }
            
            PlaySound("Attacked");

            //�з�����
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

        //�÷��̾� �ǰ�
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
