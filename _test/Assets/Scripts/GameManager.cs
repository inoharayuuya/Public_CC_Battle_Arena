using SoftGear.Strix.Unity.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region  �v���C�x�[�g
    GameObject Timer;
    PanelAndCountDownController panelController;
    GameObject playerClass;
    PlayerClass player;
    #endregion

    #region  �p�u���b�N
    public bool GameEndFlg;
    #endregion

    #region  Init�֐�
    /// <summary>
    /// Init�֐�
    /// �ϐ��̏�������A�ŏ��ɂ���������������
    /// </summary>
    void Init()
    {
        Timer = GameObject.Find("PanelAndCountDownManager");
        panelController = Timer.GetComponent<PanelAndCountDownController>();
        playerClass = GameObject.Find("PlayerClass");
        player = playerClass.GetComponent<PlayerClass>();
        GameEndFlg = false;
    }
    #endregion

    void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }

    void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }

    #region  �X�^�[�g�֐�
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    #endregion

    #region  �A�b�v�f�[�g�֐�
    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[��0�ɂȂ�����I��
        if(panelController.CountTimer == 0.0F)
        {
            GameEndFlg = true;
        }

        if(GameEndFlg == true)
        {
            panelController.GameSet();
            Debug.Log("�I��");
        }

        if(player.g_p1_hp <= 0)
        {
            Invoke("LoadLoseScene", 0.25f);
        }

        if(player.g_p2_hp <= 0)
        {
            Invoke("LoadWinScene", 0.25f);
        }

        if (panelController.GameSetFlg == true)
        {
            StrixNetwork.instance.roomSession.Disconnect();

            SceneManager.LoadScene("StrixSettingsScene");
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            StrixNetwork.instance.roomSession.Disconnect();
            
            SceneManager.LoadScene("StrixSettingsScene");
        }
    }
    #endregion
}
