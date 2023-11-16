using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Media;

class Program
{
    static void Main()
    {
        try
        {
            string filePath = "C:\\Users\\igorc\\OneDrive\\Рабочий стол\\file1";
            string[] fileLines = File.ReadAllLines(filePath);

            if (fileLines.Length == 2)
            {
                string imageUrl = fileLines[0].Split(' ')[0];
                string imageSavePath = fileLines[0].Split(' ')[1];
                string mp3Url = fileLines[1].Split(' ')[0];
                string mp3SavePath = fileLines[1].Split(' ')[1];

                Task.WaitAll(
                    Task.Run(() => DownloadFile(imageUrl, imageSavePath)),
                    Task.Run(() => DownloadFile(mp3Url, mp3SavePath))
                );

                Console.WriteLine("Загрузка файлов завершена.");

                PlaySound(mp3SavePath);
            }
            else
            {
                Console.WriteLine("Некорректный формат текстового файла.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DownloadFile(string url, string savePath)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, savePath);
                Console.WriteLine($"Файл загружен: {savePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке файла {savePath}: {ex.Message}");
        }
    }

    static void PlaySound(string filePath)
    {
        try
        {
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.Play();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при воспроизведении файла {filePath}: {ex.Message}");
        }
    }
}//