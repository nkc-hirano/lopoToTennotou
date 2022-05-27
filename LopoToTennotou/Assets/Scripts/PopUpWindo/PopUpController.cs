using Cysharp.Threading.Tasks;
using UnityEngine;

public class PopUpController : MonoBehaviour
{
    public static PopUpController instance;
    [SerializeField] GameObject[] prefabPopUpObj;   // ���ƂȂ�I�u�W�F�N�g
    [SerializeField] bool key;                      // ���͌��m�p

    [SerializeField] int number;                    // �I�u�W�F�N�g�ԍ�
    const int FRAMELATE = 60;                       // �t���[�����[�g��

    private void Awake()
    {
        instance = this;
    }
    public async void PopUpObjController(int number)
    {
        // �`�ʂ�����̂����
        using (var win = new RePopUpObjColl(prefabPopUpObj[number]))
        {
            // �ړ��ҋ@
            await UniTask.DelayFrame((int)(win.FadeTime * FRAMELATE));
            Debug.Log("���͎�t�J�n");
            // ���͑ҋ@
            await UniTask.WaitWhile(() => !key);
        }
    }

    public async void PopUpWindoController(int number)
    {
        // �`�ʂ�����̂����
        using (var win = new RePopUpWindoColl(prefabPopUpObj[number]))
        {
            // �ړ��ҋ@
            await UniTask.DelayFrame((int)(win.FadeTime * FRAMELATE));
            Debug.Log("���͎�t�J�n");
            // ���͑ҋ@
            await UniTask.WaitWhile(() => !key);
        }
    }
    public async void PopUpResultWindoController(int number)
    {
        // �`�ʂ�����̂����
        using (var win = new RePopUpResultColl(prefabPopUpObj[number]))
        {
            // �ړ��ҋ@
            await UniTask.DelayFrame((int)(win.FadeTime * FRAMELATE));
            Debug.Log("���͎�t�J�n");
            // ���͑ҋ@
            await UniTask.WaitWhile(() => !key);
        }
    }
    public async void PopUpStartStageNumberWindoController(int number)
    {
        // �`�ʂ�����̂����
        using (var win = new RePopUpStartStageNumberwindowColl(prefabPopUpObj[number]))
        {
            // �ړ��ҋ@
            await UniTask.DelayFrame((int)(win.FadeTime * FRAMELATE));
            Debug.Log("���͎�t�J�n");
            // ���͑ҋ@
            await UniTask.WaitWhile(() => !key);
        }
    }
    private void Update()
    {
        string keynum = Input.inputString;
        if (keynum.ToString() != "")
        {
            key = true;
            keynum = null;
        }
        else
        {
            key = false;
        }
    }
}