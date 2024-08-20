using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine.SceneManagement;

public class PlayerMove2 : StrixBehaviour
{
    #region �p�u���b�N�ϐ�

    [SerializeField]
    GameObject player2; 
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField] GameManager gameManager;
    public float speed;
    public float jump;
    public float backspeed = 5;
    // private bool fripX = true;
    #endregion

    #region �v���C�x�[�g�ϐ�
    GameObject Tp1;
    GameObject Tp2;
    private Animator animator;
    private DateTime time1;
    private DateTime time2;
    DateTime TimeTmp1;
    DateTime TimeTmp2;
    #endregion

    #region �u�[���^
    bool flg1;
    bool flg2;
    bool Run = false; //����A�j���[�V����
    bool Shot = false; //�łA�j���[�V����
    bool Walk = false; //�����A�j���[�V����
    bool Idle = true; //�ҋ@�A�j���[�V����
    public bool Dead = false; //�̗͂��Ȃ��Ȃ������̃A�j���[�V����
    #endregion
    //�^�C�}�[�擾
    GameObject Timer;
    PanelAndCountDownController panelController;
    GameObject playerClass;
    PlayerClass playerhp;
    Shot2 shot;
    #region �����蔻�� 
    // �e���|�[�g�ňړ�����Ƃ���U�͂������Ȃ�
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Teleport1" || other.gameObject.tag == "Teleport2")
        {
            rb.velocity = Vector3.zero;
        }

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Tp1 = GameObject.FindGameObjectWithTag("Teleport1");
        //Tp2 = GameObject.FindGameObjectWithTag("Teleport2");
        Timer = GameObject.Find("PanelAndCountDownManager");
        panelController = Timer.GetComponent<PanelAndCountDownController>();
        playerClass = GameObject.Find("PlayerClass");
        playerhp = playerClass.GetComponent<PlayerClass>();
        shot = GetComponent<Shot2>();
    }
   
    // Update is called once per frame
    void Update()
    {

        if(Dead == false)
        {
            Playermove();
        }

        if (gameManager.GameEndFlg)
        {
            if (playerhp.g_p1_hp <= 0)
            {
                SceneManager.LoadScene("WinScene");
            }

            if (playerhp.g_p2_hp <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }

            SceneManager.LoadScene("StrixSettingsScene");
        }
    }

    #region �v���C���[�̑���
    [StrixRpc]
    public void Playermove()
    {
        if (StrixNetwork.instance.playerName != "Player2")
        {
            //player2.SetActive(false);
            print("�v���C���[2��\��");
            return;
        }

        //if (isLocal == false)
        //{
        //    return;
        //}

        // ���ݎ�������0.5�b����擾
        time1 = DateTime.Now.AddSeconds(1.0f);
        time2 = DateTime.Now.AddSeconds(2.0f);
        float x = transform.localPosition.x;
        Vector2 move = Vector2.zero;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (panelController.CountDownTime == 0.0F)
        {  
            Run = false;
            Shot= false;
            Walk= false;
            Idle= true;
            Dead = false;
            if (mousePosition.x < transform.localPosition.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f); // �v���C���[���������ɂ���
                if (Input.GetKey(KeyCode.A))
                {

                    move = new Vector3(-speed, 0, 0) * Time.deltaTime;
                    //animator.Play("Run");
                    Run = true;
                    Idle = false;
                }
                
                if (Input.GetKey(KeyCode.D))
                {

                    move = new Vector3(speed - backspeed, 0, 0) * Time.deltaTime;
                    //animator.Play("Run");
                    Walk = true;
                    Idle = false;
                }
            }
            if (mousePosition.x >= transform.localPosition.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f); // �v���C���[���E�����ɂ���
                if (Input.GetKey(KeyCode.A))
                {
                    move = new Vector3(-speed + backspeed, 0, 0) * Time.deltaTime;
                    //animator.Play("Run");
                    Walk = true;
                    Idle = false;
                }
                if (Input.GetKey(KeyCode.D))
                {

                    move = new Vector3(speed, 0, 0) * Time.deltaTime;
                    //animator.Play("Run");
                    Run = true;
                    Idle = false;
                }
            }
            Vector3 moveDirection = new Vector3(0, 0, 0);
            /*************************************
            if (Input.GetKey(KeyCode.A))
            {
                move = new Vector3(-speed, 0, 0) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {

                move = new Vector3(speed, 0, 0) * Time.deltaTime;
            }
            **************************************/
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                // �X�y�[�X�L�[��������A�㉺�����̑��x���قڃ[���̂Ƃ��A�W�����v����
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse); //������ɗ͂�������
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (flg1 == false)
                {
                    flg1 = true;

                    TimeTmp1 = time1;
                    //Shot = true;
                    animator.Play("Shot");
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (flg2 == false)
                {
                    flg2 = true;

                    TimeTmp2 = time2;
                    //Shot = true;
                    animator.Play("Shot");
                }
            }
            if (DateTime.Now > TimeTmp1)
            {
                if (flg1 == true)
                {
                    flg1 = false;
                }

            }
            if (DateTime.Now > TimeTmp2)
            {
                if (flg2 == true)
                {
                    flg2 = false;
                }
            }
            if (playerhp.g_p1_hp == 0 || playerhp.g_p2_hp == 0) 
            {
                Dead = true;
                Walk = false;
                Run = false;
                Shot= false;
                Idle= false;
                rb.velocity = Vector3.zero;
            }
            Vector3 v = new Vector3(move.x, move.y, 0);
            transform.localPosition += v;
            rb.AddForce(moveDirection * speed);   
            
            
            animator.SetBool("Run", Run);
            animator.SetBool("Shot", Shot);
            animator.SetBool("Walk", Walk);
            animator.SetBool("Idle", Idle);
            animator.SetBool("Dead", Dead);
        }
        #endregion
    }
}
