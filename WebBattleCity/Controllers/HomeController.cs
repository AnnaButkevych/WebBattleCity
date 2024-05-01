using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebBattleCity.GameLogic;
using WebBattleCity.GameLogic.GameLogicEnums;
using WebBattleCity.GameLogic.GameObjects;
using WebBattleCity.Models;

namespace WebBattleCity.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private GameProcess _gameProcess;
    private string LevelName;

    public HomeController(ILogger<HomeController> logger, GameProcess gameProcess)
    {
        _logger = logger;
        _gameProcess = gameProcess;
    }

    public IActionResult Index(int keyCode = 1)
    {
        if (TempData["levelName"] != null)
        {
            ChangeLevel(TempData["levelName"] as string);
        }

        GameObject[,] gameObjects = null;

        switch (keyCode)
        {
            case 1:
                gameObjects = _gameProcess.Process(ControlsKeysEnum.None);
                break;
            case 37:
                gameObjects = _gameProcess.Process(ControlsKeysEnum.LeftArrow);
                break;
            case 38:
                gameObjects = _gameProcess.Process(ControlsKeysEnum.UpArrow);
                break;
            case 39:
                gameObjects = _gameProcess.Process(ControlsKeysEnum.RightArrow);
                break;
            case 40:
                gameObjects = _gameProcess.Process(ControlsKeysEnum.DownArrow);
                break;
            case 32:
                gameObjects = _gameProcess.Process(ControlsKeysEnum.SpaceBar);
                break;
            default:
                gameObjects = _gameProcess.GetCurrentState();
                break;
        }

        GameBoardViewModel gameBoardViewModel = new GameBoardViewModel();
        int rows = gameObjects.GetLength(0);
        int cols = gameObjects.GetLength(1);

        gameBoardViewModel.Matrix = new string[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                gameBoardViewModel.Matrix[j, i] = gameObjects[i, j].GetIconName();
            }
        }

        if (_gameProcess.IsLoser())
        {
            GameBoardViewModel isLoserViewModel = new GameBoardViewModel{ Matrix = new string[1, 1] };
            isLoserViewModel.Matrix[0, 0] = "isLoser.jpg"; 
            return View(isLoserViewModel);

        }

        if (_gameProcess.IsWinner())
        {
            GameBoardViewModel isWinnerViewModel = new GameBoardViewModel { Matrix = new string[1, 1] };
            isWinnerViewModel.Matrix[0, 0] = "isWinner.jpg";
            return View(isWinnerViewModel);
        }

        return View(gameBoardViewModel);
    }

    public IActionResult Rules()
    {
        return View();
    }

    public IActionResult Menu()
    {
        LevelsViewModel levelsViewModel = new LevelsViewModel();
        levelsViewModel.MenuPoints = new string[] { "NewGame", "Level1", "Level2", "Level3" };

        return View(levelsViewModel);
    }

    public IActionResult ChangeLevelRedirect(string levelName)
    {
        TempData["levelName"] = levelName;
        return RedirectToAction("Index");
    }

    private void ChangeLevel(string levelName)
    {
        BattleField battleField = null;
        if (levelName == "NewGame")
        {
            battleField = new BattleField();
        }
        else
        {
            battleField = new BattleField(levelName);
        }
        _gameProcess.BattleField = battleField;

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

