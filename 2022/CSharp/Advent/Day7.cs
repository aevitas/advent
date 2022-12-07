using System.Security.Cryptography;

namespace Advent;

public static class Day7
{
    private static readonly string Input = File.ReadAllText("Day7.txt");

    public static void PartOne()
    {
        var fs = BuildFileSystem("1");

        var dirs = fs.GetDirectories("*.*", SearchOption.AllDirectories);
        var eligible = new List<long>();

        foreach (var d in dirs)
        {
            var files = d.EnumerateFiles("*.*", SearchOption.AllDirectories);

            var size = files.Sum(f => f.Length);

            if (size >= 100000)
                continue;

            eligible.Add(size);
        }

        Console.WriteLine(eligible.Sum());
    }

    private static DirectoryInfo BuildFileSystem(string dir)
    {
        var root = new DirectoryInfo($"./fs/var/aoc/{dir}");
        var fs = new DirectoryInfo($"./fs/var/aoc/{dir}");

        if (fs.Exists)
            fs.Delete();

        fs.Create();

        var commands = Input.Split('$');

        foreach (var command in commands)
        {
            if (string.IsNullOrWhiteSpace(command))
                continue;

            HandleCommand(command);
        }

        void HandleCommand(string command)
        {
            var lines = command.Split(Environment.NewLine);

            var cmd = lines[0].Trim().Split(' ');

            switch (cmd[0])
            {
                case "cd":
                    var tarDir = cmd[1];
                    if (tarDir == "/")
                        return;

                    var nd = new DirectoryInfo(Path.Combine(fs.FullName, tarDir));
                    if (!nd.Exists)
                        nd.Create();

                    fs = nd;
                    break;
                case "ls":
                    foreach (var line in lines[1..])
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;

                        // File
                        if (char.IsDigit(line[0]))
                        {
                            var size = int.Parse(line.Split(' ')[0]);
                            var fileName = line.Split(' ')[1];

                            var fi = new FileInfo(Path.Combine(fs.FullName, fileName));
                            var buffer = new byte[size];
                            RandomNumberGenerator.Fill(buffer);

                            File.WriteAllBytes(fi.FullName, buffer);
                        }
                        else
                        {
                            var dirName = line.Split(' ')[1];

                            var di = new DirectoryInfo(Path.Combine(fs.FullName, dirName));

                            di.Create();
                        }
                    }
                    break;
            }
        }

        return root;
    }
}