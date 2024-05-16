namespace WebBattleCity.GameLogic.GameObjects;

public class Base : GameObject
{
    private const int Hp = 1;
    public Base(int x, int y) : base(x, y, Hp)
    {
    }

   public override string GetIconName()
    {
        return "base.jpg";
    } 
}
