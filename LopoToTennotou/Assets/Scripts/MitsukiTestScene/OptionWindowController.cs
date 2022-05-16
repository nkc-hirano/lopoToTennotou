using UniRx;
using UnityEngine;
using UnityEngine.Audio;

public class OptionWindowController : MonoBehaviour
{
    public static OptionWindowController instance;

    [SerializeField] 
    GameObject selectObj;       // 選択中オブジェクト
    [SerializeField]
    GameObject optionWindow;    // 設定画面
    [SerializeField] 
    GameObject audioWindow;     // 音量設定画面

    [SerializeField]
    GameObject[] volumeObjBox = new GameObject[3];  // 音量設定のバー
    [SerializeField]
    AudioMixer mixer;                               // オーディオミキサー

    UIInputProvider uIInputProvider;            // 入力
    AudioLogic audioLogic = new AudioLogic();   // 音の処理
    Vector3 selectObjPos;                       // 選択オブジェクトの座標 

    int beforeSelectNum;        // 前の入力との差を検知する
    int beforeVolumeNum;        // 前の入力との差を検知する
    int selectNum = 2;          // 現在の選択番号
    bool audioFlg = true;       // オーディオ設定画面か

    int[] volumeBox = new int[3] { -2, -2, -2 };    // 音量パーセント
    public int[] puBox => volumeBox;                // 送信用?
    const int moveOffset = 100;                     // 移動範囲

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 初期画面
        AudioClause();
        // 入力スクリプトセット
        uIInputProvider = gameObject.AddComponent<UIInputProvider>();
        // 十字キー入力時のイベント
        uIInputProvider.crossMovementObservable.Subscribe(val => 
        {
            // 音量設定画面の場合、横による処理を行う
            if (audioFlg) { SelectAudioVolume(); }
            // 縦による処理
            SelectObjMove();
        });
        // 決定入力時のイベント
        uIInputProvider.DecisionButtonObservable.Subscribe(val => 
        {
            // 音量設定画面じゃない場合の処理
            if (!audioFlg) { SelectNumber(selectNum); }
            // 音量設定画面の場合の処理
            else 
            {
                AudioClause();                      // 表示切り替え
                audioLogic.mixer = mixer;           // ミキサー設定
                audioLogic.MixerVol(volumeBox);     // 音量設定 
            }
        });
    }

    // 選択された時の処理
    void SelectNumber(int num)
    {
        switch (num)
        {
            case 2:
                Destroy(gameObject);    // 選択されたらオブジェクト削除
                break;
            case 1:
                AudioClause();          // 表示切り替え
                break;
            case 0:
                Debug.Log("クレジット"); // デバッグ用
                break;
        }
    }

    // 上下処理
    void SelectObjMove() 
    {
        // 入力情報
        int inputNum = (int)uIInputProvider.crossMovementObservable.Value.y;
        // 前の情報より現在の情報の値が大きい場合
        if (inputNum > beforeSelectNum)
        {
            beforeSelectNum = inputNum;     // 今の情報を過去形にする
            inputNum = 1;                   // 入力情報を大きい値にする
        }
        // 前の情報より現在の情報の値が小さい場合
        else if (inputNum < beforeSelectNum)
        {
            beforeSelectNum = inputNum;     // 今の情報を過去形にする
            inputNum = -1;                  // 入力情報を小さい値にする
        }
        // その他、一緒の場合
        else { inputNum = 0; }              // 入力情報を0にする
        selectNum += inputNum;              // 入力情報を加算する

        selectNum = 2 < selectNum ? 2 : 0 > selectNum ? 0 : selectNum;  // 限界を越えないように調整
        selectObjPos = new Vector2(0, selectNum * moveOffset - 100);    // 座標設定
        selectObj.transform.localPosition = selectObjPos;               // 座標変更
    }

    // 横処理
    void SelectAudioVolume()
    {
        // 入力情報
        int inputNum = (int)uIInputProvider.crossMovementObservable.Value.x;
        // 前の情報より現在の情報の値が大きい場合
        if (inputNum > beforeVolumeNum)
        {
            beforeVolumeNum = inputNum;
            inputNum = 1;
        }
        // 前の情報より現在の情報の値が小さい場合
        else if (inputNum < beforeVolumeNum)
        {
            beforeVolumeNum = inputNum;
            inputNum = -1;
        }
        // 同じの場合
        else
        {
            beforeVolumeNum = inputNum;
            inputNum = 0;
        }
        // 入力された分加算し、調整、バーの処理をおこなう
        volumeBox[selectNum] += inputNum;
        volumeBox[selectNum] = 0 < volumeBox[selectNum] ? 0 : -10 > volumeBox[selectNum] ? -10 : volumeBox[selectNum];
        VolumeExpression();
    }

    // 音量のバーの長さ処理
    void VolumeExpression() 
    {
        // (一番上が2なので2引く選択番号で)
        // パーセントの十倍のサイズにする
        volumeObjBox[2 - selectNum].transform.localScale =
            new Vector3(10 * -volumeBox[selectNum], 1, 1);
    }
    
    // 設定画面の表示切り替え
    void AudioClause()
    {
        optionWindow.SetActive(audioFlg);   // 表示切り替え
        audioFlg = audioFlg ? false : true; // フラグ切り替え
        audioWindow.SetActive(audioFlg);    // 表示切り替え
        selectNum = 2;                      // 一番上に設定
        selectObjPos = new Vector2(0, selectNum * moveOffset - 100);    // 座標設定
        selectObj.transform.localPosition = selectObjPos;               // 座標変更
    }
}