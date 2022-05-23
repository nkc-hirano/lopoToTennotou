using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace SceneController
{
    public class TitleSceneStateUpdater : SceneStateUpdaterBase
    {
        [SerializeField]
        private FadeFaçade fadeFaçade;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        public override void LoadNextScene()
        {
            FadeOutAsync(tokenSource.Token).Forget();
        }

        async public UniTask FadeOutAsync(CancellationToken token)
        {
            float waitSecondTime = 1.01f;
            // ここでフェードアウトを呼び出しているのでIntroductionSceneでFadeInメソッドを必ず呼ぶ
            fadeFaçade.FadeOut(waitSecondTime, true);
            await UniTask.Delay((int)waitSecondTime * 1000);
            token.ThrowIfCancellationRequested();
            controller.SceneTypeUpdateObserver.OnNext(SceneStateType.Introduction);
        }
    }
}