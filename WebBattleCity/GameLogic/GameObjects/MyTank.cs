using System;
using System.Threading.Tasks;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public class MyTank : Tank
{
    private const int Hp = 8;
    public MyTank(int x, int y) : base(x, y, Hp)
    {
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
