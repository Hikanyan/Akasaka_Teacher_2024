using Cysharp.Threading.Tasks;
using HikanyanLaboratory.Task.Script.Othello.Model;

namespace HikanyanLaboratory.Task.Script.Othello.Services
{
    public interface IPersistenceService
    {
        UniTask SaveState(OthelloGameState gameState);
        UniTask<OthelloGameState> LoadState();
    }
}