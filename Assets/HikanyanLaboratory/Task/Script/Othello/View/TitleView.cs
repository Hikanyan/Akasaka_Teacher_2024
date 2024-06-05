using HikanyanLaboratory.Task.Script.Othello.Scene;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace HikanyanLaboratory.Task.Script.Othello.View
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private ManagerSceneController _sceneController;

        [Inject]
        public void Construct(ManagerSceneController sceneController)
        {
            _sceneController = sceneController;
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnStartButtonClicked()
        {
            _sceneController.LoadGameScene();
        }
    }
}