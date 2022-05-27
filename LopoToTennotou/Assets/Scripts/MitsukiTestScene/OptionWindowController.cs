using UniRx;
using UnityEngine;
using UnityEngine.Audio;

public class OptionWindowController : MonoBehaviour
{
    public static OptionWindowController instance;

    [SerializeField] 
    GameObject selectObj;       // �I�𒆃I�u�W�F�N�g
    [SerializeField]
    GameObject optionWindow;    // �ݒ���
    [SerializeField] 
    GameObject audioWindow;     // ���ʐݒ���

    [SerializeField]
    GameObject[] volumeObjBox = new GameObject[3];  // ���ʐݒ�̃o�[
    [SerializeField]
    AudioMixer mixer;                               // �I�[�f�B�I�~�L�T�[

    UIInputProvider uIInputProvider;            // ����
    AudioLogic audioLogic = new AudioLogic();   // ���̏���
    Vector3 selectObjPos;                       // �I���I�u�W�F�N�g�̍��W 

    int beforeSelectNum;        // �O�̓��͂Ƃ̍������m����
    int beforeVolumeNum;        // �O�̓��͂Ƃ̍������m����
    int selectNum = 2;          // ���݂̑I��ԍ�
    bool audioFlg = true;       // �I�[�f�B�I�ݒ��ʂ�

    int[] volumeBox = new int[3] { -2, -2, -2 };    // ���ʃp�[�Z���g
    public int[] puBox => volumeBox;                // ���M�p?
    const int moveOffset = 100;                     // �ړ��͈�

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 0;
        // �������
        AudioClause();
        // ���̓X�N���v�g�Z�b�g
        uIInputProvider = gameObject.AddComponent<UIInputProvider>();
        // �\���L�[���͎��̃C�x���g
        uIInputProvider.crossMovementObservable.Subscribe(val => 
        {
            // ���ʐݒ��ʂ̏ꍇ�A���ɂ�鏈�����s��
            if (audioFlg) { SelectAudioVolume(); }
            // �c�ɂ�鏈��
            SelectObjMove();
        });
        // ������͎��̃C�x���g
        uIInputProvider.DecisionButtonObservable.Subscribe(val => 
        {
            // ���ʐݒ��ʂ���Ȃ��ꍇ�̏���
            if (!audioFlg) { SelectNumber(selectNum); }
            // ���ʐݒ��ʂ̏ꍇ�̏���
            else 
            {
                AudioClause();                      // �\���؂�ւ�
                audioLogic.mixer = mixer;           // �~�L�T�[�ݒ�
                audioLogic.MixerVol(volumeBox);     // ���ʐݒ� 
            }
        });
    }

    // �I�����ꂽ���̏���
    void SelectNumber(int num)
    {
        switch (num)
        {
            case 2:
                Time.timeScale = 1;
                Destroy(gameObject);    // �I�����ꂽ��I�u�W�F�N�g�폜
                break;
            case 1:
                AudioClause();          // �\���؂�ւ�
                VolumeExpression();
                break;
            case 0:
                Debug.Log("�N���W�b�g"); // �f�o�b�O�p
                break;
        }
    }

    // �㉺����
    void SelectObjMove() 
    {
        // ���͏��
        int inputNum = (int)uIInputProvider.crossMovementObservable.Value.y;
        // �O�̏���茻�݂̏��̒l���傫���ꍇ
        if (inputNum > beforeSelectNum)
        {
            beforeSelectNum = inputNum;     // ���̏����ߋ��`�ɂ���
            inputNum = 1;                   // ���͏���傫���l�ɂ���
        }
        // �O�̏���茻�݂̏��̒l���������ꍇ
        else if (inputNum < beforeSelectNum)
        {
            beforeSelectNum = inputNum;     // ���̏����ߋ��`�ɂ���
            inputNum = -1;                  // ���͏����������l�ɂ���
        }
        // ���̑��A�ꏏ�̏ꍇ
        else { inputNum = 0; }              // ���͏���0�ɂ���
        selectNum += inputNum;              // ���͏������Z����

        selectNum = 2 < selectNum ? 2 : 0 > selectNum ? 0 : selectNum;  // ���E���z���Ȃ��悤�ɒ���
        selectObjPos = new Vector2(0, selectNum * moveOffset - 100);    // ���W�ݒ�
        selectObj.transform.localPosition = selectObjPos;               // ���W�ύX
    }

    // ������
    void SelectAudioVolume()
    {
        // ���͏��
        int inputNum = (int)uIInputProvider.crossMovementObservable.Value.x;
        // �O�̏���茻�݂̏��̒l���傫���ꍇ
        if (inputNum > beforeVolumeNum)
        {
            beforeVolumeNum = inputNum;
            inputNum = 1;
        }
        // �O�̏���茻�݂̏��̒l���������ꍇ
        else if (inputNum < beforeVolumeNum)
        {
            beforeVolumeNum = inputNum;
            inputNum = -1;
        }
        // �����̏ꍇ
        else
        {
            beforeVolumeNum = inputNum;
            inputNum = 0;
        }
        // ���͂��ꂽ�����Z���A�����A�o�[�̏����������Ȃ�
        volumeBox[selectNum] += inputNum;
        volumeBox[selectNum] = 0 < volumeBox[selectNum] ? 0 : -10 > volumeBox[selectNum] ? -10 : volumeBox[selectNum];
        VolumeExpression();
    }

    // ���ʂ̃o�[�̒�������
    void VolumeExpression() 
    {
        // (��ԏオ2�Ȃ̂�2�����I��ԍ���)
        // �p�[�Z���g�̏\�{�̃T�C�Y�ɂ���
        volumeObjBox[2 - selectNum].transform.localScale =
            new Vector3(10 * -volumeBox[selectNum], 1, 1);
    }
    
    // �ݒ��ʂ̕\���؂�ւ�
    void AudioClause()
    {
        optionWindow.SetActive(audioFlg);   // �\���؂�ւ�
        audioFlg = audioFlg ? false : true; // �t���O�؂�ւ�
        audioWindow.SetActive(audioFlg);    // �\���؂�ւ�
        selectNum = 2;                      // ��ԏ�ɐݒ�
        selectObjPos = new Vector2(0, selectNum * moveOffset - 100);    // ���W�ݒ�
        selectObj.transform.localPosition = selectObjPos;               // ���W�ύX
    }
}