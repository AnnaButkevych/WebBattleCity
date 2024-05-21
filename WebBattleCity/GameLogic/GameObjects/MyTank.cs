using System;
using System.Threading.Tasks;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public class MyTank : Tank
{
    private const int Hp = 8;
    public string Id;
    
    public MyTank(int x, int y, string id="") : base(x, y, Hp)
    {
        Id = id;
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
