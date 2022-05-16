using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUseSound : MonoBehaviour
{
    // ���̐ݒ�
    [SerializeField] AudioData data;
    AudioLogic logic = new AudioLogic();
    void Start()
    {
        data.audioSource = gameObject.AddComponent<AudioSource>();
        data.audioSource = logic.SetSource(data);
    }
}
