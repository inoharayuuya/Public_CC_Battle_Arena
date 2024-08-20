using SoftGear.Strix.Unity.Runtime;
using UnityEngine;

public class ReflectionBullet2 : MonoBehaviour
{
    [SerializeField] GameObject shot;
    Shot1 shot1;
    GameObject playerClass;
    PlayerClass player;
    [SerializeField]
    GameObject bullet;
    public float cnt = 0;
    float elapsedTime = 0f; // �o�ߎ��Ԃ̃J�E���g�ϐ�
    private Rigidbody2D rb;
    private Transform bulletTransform;
    private string playerName;

    // �e���ǂɓ�����������SE
    [SerializeField] AudioSource BulletReflectionSE;

    // �e���v���C���[�ɓ�����������SE
    [SerializeField] AudioSource DamageSE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTransform = transform;
        playerClass = GameObject.Find("PlayerClass");
        player = playerClass.GetComponent<PlayerClass>();
        shot1 = shot.GetComponent<Shot1>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime; // �o�ߎ��Ԃ��J�E���g

        if (elapsedTime >= 4)
        {
            Destroy(bullet);
        }
        Vector2 direction = rb.velocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        switch (cnt)
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
            // SE��炷
            BulletReflectionSE.Play();

            cnt++;
            Debug.Log("a");
        }
        if (cnt == 3)
        {
            Destroy(bullet);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (name == "Player1")
            {
                print("�v���C���[1�ɏՓ�");
                if (player.g_p1_hp > 0)
                {
                    // SE��炷
                    DamageSE.Play();

                    player.g_p1_hp -= player.p2_attack;
                }
                Debug.Log(player.g_p1_hp);
                //Invoke("DestroyBullet", 0.25f);
                Destroy(gameObject);
            }
            //if (StrixNetwork.instance.playerName == "Player2")
            //{
            //    print("�v���C���[2�ɏՓ�");
            //    if (player.g_p2_hp > 0)
            //    {
            //        // SE��炷
            //        DamageSE.Play();

            //        player.g_p2_hp -= player.p1_attack;
            //    }
            //    Debug.Log(player.g_p2_hp);
            //    Destroy(bullet);
            //}
        }
    }
}