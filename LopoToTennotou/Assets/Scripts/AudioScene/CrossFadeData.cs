using UnityEngine.Audio;

public struct CrossFadeData
{
    public AudioMixer mixer;
    public AudioMixerSnapshot[] snapshots;
    public float[] weights;
}