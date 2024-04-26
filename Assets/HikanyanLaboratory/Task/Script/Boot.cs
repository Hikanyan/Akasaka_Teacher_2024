using UnityEngine;
using UnityEngine.SceneManagement;

namespace HikanyanLaboratory.Task.Othello
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private string _loadScene;

        public void Start()
        {
            //OhelloSceneを読み込む
            SceneManager.LoadScene(_loadScene);
            Debug.Log("OthelloSceneを読み込みました");
        }
    }
}