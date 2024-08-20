using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_MouseClick1 : MonoBehaviour
{
    GameObject strixConnect;
    StrixConnectGUI1 strixConnectGUI;

    #region  クリック関数
    /// <summary>
    /// クリック関数
    /// マウスでクリックされたときに呼び出される
    /// </summary>
    public void MouseClick()
    {
        //int rand = Random.Range(1, 4);
        int rand = 1;

        switch (rand)
        {
            case 1:
                SceneManager.LoadScene("Stage1");
                break;

            case 2:
                SceneManager.LoadScene("Stage2");
                break;

            case 3:
                SceneManager.LoadScene("Stage3");
                break;
        }
    }

    public void MouseClick2()
    {
        SceneManager.LoadScene("StrixSettingsScene");
    }
    #endregion

    private void Start()
    {
        strixConnect = GameObject.Find("ClickEvent1");
        //strixConnect = GameObject.Find("ClickEvent2");
        strixConnectGUI = strixConnect.GetComponent<StrixConnectGUI1>();
    }

    #region  アップデート関数
    /// <summary>
    /// アップデート関数
    /// フレームごとに呼ばれる
    /// </summary>
    private void Update()
    {
        // マウスの左クリックが押されたときの処理
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            strixConnectGUI.Connect();

            //MouseClick();
        }
    }
    #endregion
}
