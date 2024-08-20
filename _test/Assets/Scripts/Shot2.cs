using SoftGear.Strix.Unity.Runtime;
using System;
using UnityEngine;

public class Shot2 : StrixBehaviour
{
    [SerializeField]
    private Texture2D cursor; //カーソル
    public GameObject bulletPrefab;//反射する弾
    public GameObject bulletPrefab2;//追尾する弾
    public float bulletSpeed = 10f;
    public float offsetDistance = 0.5f;//弾が出る位置の設定

    private DateTime time1;
    private DateTime time2;
    DateTime TimeTmp1;
    DateTime TimeTmp2;
    bool flg1;
    bool flg2;
    bool Dead;
    //タイマー取得
    GameObject Timer;
    PanelAndCountDownController panelController;
    GameObject playerClass;
    PlayerClass playerhp;

    // SE
    [SerializeField] AudioSource ReflectionSE;
    [SerializeField] AudioSource ArrowSE;

    private void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
        Timer = GameObject.Find("PanelAndCountDownManager");
        panelController = Timer.GetComponent<PanelAndCountDownController>();
        playerClass = GameObject.Find("PlayerClass");
        playerhp = playerClass.GetComponent<PlayerClass>();
        Dead= false;
    }
    void Update()
    {
        if(Dead == false && StrixNetwork.instance.selfRoomMember.GetName() == "Player2")
        {
          Shots();
        }

    }
    [StrixRpc]
    public void Shots()
    {
        if (StrixNetwork.instance.playerName != "Player2")
        {
            return;
        }

        // 現在時刻から0.5秒先を取得
        time1 = DateTime.Now.AddSeconds(1.0f);
        time2 = DateTime.Now.AddSeconds(2.0f);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (panelController.CountDownTime == 0.0F)
        {
            if (mousePosition.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f); // プレイヤーを左向きにする
            }
            if (mousePosition.x >= transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f); // プレイヤーを右向きにする
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (flg1 == false)
                {
                    // SEを鳴らす
                    ReflectionSE.Play();

                    flg1 = true;

                    TimeTmp1 = time1;
                    Vector2 direction = (mousePosition - (transform.position + transform.up * offsetDistance)).normalized;
                    Vector2 velocity = direction.normalized * bulletSpeed; // 正規化後に速さを掛ける
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.right * offsetDistance, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = velocity; // 速度ベクトルを設定
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (flg2 == false)
                {
                    // SEを鳴らす
                    ArrowSE.Play();

                    flg2 = true;

                    TimeTmp2 = time2;
                    Vector2 direction = (mousePosition - (transform.position + transform.up * offsetDistance)).normalized;
                    Vector2 velocity = direction.normalized * bulletSpeed; // 正規化後に速さを掛ける
                    GameObject bullet = Instantiate(bulletPrefab2, transform.position + transform.right * offsetDistance, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = velocity; // 速度ベクトルを設定
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
            }
        }
    }
}
