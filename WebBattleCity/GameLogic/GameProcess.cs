using System;
using WebBattleCity.GameLogic.GameObjects;
namespace WebBattleCity.GameLogic;

public class GameProcess {
public void Process(){
        BattleField battleField = new BattleField();
        battleField.DrawFrame();


        List<EnemyTank> enemyTanks = battleField.enemyTanksProperty;

        MyTank leopard = battleField.MyTankProperty;
        Projectile myTankProjectile;

        while (true)
        {
            if (leopard.IsDestroyed)
            {
                Console.WriteLine("!GAME OVER!");
                break;
            }
            if (enemyTanks.All(x => x.IsDestroyed))
            {
                Console.WriteLine("!WINNER!");
                break;
            }
            ConsoleKey currentInput = Console.ReadKey(true).Key;
            Console.Clear();
            List<Projectile> projectiles = new List<Projectile>();
            switch (currentInput)
            {
                case ConsoleKey.UpArrow:
                    leopard.TurnAndMove(GameLogicEnums.Vector.Up, battleField);
                    break;
                case ConsoleKey.DownArrow:
                    leopard.TurnAndMove(GameLogicEnums.Vector.Down, battleField);
                    break;
                case ConsoleKey.LeftArrow:
                    leopard.TurnAndMove(GameLogicEnums.Vector.Left, battleField);
                    break;
                case ConsoleKey.RightArrow:
                    leopard.TurnAndMove(GameLogicEnums.Vector.Right, battleField);
                    break;
                case ConsoleKey.Spacebar:
                    projectiles.Add(leopard.Fire());
                    break;
            }
            foreach (var enemyTank in enemyTanks)
            {
                if (enemyTank.IsOnTheSameLine(leopard))
                {
                    GameLogicEnums.Vector vectorToShoot = enemyTank.FindVectorToShoot(leopard);
                    projectiles.Add(enemyTank.FireToVector(vectorToShoot));
                }
                enemyTank.MoveEnemyTank(battleField);
            }

            battleField.UpdateField(projectiles);
            battleField.DrawFrame();
        }

    }
}

