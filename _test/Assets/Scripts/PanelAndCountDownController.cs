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
    #region  �v���C�x�[�g
    [SerializeField] GameObject CountPanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject GameSetPanel;
    private Color color;
    #endregion

    #region  �p�u���b�N
    [SerializeField] public float CountDownTime =  5;
    [SerializeField] public float CountTimer    = 60;
    [SerializeField] public float GameSetTimer  =  2;
    public TextMeshProUGUI TextCountDown;  // �\���p�e�L�X�gUI
    public Text TextTimer;                 // �\���p�e�L�X�gUI
    public bool GameSetFlg;
    private bool gameStratFlg;
    #endregion

    #region  Init�֐�
    /// <summary>
    /// Init�֐�
    /// �ϐ��̏�������A�ŏ��ɏ������������̂�����
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

    #region  �J�E���g�_�E���֐�
    /// <summary>
    /// CountDownTimer�֐�
    /// �ŏ��̃J�E���g�_�E���̏���
    /// </summary>
    void CountDownTimer()
    {
        // �o�ߎ����������Ă���
        CountDownTime -= Time.deltaTime;

        // �J�E���g��0.5�ȉ��ɂȂ��FIGHT���\�������
        if (CountDownTime < 0.5F)
        {
            TextCountDown.text = String.Format("FIGHT!");

            // 0.0�b�ȉ��ɂȂ�����J�E���g�_�E���^�C����0.0�ŌŒ�i�~�܂����悤�Ɍ�����j
            if (CountDownTime < 0.0F)
            {
                CountDownTime = 0.0F;
                CountPanel.SetActive(false);

                // �^�C�}�[�֐��̌Ăяo��
                Timer();
            }
        }
        else
        {
            // �J�E���g�_�E���^�C���𐮌`���ĕ\��
            TextCountDown.text = String.Format("{0:0}", CountDownTime);
        }
    }
    #endregion

    #region  �^�C�}�[�֐�
    /// <summary>
    /// �^�C�}�[�֐�
    /// �Q�[�����̃^�C�}�[�̏���
    /// </summary>
    void Timer()
    {
        // �o�ߎ����������Ă���
        CountTimer -= Time.deltaTime;

        // �J�E���g�_�E���^�C���𐮌`���ĕ\��
        TextTimer.text = String.Format("{0:0}", CountTimer);

        // �J�E���g���c��5�b�ɂȂ�����
        if(CountTimer < 5.5F)
        {
            TextTimer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        // �J�E���g��0�ɂȂ�����
        if (CountTimer < 0.0F)
        {
            CountTimer = 0;
        }
    }
    #endregion

    #region  �Q�[���Z�b�g�^�C�}�[
    /// <summary>
    /// �Q�[���Z�b�g�^�C�}�[�֐�
    /// �^�C�}�[��0�ɂȂ�����Ă΂��
    /// </summary>
    public void GameSet()
    {
        // �Q�[���Z�b�g�p�l����\��
        GameSetPanel.SetActive(true);

        // �o�ߎ����������Ă���
        GameSetTimer -= Time.deltaTime;
        
        // �J�E���g��0�ɂȂ�����
        if (GameSetTimer <= 0.0F)
        {
            GameSetFlg = true;
            GameSetTimer = 0;
        }
    }
    #endregion

    #region  �X�^�[�g�֐�
    // Start is called before the first frame update
    void Start()
    {
        // �������֐��̌Ăяo��
        Init();
    }
    #endregion

    #region  �A�b�v�f�[�g�֐�
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
