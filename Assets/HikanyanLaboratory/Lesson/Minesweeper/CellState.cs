namespace HikanyanLaboratory.Lesson.Minesweeper
{
    public enum CellState
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,

        Mine = -1, // 地雷
    }

    public enum CellVisibility
    {
        Secret, // 隠し
        Revealed, // 開示済み
        Flagged, // 旗立て済み
    }
}