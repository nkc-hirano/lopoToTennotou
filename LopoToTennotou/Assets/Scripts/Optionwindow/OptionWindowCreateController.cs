using UniRx;
using UnityEngine;

public class OptionWindowCreateController : MonoBehaviour
{
    [SerializeField] GameObject window;
    [SerializeField] GameObject canvas;

    UIInputProvider uIInputProvider;

    void Start()
    {
        uIInputProvider = gameObject.AddComponent<UIInputProvider>();
        uIInputProvider.SelectButtonObservable.Subscribe(val =>
        {
            Instantiate(window, canvas.transform);
        });
    }
}