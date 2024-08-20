using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoopAudio : MonoBehaviour
{
    // イントロ、ループ用BGM
    [SerializeField] AudioSource IntroAudioSource;
    [SerializeField] AudioSource LoopAudioSource;

    // イントロからループ再生につなぐときのずれをなくす用
    public float startLoopSound;

    // 各BGMを一度だけ再生する用のカウント変数
    int soundCnt;

    // Start is called before the first frame update
    void Start()
    {
        // 各BGMを一度だけ再生する用のカウント変数
        soundCnt = 0;

        // デバッグ用
        //IntroAudioSource.time = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        // イントロを流す
        if (soundCnt == 0)
        {
            IntroAudioSource.Play();

            soundCnt = 1;
        }

        // イントロが終わったらループ用を流す
        if(soundCnt == 1 && IntroAudioSource.time >= startLoopSound)
        {
            LoopAudioSource.Play();

            soundCnt = 2;
        }

    }
}
