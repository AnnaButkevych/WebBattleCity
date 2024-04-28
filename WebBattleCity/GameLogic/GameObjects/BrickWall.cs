using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class BrickWall : WallsBase
{
    private const int Hp = 2;

    public BrickWall(int x, int y) : base(x, y, Hp)
    {
    }

    public override string GetIconName()
    {
        return "brickWall.jpg";
    }
}