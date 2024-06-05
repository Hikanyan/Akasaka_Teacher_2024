using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Factory
{
    public interface IGameObjectFactory
    {
        GameObject CreatePiece(int color);
        GameObject CreateBoard();
    }
}