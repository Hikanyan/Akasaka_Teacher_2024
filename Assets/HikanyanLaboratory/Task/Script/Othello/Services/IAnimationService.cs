using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Services
{
    public interface IAnimationService
    {
        void AnimatePiece(GameObject piece, Vector2 position);
    }
}