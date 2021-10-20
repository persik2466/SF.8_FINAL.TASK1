using System;
using System.IO;

namespace SF._8_FINAL.TASK1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите полное имя папки, которую требуется почистить: ");
            string path = Console.ReadLine();
            if (!path.Contains(@":\"))
            {
                Console.WriteLine("Указан некорректный путь к папке: " + path);
            }
            else
            {
                var di = new DirectoryInfo(path);
                try
                {
                    if (di.Exists)
                    {
                        //Запуск рекурсивного метода очистки папки
                        DelDirFile(di);
                    }
                    else
                    {
                        Console.WriteLine("Каталога {0} не существует", path);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
            Console.ReadKey();

        }

        /// <summary>
        /// Рекурсивный метод очистки папки
        /// </summary>
        /// <param name="d"></param>
        static void DelDirFile(DirectoryInfo d)
        {
            FileInfo[] fil = d.GetFiles();

            foreach (FileInfo file in fil)
            {
                Console.WriteLine("Файл: {0}, использовался {1} ", file.Name, file.LastAccessTime);

                if (file.LastAccessTime < DateTime.Now - TimeSpan.FromMinutes(30))
                {
                    Console.WriteLine($"Удаляем файл: {file.Name}");
                    file.Delete();
                }
                else
                {
                    Console.WriteLine("Файл {0} использовался в последние 30 минут, не трогаем его", file.Name);
                }
            }

            DirectoryInfo[] direct = d.GetDirectories();

            foreach (DirectoryInfo dir in direct)
            {
                Console.WriteLine("Каталог: {0} использовался {1} ", dir.FullName, dir.LastAccessTime);

                if (dir.LastAccessTime < DateTime.Now - TimeSpan.FromMinutes(30))
                {
                    Console.WriteLine($"Удаляем каталог: {dir.Name}");
                    dir.Delete(true);
                }
                else
                {
                    Console.WriteLine("Каталог {0} использовался в последние 30 минут, не трогаем его ", dir.Name);
                    DelDirFile(dir);
                }
            }
        }
    }
}
