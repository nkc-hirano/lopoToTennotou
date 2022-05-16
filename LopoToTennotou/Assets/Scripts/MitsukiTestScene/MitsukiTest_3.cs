using UnityEngine;

public class MitsukiTest_3 : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}