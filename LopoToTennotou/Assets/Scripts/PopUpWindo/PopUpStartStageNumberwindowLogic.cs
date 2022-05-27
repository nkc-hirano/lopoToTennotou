using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using GameCore;
using Zenject;

public class PopUpStartStageNumberwindowLogic : MonoBehaviour
{
    public static PopUpStartStageNumberwindowLogic inatance; // ���Ŏg����悤��

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject popUpStartStageNumberWindo;    // ���ƂȂ�I�u�W�F�N�g
    GameObject createStartStageNumberWindo;   // �쐬����I�u�W�F�N�g

    float speed = 0;        // �Q�[���I�u�W�F�N�g�̈ړ��ړ�

    float fadeTime = 1.5f; // �E�B���h�E��0.5f����

    public float FadeTime => fadeTime;

    [Inject]
    CoreStateController stateController;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        inatance = this;
    }
    public void CreatePopUpStartStageNumberWindo()
    {
        createStartStageNumberWindo = Instantiate(popUpStartStageNumberWindo, canvas.transform);
    }
    public void SetChild(GameObject children)
    {
        Instantiate(children, createStartStageNumberWindo.transform);
    }

    public void PouUpSummonWind()
    {
        DOTween
          .To(() => speed = 1200, (x) => speed = x, 0f, fadeTime)
          .SetUpdate(true)
          .OnUpdate(BackWind);
    }
    async public UniTask PouUpRepatriationWind()
    {
        await DOTween
          .To(() => speed, (x) => speed = x, -1200, fadeTime / 2)
          .SetUpdate(true)
          .OnUpdate(BackWind)
          .ToUniTask();
        Debug.Log("�ړ��I��");
        Debug.Log("�폜");
        Destroy(createStartStageNumberWindo);
    }
    private void BackWind()
    {
        Debug.Log("�ړ���");
        createStartStageNumberWindo.transform.localPosition = new Vector2(speed, 0);
    }
}