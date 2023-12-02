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

var powerMinimumGameIds = new List<int>();
foreach (var game in games)
{
    //var maxOfEachColour = game.GameCubes.SelectMany(x => x.Cubes).GroupBy(x => x.Colour).Select(x => (x.Key, x.Max(c => c.Count)));
    var maxOfEachColour = game.GameCubes.SelectMany(x => x.Cubes).GroupBy(x => x.Colour).Select(x => x.Max(c => c.Count));
    powerMinimumGameIds.Add(maxOfEachColour.Aggregate(1, (x, y) => x * y));

}

Console.WriteLine($"Sum of Possible game ids: {powerMinimumGameIds.Sum(x => x)}");

public enum CubeColour
{
    Red, Green, Blue
}

public class Games
{
    public int GameId { get; set; }
    public List<GameCubes> GameCubes { get; set; }

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
