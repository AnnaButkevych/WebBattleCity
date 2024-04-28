using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class WoodsWall : WallsBase
{
    private const int Hp = 1;

    public WoodsWall(int x, int y) : base(x, y, Hp)
    {
    }

    public override string GetIconName()
    {
        return "woodsWall.jpg";
    }
}