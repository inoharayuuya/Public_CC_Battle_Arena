using SoftGear.Strix.Unity.Runtime;
using UnityEngine;

public class ReflectionBullet : StrixBehaviour
{
    GameObject playerClass;
    PlayerClass player;
    [SerializeField]
    GameObject bullet;
    public float cnt = 0;
    float elapsedTime = 0f; // 経過時間のカウント変数
    private Rigidbody2D rb;
    private Transform bulletTransform;
    private string playerName;
    SpriteRenderer spriteRenderer;
    Collider2D collider;

    // 弾が壁に当たった時のSE
    [SerializeField] AudioSource BulletReflectionSE;

    // 弾がプレイヤーに当たった時のSE
    [SerializeField] AudioSource DamageSE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTransform = transform;
        playerClass = GameObject.Find("PlayerClass");
        player = playerClass.GetComponent<PlayerClass>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
    private void Update()
    { 
        elapsedTime += Time.deltaTime; // 経過時間をカウント

        if (elapsedTime >= 4)
        {
            Destroy(bullet);
        }
        Vector2 direction = rb.velocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        switch(cnt)
        {
                case 0:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
                case 1:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
                case 2:
                GetComponent<Renderer>().material.color = Color.red;
                break;
        }

    }

    private void DestroyBullet()
    {
        Destroy(bullet);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var name = collision.gameObject.name;

        if (collision.gameObject.CompareTag("Wall"))
        {
            // SEを鳴らす
            BulletReflectionSE.Play();

            cnt++;
            //Debug.Log("a");
        }
        if (cnt == 3)
        {
            Destroy(bullet);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //if(StrixNetwork.instance.playerName == "Player1")
            //{
            //    print("プレイヤー1に衝突");
            //    if (player.g_p1_hp > 0)
            //    {
            //        // SEを鳴らす
            //        DamageSE.Play();

            //        player.g_p1_hp -= player.p2_attack;
            //    }
            //    Debug.Log(player.g_p1_hp);
            //    Destroy(bullet);
            //}
            print("playerName" + playerName);

            if (name == "Player1(Clone)")
            {
                print("プレイヤー2に衝突");
                if (player.g_p2_hp > 0)
                {
                    // SEを鳴らす
                    DamageSE.Play();

                    player.g_p2_hp -= player.p1_attack;
                }
                Debug.Log(player.g_p2_hp);
                //Invoke("DestroyBullet", 0.25f);
                Destroy(bullet);
                //var color = spriteRenderer.color;
                //color.a = 0;
                //spriteRenderer.color = color;
                //collider.isTrigger = true;
            }
        }
    }
}