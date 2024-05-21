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
    private readonly GameProcess _gameProcess;
    private string LevelName;
    private string folderPath = "GameLogic/";

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

        GameObject[,] gameObjects = keyCode switch
        {
            1 => _gameProcess.Process(ControlsKeysEnum.None),
            37 => _gameProcess.Process(ControlsKeysEnum.LeftArrow),
            38 => _gameProcess.Process(ControlsKeysEnum.UpArrow),
            39 => _gameProcess.Process(ControlsKeysEnum.RightArrow),
            40 => _gameProcess.Process(ControlsKeysEnum.DownArrow),
            32 => _gameProcess.Process(ControlsKeysEnum.SpaceBar),
            _ => _gameProcess.GetCurrentState()
        };

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
        string[] txtFiles = Directory.GetFiles(folderPath, "*.txt");

        string[] fileNames = new string[txtFiles.Length];
        for (int i = 0; i < txtFiles.Length; i++)
        {
            fileNames[i] = Path.GetFileNameWithoutExtension(txtFiles[i]);
        }

        levelsViewModel.MenuPoints = fileNames;

        return View(levelsViewModel);
    }

    public IActionResult LevelEditor(string? levelName, bool isNewLevel = false)
    {
        string fileName;

        if (isNewLevel)
        {
            fileName = levelName;
            levelName = "BattleFieldMatrix";
        }
        else
        {
            if (levelName == "NewGame" || levelName == null)
            {
                levelName = "BattleFieldMatrix";
            } 
            fileName = levelName;
        }

        string fileState = FileReader.ReadFile($"GameLogic/{levelName}.txt");

        string[] lines = fileState.Split(new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.RemoveEmptyEntries);
        
        int rows = lines.Length;
        int cols = lines[0].Split(',').Length-1;

        int[,] numbersArray = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            string[] values = lines[i].TrimEnd(',').Split(',');

            for (int j = 0; j < cols; j++)
            {
                numbersArray[i, j] = int.Parse(values[j].Trim());
            }
        }
        ViewBag.levelName = fileName;
        
        return View(new LevelEditorViewModel{ ItemPositions = numbersArray});
    }

    public IActionResult ChangeLevelRedirect(string levelName)
    {
        TempData["levelName"] = levelName;
        return RedirectToAction("Index");
    }
    
    public IActionResult NewLevelEditor()
    {
        return View();
    }
    
    public IActionResult Save([FromForm] IFormCollection formCollection, string levelName)
    {
        string filePath = $"GameLogic/{levelName}.txt";
        IEnumerable<KeyValuePair<string, string>> keyValuePairs = formCollection.Keys
            .Select(key => new KeyValuePair<string, string>(key, formCollection[key]));
        
        WriteValuesToFile(keyValuePairs, filePath);
        return RedirectToAction("Menu","Home");
    }
    
    static void WriteValuesToFile(IEnumerable<KeyValuePair<string, string>> keyValuePairs, string filePath)
    {
        using StreamWriter writer = new StreamWriter(filePath);
        int count = 1;
        foreach (var pair in keyValuePairs)
        {
            if (count == 10)
            {
                writer.Write(pair.Value + ",\n");
                count = 0;
            }
            else
            {
                writer.Write(pair.Value + ", ");
            }
            count++;
        }
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

