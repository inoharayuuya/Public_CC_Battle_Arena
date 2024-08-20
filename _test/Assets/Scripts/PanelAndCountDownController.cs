using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine.SceneManagement;

public class PanelAndCountDownController : StrixBehaviour
{
    #region  プライベート
    [SerializeField] GameObject CountPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject GameSetPanel;
    private Color color;
    #endregion

    #region  パブリック
    [SerializeField] public float CountDownTime =  5;
    [SerializeField] public float CountTimer    = 60;
    [SerializeField] public float GameSetTimer  =  2;
    public TextMeshProUGUI TextCountDown;  // 表示用テキストUI
    public Text TextTimer;                 // 表示用テキストUI
    public bool GameSetFlg;
    private bool gameStratFlg;
    #endregion

    #region  Init関数
    /// <summary>
    /// Init関数
    /// 変数の初期化や、最初に処理したいものを書く
    /// </summary>
    void Init()
    {
        CountPanel.SetActive(true);
        GamePanel.SetActive(true);
        GameSetPanel.SetActive(false);
        TextTimer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        TextTimer.text = String.Format("{0:0}", CountTimer);
        GameSetFlg = false;
        gameStratFlg = false;
    }
    #endregion

    #region  カウントダウン関数
    /// <summary>
    /// CountDownTimer関数
    /// 最初のカウントダウンの処理
    /// </summary>
    void CountDownTimer()
    {
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;

        // カウントが0.5以下になるとFIGHTが表示される
        if (CountDownTime < 0.5F)
        {
            TextCountDown.text = String.Format("FIGHT!");

            // 0.0秒以下になったらカウントダウンタイムを0.0で固定（止まったように見せる）
            if (CountDownTime < 0.0F)
            {
                CountDownTime = 0.0F;
                CountPanel.SetActive(false);

                // タイマー関数の呼び出し
                Timer();
            }
        }
        else
        {
            // カウントダウンタイムを整形して表示
            TextCountDown.text = String.Format("{0:0}", CountDownTime);
        }
    }
    #endregion

    #region  タイマー関数
    /// <summary>
    /// タイマー関数
    /// ゲーム中のタイマーの処理
    /// </summary>
    void Timer()
    {
        // 経過時刻を引いていく
        CountTimer -= Time.deltaTime;

        // カウントダウンタイムを整形して表示
        TextTimer.text = String.Format("{0:0}", CountTimer);

        // カウントが残り5秒になった時
        if(CountTimer < 5.5F)
        {
            TextTimer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        // カウントが0になった時
        if (CountTimer < 0.0F)
        {
            CountTimer = 0;
        }
    }
    #endregion

    #region  ゲームセットタイマー
    /// <summary>
    /// ゲームセットタイマー関数
    /// タイマーが0になったら呼ばれる
    /// </summary>
    public void GameSet()
    {
        // ゲームセットパネルを表示
        GameSetPanel.SetActive(true);

        // 経過時刻を引いていく
        GameSetTimer -= Time.deltaTime;
        
        // カウントが0になった時
        if (GameSetTimer <= 0.0F)
        {
            GameSetFlg = true;
            GameSetTimer = 0;
        }
    }
    #endregion

    #region  スタート関数
    // Start is called before the first frame update
    void Start()
    {
        // 初期化関数の呼び出し
        Init();
    }
    #endregion

    #region  アップデート関数
    // Update is called once per frame
    void Update()
    {
        if (StrixNetwork.instance.room.GetMemberCount() == 2)
        {
            gameStratFlg = true;
            CountDownTimer();
        }
        else if (StrixNetwork.instance.room.GetMemberCount() == 1)
        {
            if (gameStratFlg)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                TextCountDown.text = String.Format("PLEASE WAITING");
            }
        }
    }
    #endregion
}
