using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControllerTest : MonoBehaviour
{
    [SerializeField]
    SoundNumber soundNum = (int)SoundNumber.sound1_Test;
    [SerializeField]
    AudioController_Base audioController;
    AudioSource source;

    // Å™ïKê{      í«â¡çÄñ⁄Å´
    [SerializeField]
    AudioMixerGroup group;
    [SerializeField]
    bool loopFlg;

    [SerializeField]
    float playbackTime;
    [SerializeField]
    bool crossFade;
    [SerializeField]
    CrossFadeData data;
    [SerializeField]
    float speed;

    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source = audioController.SetSource(source, (int)soundNum);
        //    source = audioController.SetSource(source,(int)soundNum,group,loopFlg);
    }
    void Update()
    {
        audioController.AudioPlay();
        //    audioController.AudioPlay(playbackTime,crossFade,data,speed);
        if (Input.GetKeyDown(KeyCode.Return))
            audioController.AudioStop();
    }
}