namespace Utils;
public static class FilesUtil
{
    public static void LoadEnvFile(){
        foreach (var line in File.ReadAllLines(@"./.env"))
        {
            var parts = line.Split(
                '=',
                StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                continue;
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}