using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class TrackingBullet2 : StrixBehaviour
{
    GameObject playerClass;
    PlayerClass player;
    [SerializeField] GameObject bullet;
    [SerializeField] private Transform tirgetTrans; //�ǂ�������Ώۂ�Transform
    [SerializeField] private float bulletSpeed;  �@ //�e�̑��x
    [SerializeField] private float limitSpeed;      //�e�̐������x
    private Rigidbody2D rb;                         //�e��Rigidbody2D
    private Transform bulletTrans;                  //�e��Transform
    float elapsedTime = 0f; // �o�ߎ��Ԃ̃J�E���g�ϐ�
    public float delayTime = 1f;   // �x�����ԁi1�b�j

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletTrans = GetComponent<Transform>();
        playerClass = GameObject.Find("PlayerClass");
        player = playerClass.GetComponent<PlayerClass>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime; // �o�ߎ��Ԃ��J�E���g
        if (elapsedTime >= delayTime)
        {
            if (tirgetTrans != null)
            {
                Vector3 vector3 = tirgetTrans.position - bulletTrans.position;  //�e����ǂ�������Ώۂւ̕������v�Z
                rb.AddForce(vector3.normalized * bulletSpeed);                  //�����̒�����1�ɐ��K���A�C�ӂ̗͂�AddForce�ŉ�����

                float speedXTemp = Mathf.Clamp(rb.velocity.x, -limitSpeed, limitSpeed); //X�����̑��x�𐧌�
                float speedYTemp = Mathf.Clamp(rb.velocity.y, -limitSpeed, limitSpeed);  //Y�����̑��x�𐧌�
                rb.velocity = new Vector3(speedXTemp, speedYTemp);

            }
        }
        //�e�̔��ł�������ɉ摜�̌��������킹��
        Vector2 direction = rb.velocity.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletTrans.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    [StrixRpc]
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            player.g_p1_hp -= player.p1_attack;
            Debug.Log(player.g_p1_hp);
            // �I�u�W�F�N�g���폜
            Destroy(bullet);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(bullet);
        }
    }
}
