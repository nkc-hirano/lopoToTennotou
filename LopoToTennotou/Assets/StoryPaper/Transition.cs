using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private Material _transitionIn;

    void Start()
    {
        GetComponent<Image>().material = _transitionIn;
        _transitionIn.SetFloat("_Flip", 1);
    }

    public void TransitionStart()
    {
        StartCoroutine(BeginTransition());
    }

    IEnumerator BeginTransition()
    {
        yield return Animate(_transitionIn, 1.1f);
    }

    /// <summary>
    /// time秒かけてトランジションを行う
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Animate(Material material, float time)
    {
        GetComponent<Image>().material = material;
        float current = 1;
        while (current < time)
        {
            material.SetFloat("_Flip", current / time);
            yield return new WaitForEndOfFrame();
            current -= Time.deltaTime;
        }
        material.SetFloat("_Flip", 1);
    }
}