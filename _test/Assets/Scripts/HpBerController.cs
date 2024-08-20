using SoftGear.Strix.Unity.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBerController : StrixBehaviour
{
    #region  プライベート
    // private
    [StrixSyncField] public Slider p1_slider;
    [StrixSyncField] public Slider p2_slider;
    //[SerializeField] DateTime time;
    GameObject playerClass;
    PlayerClass player;
    //DateTime TimeTmp;
    //bool flg;
    #endregion

    #region  パブリック
    // public

    #endregion
    
    #region  Init関数
    /// <summary>
    /// 初期化の関数
    /// </summary>
    void Init()
    {
        playerClass = GameObject.Find("PlayerClass");
        player = playerClass.GetComponent<PlayerClass>();

        //// プレイヤー変数
        //g_p1_hp = player1.p1_hp;  // プレイヤー1hpの取得
        //g_p2_hp = player1.p2_hp;  // プレイヤー2hpの取得

        // カウントダウンに使うフラグ
        //flg = false;
    }
    #endregion

    #region  GetCoolTime関数
    /// <summary>
    /// クールタイム用の時間を取得する関数
    /// </summary>
    void GetCoolTime()
    {
        // 現在時刻から0.5秒先を取得
        //time = DateTime.Now.AddSeconds(0.5);
    }
    #endregion

    #region  SetSliderValue関数
    /// <summary>
    /// スライダーに数値をセットする関数
    /// </summary>
    void SetSliderValue()
    {
        // sliderに現在の値を代入
        p1_slider.value = player.g_p1_hp;
        p2_slider.value = player.g_p2_hp;
    }
    #endregion

    #region  CoolTime関数
    /// <summary>
    /// クールタイムの処理
    /// </summary>
    //void CoolTime()
    //{
    //    //// クールタイムが無ければ
    //    //if (flg == false)
    //    //{
    //    //    flg = true;

    //    //    TimeTmp = time;

    //    //    // プレイヤーのHPが残っている場合
    //    //    if (player1.p1_hp > 0 && player1.p1_hp > 0)
    //    //    {
    //    //        p1_hp -= 10;
    //    //        p2_hp -= 10;
    //    //    }
    //    //}

    //    //// クールタイムの時間と比較し、現在時刻の方が大きかったら
    //    //if (DateTime.Now > TimeTmp)
    //    //{
    //    //    flg = false;
    //    //}
    //}
    #endregion

    #region  Start関数
    void Start()
    {
        // 変数の初期化
        Init();
    }
    #endregion

    #region  Update関数
    // Update is called once per frame
    void Update()
    {
        // クールタイムの取得
        //GetCoolTime();

        // スライダーに数値をセット
        SetSliderValue();

        // クールタイムでする処理
        //CoolTime();
    }
    #endregion
}
