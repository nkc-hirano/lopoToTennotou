using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PopUpWindoLogic : MonoBehaviour
{
    public static PopUpWindoLogic inatance; // ���Ŏg����悤��

    [SerializeField]
    GameObject canvas;      // 
    [SerializeField]
    GameObject popUpWindo;    // ���ƂȂ�I�u�W�F�N�g
    GameObject createWindo;   // �쐬����I�u�W�F�N�g

    float speed = 0;        // �Q�[���I�u�W�F�N�g�̈ړ��ړ�

    // ���f�o�b�O�p��
    [SerializeField]
    float fadeTime = 0.5f; // �E�B���h�E��0.5f����

    public float FadeTime => fadeTime;

    private void Awake()
    {
        inatance = this;
    }
    public void CreatePopUpWindo() 
    {
        createWindo = Instantiate(popUpWindo, canvas.transform);
    }
    public void SetChild(GameObject children)
    {
        Instantiate(children, createWindo.transform);
    }

    public void PouUpSummonWind()
    {
        DOTween
          .To(() => speed = 0, (x) => speed = x, 1f, fadeTime - fadeTime * 0.1f)
          .SetUpdate(true)
          .OnUpdate(BackWind);
    }
    async public UniTask PouUpRepatriationWind()
    {
        await DOTween
          .To(() => speed, (x) => speed = x, 0, fadeTime / 2)
          .SetUpdate(true)
          .OnUpdate(BackWind)
          .ToUniTask();
        Debug.Log("�ړ��I��");
        Debug.Log("�폜");
        Destroy(createWindo);
    }
    private void BackWind()
    {
        Debug.Log("�ړ���");
        createWindo.transform.localScale = new Vector2(speed, speed);
    }
}
