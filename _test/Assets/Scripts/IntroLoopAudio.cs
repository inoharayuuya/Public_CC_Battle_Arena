using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoopAudio : MonoBehaviour
{
    // �C���g���A���[�v�pBGM
    [SerializeField] AudioSource IntroAudioSource;
    [SerializeField] AudioSource LoopAudioSource;

    // �C���g�����烋�[�v�Đ��ɂȂ��Ƃ��̂�����Ȃ����p
    public float startLoopSound;

    // �eBGM����x�����Đ�����p�̃J�E���g�ϐ�
    int soundCnt;

    // Start is called before the first frame update
    void Start()
    {
        // �eBGM����x�����Đ�����p�̃J�E���g�ϐ�
        soundCnt = 0;

        // �f�o�b�O�p
        //IntroAudioSource.time = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        // �C���g���𗬂�
        if (soundCnt == 0)
        {
            IntroAudioSource.Play();

            soundCnt = 1;
        }

        // �C���g�����I������烋�[�v�p�𗬂�
        if(soundCnt == 1 && IntroAudioSource.time >= startLoopSound)
        {
            LoopAudioSource.Play();

            soundCnt = 2;
        }

    }
}
