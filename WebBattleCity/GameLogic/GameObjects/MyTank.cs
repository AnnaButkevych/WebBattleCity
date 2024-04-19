using System;
using System.Threading.Tasks;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public class MyTank : Tank
{
    private int NumberOfShoots;
    public MyTank(int x, int y) : base(x, y)
    {
        NumberOfShoots = 8;
        Icon = 'ꥃ';
    }

    public override bool TryDestroy()
    {
        NumberOfShoots--;
        if (NumberOfShoots == 0)
        {
            IsDestroyed = true;
        }
        return IsDestroyed;
    }

   public override string GetIconName()
    {
        if (CurrentVector == Vector.Up)
        {
            return "myTankUp.jpg";
        }
        if (CurrentVector == Vector.Down)
        {
            return "myTankDown.jpg";
        }
        if (CurrentVector == Vector.Right)
        {
            return "myTankRight.jpg";
        }
        return "myTankLeft.jpg";
    } 
}
