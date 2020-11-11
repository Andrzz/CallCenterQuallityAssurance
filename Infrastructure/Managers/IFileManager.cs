namespace Infrastructure.Managers
{
    public interface IFileManager
    {
        /// <summary>
        /// Get the lines in the file located at the specified path
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns>Array of lines</returns>
        string[] ReadLines(string pathToFile);
    }
}
