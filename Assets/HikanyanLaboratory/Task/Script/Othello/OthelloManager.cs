using HikanyanLaboratory.Task.Othello;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class OthelloManager : IStartable
{
    private readonly GameObject _boardObject;
    private readonly GameObject _blackStone;
    private readonly GameObject _whiteStone;
    private GameObject _boardContainer;

    private readonly StateMachine _stateMachine;
    private readonly int _boardSize;

    // [Inject]
    // public OthelloManager(GameObject boardObject, GameObject blackStone, GameObject whiteStone, int boardSize = 8)
    // {
    //     _boardObject = boardObject;
    //     _blackStone = blackStone;
    //     _whiteStone = whiteStone;
    //     _boardSize = boardSize;
    // }

    public void Start()
    {
        _boardContainer = GameObject.Instantiate(new GameObject("BoardContainer"));
        if (_boardContainer == null)
        {
            Debug.LogError("Board container not found!");
            return;
        }

        BoardGeneration();
        Debug.Log("OthelloManager Start");
    }

    private void BoardGeneration()
    {
        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                GameObject board = GameObject.Instantiate(_boardObject, new Vector3(i, 0, j), Quaternion.identity);
                board.transform.parent = _boardContainer.transform;
            }
        }
    }

    public void StartGame()
    {
    }

    public void EndGame()
    {
    }
}