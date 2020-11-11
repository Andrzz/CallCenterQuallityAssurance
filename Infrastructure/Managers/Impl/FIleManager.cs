namespace Infrastructure.Managers.Impl
{
    using System.IO;

    public class FIleManager : IFileManager
    {
        public string[] ReadLines(string pathToFile)
        {
            return File.ReadAllLines(pathToFile);
        }
    }
}
