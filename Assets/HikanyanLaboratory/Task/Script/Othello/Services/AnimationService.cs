using DG.Tweening;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Services
{
    public class AnimationService : IAnimationService
    {
        public void AnimatePiece(GameObject piece, Vector2 position)
        {
            // DOTweenを使用して駒を新しい位置にアニメーションで移動
            piece.transform.DOMove(new Vector3(position.x, piece.transform.position.y, position.y), 0.5f);
        }
    }
}