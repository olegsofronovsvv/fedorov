using GameConcept.Contracts;

namespace GameConcept.World;

internal sealed class LocalMap : IMovable
{
    public LocalMap(int[,] cells)
    {
        Cells = cells;
    }

    public int[,] Cells { get; }
    public int PlayerX { get; private set; }
    public int PlayerY { get; private set; }
    public int CurrentCell => Cells[PlayerY, PlayerX];

    public bool Move(int dx, int dy)
    {
        int newX = PlayerX + dx;
        int newY = PlayerY + dy;
        if (newX < 0 || newY < 0 || newY >= Cells.GetLength(0) || newX >= Cells.GetLength(1))
        {
            return false;
        }

        PlayerX = newX;
        PlayerY = newY;
        return true;
    }

    public void ClearCurrentCell()
    {
        Cells[PlayerY, PlayerX] = 1;
    }

    public void Draw()
    {
        for (int y = 0; y < Cells.GetLength(0); y++)
        {
            for (int x = 0; x < Cells.GetLength(1); x++)
            {
                Console.Write(PlayerX == x && PlayerY == y ? "0 " : Cells[y, x] + " ");
            }

            Console.WriteLine();
        }
    }
}
