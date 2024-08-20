using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine.SceneManagement;

public class PlayerMove2 : StrixBehaviour
{
    #region パブリック変数

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

    #region プライベート変数
    GameObject Tp1;
    GameObject Tp2;
    private Animator animator;
    private DateTime time1;
    private DateTime time2;
    DateTime TimeTmp1;
    DateTime TimeTmp2;
    #endregion

    #region ブール型
    bool flg1;
    bool flg2;
    bool Run = false; //走るアニメーション
    bool Shot = false; //打つアニメーション
    bool Walk = false; //歩くアニメーション
    bool Idle = true; //待機アニメーション
    public bool Dead = false; //体力がなくなった時のアニメーション
    #endregion
    //タイマー取得
    GameObject Timer;
    PanelAndCountDownController panelController;
    GameObject playerClass;
    PlayerClass playerhp;
    Shot2 shot;
    #region 当たり判定 
    // テレポートで移動するとき一旦力を加えない
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

    #region プレイヤーの操作
    [StrixRpc]
    public void Playermove()
    {
        if (StrixNetwork.instance.playerName != "Player2")
        {
            //player2.SetActive(false);
            print("プレイヤー2非表示");
            return;
        }

        //if (isLocal == false)
        //{
        //    return;
        //}

        // 現在時刻から0.5秒先を取得
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
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f); // プレイヤーを左向きにする
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
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f); // プレイヤーを右向きにする
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
                // スペースキーが押され、上下方向の速度がほぼゼロのとき、ジャンプする
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse); //上方向に力を加える
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
