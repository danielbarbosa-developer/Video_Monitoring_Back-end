using System;
using System.IO;

namespace Backend.Application.Handlers
{
    public static class VideoFileHandler
    {
        public static string ConvertVideoToBase64(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }

        public static void SaveVideoAsBinaryFile(string videoContent, string path)
        {
            var bytes = Convert.FromBase64String(videoContent);
            File.WriteAllBytes(path, bytes);
        }
    }
}