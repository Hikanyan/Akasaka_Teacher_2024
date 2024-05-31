namespace HikanyanLaboratory.Task.Script.Othello.Model
{
    public interface IOthelloModel
    {
        OthelloGameState GetGameState();
        void SetGameState(OthelloGameState gameState);
    }
}