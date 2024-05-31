﻿using UnityEngine;
using Cysharp.Threading.Tasks;
using VContainer;

namespace HikanyanLaboratory.Task.Script.Othello.Scene
{
    public class ManagerSceneController
    {
        private readonly SceneLoader _sceneLoader;

        public ManagerSceneController(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private async void Start()
        {
            await _sceneLoader.LoadSceneAsync("TitleScene");
        }

        public async void LoadGameScene()
        {
            await _sceneLoader.UnloadSceneAsync("TitleScene");
            await _sceneLoader.LoadSceneAsync("Othello");
        }

        public async void LoadResultScene()
        {
            await _sceneLoader.UnloadSceneAsync("Othello");
            await _sceneLoader.LoadSceneAsync("ResultScene");
        }

        public async void LoadTitleScene()
        {
            await _sceneLoader.UnloadSceneAsync("ResultScene");
            await _sceneLoader.LoadSceneAsync("TitleScene");
        }
    }
}