var input = File.ReadAllLines("./input.txt");

var games = new List<Games>();

foreach (var line in input)
{
    var gameSplit = line.Split(":");
    var gameId = int.Parse(gameSplit[0].Substring("Game ".Length));
    var revealSplit = gameSplit[1].Split(";");
    var game = new Games { GameId = gameId };
    foreach (var reveal in revealSplit)
    {
        var gameCubes = new GameCubes();
        var cubeRevealSplit = reveal.Split(",");
        foreach (var cubeReveal in cubeRevealSplit)
        {
            var cubeSplit = cubeReveal.Trim().Split(" ");
            var colour = cubeSplit[1].ToLower() switch
            {
                "red" => CubeColour.Red,
                "green" => CubeColour.Green,
                "blue" => CubeColour.Blue,
                _ => throw new Exception("Unknown colour")
            };
            var count = int.Parse(cubeSplit[0]);
            gameCubes.Cubes.Add(new GameCube(colour, count));
        }
        game.GameCubes.Add(gameCubes);
    }
    games.Add(game);
}

var redMax = 12;
var greenMax = 13;
var blueMax = 14;
var possibleGameIds = new List<int>();
foreach (var game in games)
{
    var revealPossible = true;
    foreach (var reveal in game.GameCubes)
    {
        var redCount = reveal.Cubes.Where(c => c.Colour == CubeColour.Red).Sum(c => c.Count);
        var greenCount = reveal.Cubes.Where(c => c.Colour == CubeColour.Green).Sum(c => c.Count);
        var blueCount = reveal.Cubes.Where(c => c.Colour == CubeColour.Blue).Sum(c => c.Count);
        if (redCount > redMax || greenCount > greenMax || blueCount > blueMax)
        {
            revealPossible = false;
            break;
        }
    }

    if (revealPossible)
    {
        possibleGameIds.Add(game.GameId);
    }
}

Console.WriteLine($"Sum of Possible game ids: {possibleGameIds.Sum(x=>x)}");

public enum CubeColour
{
    Red, Green, Blue
}

public class Games
{
    public int GameId { get; set; }
    public List<GameCubes> GameCubes { get; set;  }

    public Games()
    {
        GameCubes = new List<GameCubes>();
    }
}

public class GameCubes
{
    public List<GameCube> Cubes { get; }

    public GameCubes()
    {
        Cubes = new List<GameCube>();
    }
}

public class GameCube
{
    public CubeColour Colour { get; }
    public int Count { get; }
    
    public GameCube(CubeColour colour, int count)
    {
        Colour = colour;
        Count = count;
    }
}  
