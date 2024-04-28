using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class StoneWall : WallsBase
{
    private const int Hp = 3;

    public StoneWall(int x, int y) : base(x, y, Hp)
    {
    }

    public override string GetIconName()
    {
        return "stoneWall.jpg";
    }
}
