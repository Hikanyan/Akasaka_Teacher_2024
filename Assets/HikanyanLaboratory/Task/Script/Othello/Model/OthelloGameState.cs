using System.Collections.Generic;
using UnityEngine;

namespace HikanyanLaboratory.Task.Script.Othello.Model
{
    public class OthelloGameState
    {
        public List<GameObject> Pieces { get; set; }
        public Player CurrentPlayer { get; set; }
    }
}