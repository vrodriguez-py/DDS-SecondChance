using Fire_Emblem_View;
namespace Fire_Emblem
{
    public static class FileHelper
    {
        public static List<string> PrintFiles(string directoryPath, View _view)
        {
            List<string> fileNames = new List<string>();
            if (System.IO.Directory.Exists(directoryPath))
            {
                var files = new System.IO.DirectoryInfo(directoryPath).GetFiles()
                    .OrderBy(f => f.Name);

                int index = 0;
                foreach (var file in files)
                {
                    _view.WriteLine($"{index}: {file.Name}");
                    index++;
                    fileNames.Add(file.Name);
                }
                
                
            }
            else
            {
                System.Console.WriteLine("Directory does not exist.");
                
            }

            return fileNames;

        }
    }
}
