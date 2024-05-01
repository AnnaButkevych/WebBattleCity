using System;
using System.IO.Pipelines;
using WebBattleCity.GameLogic.GameLogicEnums;

namespace WebBattleCity.GameLogic.GameObjects;

public class BattleField
{
    public int Length = 10;
    public int Height = 10;
    public GameObject[,] State;
    public int Indentation = 3;
    public MyTank MyTankProperty;
    public List<EnemyTank> enemyTanksProperty = new();


    public BattleField(string fileName)
    {
        State = new GameObject[Length, Height];
        InitialisePositions(fileName);
    }

    public BattleField()
    {
        State = new GameObject[Length, Height];
        InitialisePositions();
    }

    public void UpdateField(List<Projectile> projectiles)
    {

        for (int i = 0; i < Length; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (State[j, i] is Projectile)
                {
                    Projectile projectile = (Projectile)State[j, i];
                        switch (projectile.Vector)
                        {
                            case Vector.Left:
                            if (j > 0)
                            {
                                if (State[j - 1, i] is EmptyPosition)
                                {
                                    State[j - 1, i] = new Projectile(j - 1, i, projectile.Vector, State[j, i] as Tank);
                                    State[j, i] = new EmptyPosition(j, i);
                                }
                                else
                                {
                                    if (State[j - 1, i].TryDestroy(State[j, i] as Projectile))
                                    {
                                        State[j - 1, i] = new EmptyPosition(j - 1, i);
                                    }
                                }
                            }
                            if (j == 0)
                            {
                                State[j, i] = new EmptyPosition(j, i);
                            }
                                break;
                            case Vector.Right:
                            if (j < Length - 1)
                            {
                                if (State[j + 1, i] is EmptyPosition)
                                {
                                    State[j + 1, i] = new Projectile(j + 1, i, projectile.Vector, State[j, i] as Tank);
                                    State[j, i] = new EmptyPosition(j, i);
                                }
                                else
                                {
                                    if (State[j + 1, i].TryDestroy(State[j, i] as Projectile))
                                    {
                                        State[j + 1, i] = new EmptyPosition(j + 1, i);
                                    }
                                }
                            }
                            if (j == Length - 1)
                            {
                                if ((State[j, i] as Projectile).IsOut)
                                {
                                    State[j, i] = new EmptyPosition(j, i);

                                }
                                else
                                {
                                    (State[j, i] as Projectile).IsOut = true;

                                }
                            }
                            break;
                            case Vector.Up:
                            if (i > 0)
                            {
                                if (State[j, i - 1] is EmptyPosition)
                                {
                                    State[j, i - 1] = new Projectile(j, i - 1, projectile.Vector, State[j, i] as Tank);
                                    State[j, i] = new EmptyPosition(j, i);
                                }
                                else
                                {
                                    if (State[j, i - 1].TryDestroy(State[j, i] as Projectile))
                                    {
                                        State[j, i - 1] = new EmptyPosition(j, i - 1);
                                    }
                                }
                            }
                            if (i == 0)
                            {
                                State[j, i] = new EmptyPosition(j, i);
                            }
                            break;
                            case Vector.Down:
                            if (i < Length - 1)
                            {
                                if (State[j, i + 1] is EmptyPosition)
                                {
                                    State[j, i + 1] = new Projectile(j, i + 1, projectile.Vector, State[j, i] as Tank);
                                    State[j, i] = new EmptyPosition(j, i);
                                }
                                else
                                {
                                    if (State[j, i + 1].TryDestroy(State[j, i] as Projectile))
                                    {
                                        State[j, i + 1] = new EmptyPosition(j, i + 1);
                                    }
                                }

                            }
                            if (i == Length - 1)
                            {
                                if ((State[j, i] as Projectile).IsOut)
                                {
                                    State[j, i] = new EmptyPosition(j, i);

                                }
                                else
                                {
                                    (State[j, i] as Projectile).IsOut = true;

                                }
                            }
                            break;
                            default: throw new Exception();
                        }
                }

                if (State[j, i] is Tank)
                {
                    Tank mytank = (Tank)State[j, i];
                    if (State[mytank.X, mytank.Y] is EmptyPosition)
                    {
                        State[mytank.X, mytank.Y] = State[j, i];
                        if (j != mytank.X || i != mytank.Y)
                        {
                            State[j, i] = new EmptyPosition(j, i);
                        }
                    }
                    if (State[mytank.X, mytank.Y] is not EmptyPosition && !(State[mytank.X, mytank.Y] is Tank))
                    {
                        switch ((State[j, i] as Tank).CurrentVector)
                        {
                            case Vector.Left:
                                State[j, i].X++;
                                break;
                            case Vector.Right:
                                State[j, i].X--;
                                break;
                            case Vector.Up:
                                State[j, i].Y++;
                                break;
                            case Vector.Down:
                                State[j, i].Y--;
                                break;
                        }
                    }

                }

            }
        }

        if (projectiles.Count > 0)
        {
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.X <= Length && projectile.X >= 0 && projectile.Y <= Length && projectile.Y >= 0)
                {
                    if (State[projectile.X, projectile.Y] is EmptyPosition)
                    {
                        State[projectile.X, projectile.Y] = projectile;
                    }
                    else
                    {
                        if (State[projectile.X, projectile.Y].TryDestroy(projectile))
                        {
                            State[projectile.X, projectile.Y] = new EmptyPosition(projectile.X, projectile.Y);
                        }

                    }
                }
            }
        }
    }

    private void InitialisePositions(string fileName = "BattleFieldMatrix")
    {
        string fileState = FileReader.ReadFile($"GameLogic/{fileName}.txt");
        for (int i = 0; i < fileState.Length; i = i + Indentation)
        {
            int x = (i / Indentation) % 10;
            int y = i / (Length * Indentation);


            PositionsEnum obj = (PositionsEnum)Enum.Parse(typeof(PositionsEnum), fileState[i].ToString());
            switch (obj)
            {
                case PositionsEnum.BrickWall:
                    State[x, y] = new BrickWall(x, y);
                    break;
                case PositionsEnum.StoneWall:
                    State[x, y] = new StoneWall(x, y);
                    break;
                case PositionsEnum.WoodsWall:
                    State[x, y] = new WoodsWall(x, y);
                    break;
                case PositionsEnum.Empty:
                    State[x, y] = new EmptyPosition(x, y);
                    break;
                case PositionsEnum.EnemyTank:
                    State[x, y] = new EnemyTank(x, y);
                    enemyTanksProperty.Add((EnemyTank)State[x, y]);
                    break;
                case PositionsEnum.MyTank:
                    State[x, y] = new MyTank(x, y);
                    MyTankProperty = (MyTank)State[x, y];
                    break;
            }

        }
    }
}

