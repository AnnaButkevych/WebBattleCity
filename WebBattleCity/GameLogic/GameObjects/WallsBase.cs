using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class WallsBase : GameObject
{
    public WallsBase(int x, int y, int hp) : base(x, y, hp)
    {
        IsDestroyed = false;
    }
}
