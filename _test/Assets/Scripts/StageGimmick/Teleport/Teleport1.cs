using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Teleport1 : MonoBehaviour
{

    GameObject Player1;
    GameObject Player2;
    GameObject Tp1;
    GameObject Tp2;

    CoolTime script_cooltime;

    // プレイヤーがTp1に触れたらTp2にテレポートする
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && script_cooltime.cnt == 0)
        {
            Player1.transform.position = Tp2.transform.position;
            Debug.Log("プレイヤー１、Tp1に触れました");

            script_cooltime.cooltimetmp = script_cooltime.cooltime;
            script_cooltime.cnt++;


        }

        if (other.gameObject.tag == "Player2" && script_cooltime.cnt == 0)
        {
            Player2.transform.position = Tp2.transform.position;
            Debug.Log("プレイヤー2、Tp1に触れました");

            script_cooltime.cooltimetmp = script_cooltime.cooltime;
            script_cooltime.cnt++;


        }

        



    }




    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player");
        Player2 = GameObject.FindGameObjectWithTag("Player2");
        Tp1 = GameObject.FindGameObjectWithTag("Teleport1");
        Tp2 = GameObject.FindGameObjectWithTag("Teleport2");

        script_cooltime = GameObject.Find("CoolTimeManager").GetComponent<CoolTime>();

    }

    // Update is called once per frame
    void Update()
    {

        if (DateTime.Now > script_cooltime.cooltimetmp)
        {
            script_cooltime.cnt = 0;
        }
    }
}