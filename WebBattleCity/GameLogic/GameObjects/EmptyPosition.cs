using System;
namespace WebBattleCity.GameLogic.GameObjects;

public class EmptyPosition : GameObject
{
    public EmptyPosition(int x, int y, char icon = ' ') : base(x, y)
    {
        Icon = icon;
    }

    public override string GetIconName()
    {
        return "emptyPosition.jpg";
    }
}