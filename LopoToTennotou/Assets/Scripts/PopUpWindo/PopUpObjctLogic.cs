using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PopUpObjctLogic : MonoBehaviour
{
    public static PopUpObjctLogic inatance; // ���Ŏg����悤��

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject popUpObj;
    GameObject createObj;

    float speed = 0;        // �Q�[���I�u�W�F�N�g�̈ړ��ړ�

    // ���f�o�b�O�p��
    [SerializeField]
    float fadeTime = 2.5f; // �|�b�v�A�b�v��2.5f�ȉ�����

    public float FadeTime => fadeTime;

    private void Awake()
    {
        inatance = this;
    }

    public void CreatePopUpObj() 
    {
        createObj = Instantiate(popUpObj, canvas.transform);
    }
    public void SetChild(GameObject children) 
    {
        GameObject obj = Instantiate(children, createObj.transform);
        obj.transform.localPosition = new Vector3(470, 205);
    }
    public void PouUpSummonMove()
    {
        DOTween
          .To(() => speed = 350, (x) => speed = x, 0f, fadeTime)
          .SetUpdate(true)
          .OnUpdate(BackMove);
    }
    async public UniTask PouUpRepatriationMove()
    {
        await DOTween
          .To(() => speed, (x) => speed = x, 350f, fadeTime/2)
          .SetUpdate(true)
          .OnUpdate(BackMove)
          .ToUniTask();
        Debug.Log("�ړ��I��");
        Debug.Log("�폜");
        Destroy(createObj);
    }
    private void BackMove()
    {
        Debug.Log("�ړ���");
        createObj.transform.localPosition = new Vector2(speed, 0);
    }
}