using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class SceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!loadOperation.isDone)
            {
                await UniTask.Yield();
            }

            // 新しいシーンをアクティブに設定
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        public async UniTask UnloadSceneAsync(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                var unloadOperation = SceneManager.UnloadSceneAsync(sceneName);
                while (!unloadOperation.isDone)
                {
                    await UniTask.Yield();
                }
            }
        }
    }
}