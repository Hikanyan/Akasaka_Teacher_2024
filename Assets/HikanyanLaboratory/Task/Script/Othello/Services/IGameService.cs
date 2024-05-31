namespace HikanyanLaboratory.Task.Script.Othello.Services
{
    public interface IGameService
    {
        void StartGame();
        void EndGame();
        void MakeMove(int x, int y);
    }
}