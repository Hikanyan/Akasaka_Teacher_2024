using System.IO;
using UnityEngine;
using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Task.Script.Othello.Model;

namespace HikanyanLaboratory.Task.Script.Othello.Services
{
    /// <summary>
    /// SaveとLoadを行う
    /// </summary>
    public abstract class PersistenceService : IPersistenceService
    {
        private const string SaveFilePath = "OthelloSaveData.json";

        public async UniTask SaveState(OthelloGameState gameState)
        {
            string json = JsonUtility.ToJson(gameState);
            using StreamWriter writer = new StreamWriter(SaveFilePath);
            await writer.WriteAsync(json);
        }

        public async UniTask<OthelloGameState> LoadState()
        {
            if (!File.Exists(SaveFilePath))
            {
                return null;
            }

            using StreamReader reader = new StreamReader(SaveFilePath);
            string json = await reader.ReadToEndAsync();
            return JsonUtility.FromJson<OthelloGameState>(json);
        }
    }
}