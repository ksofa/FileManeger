using System;
using System.IO;
using System.Text;
namespace FileM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Добро пожаловать! Это файловый менеджер. Вот список доступных операций.");
                Menu();
                Select();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Menu();
                Select();
            }
        }
        /// <summary>
        /// Выводит меню и правила испозьзования файлового менеджера.
        /// </summary>
        static void Menu()
        {
            Console.WriteLine("Введите номер одной из предложенных ниже операций");
            Console.WriteLine("1. Просмотр списка дисков компьютера и выбор диска");
            Console.WriteLine("2. Переход в другую директорию (выбор папки)");
            Console.WriteLine("3. Просмотр списка файлов в директории");
            Console.WriteLine("4. Вывод содержимого текстового файла в консоль в кодировке UTF-8");
            Console.WriteLine("5. Вывод содержимого текстового файла в консоль в выбранной пользователем кодировке");
            Console.WriteLine("6. Копирование файла");
            Console.WriteLine("7. Перемещение файла в выбранную пользователем директорию");
            Console.WriteLine("8. Удаление файла");
            Console.WriteLine("9. Создание простого текстового файла в кодировке UTF-8");
            Console.WriteLine("10.Создание простого текстового файла в выбранной пользователем кодировке");
            Console.WriteLine("11.Конкатенация содержимого двух или более текстовых файлов и вывод результата в консоль в кодировке UTF - 8");
            Console.WriteLine("");
        }
        //Происходит выбор операции
        static void Select()
        {
            string drive = null;
            string directory = null;
            string file = null;
            do
            {
                Console.WriteLine("Введите номер операции:");
                int number;
                //Проверка правильности введенного номера.
                while (!int.TryParse(Console.ReadLine(), out number) || (number < 0 || number > 12))
                {
                    Console.WriteLine("Введенный номер оперции не корректен.Введите номер операции снова.");
                }
                switch (number)
                {
                    case 1:
                        //Просмотр списка дисков компьютера и выбор диска.
                        СhooseDrives(ref drive);
                        break;
                    case 2:
                        СhooseDrives(ref drive);
                        //Переход в другую директорию(выбор папки).
                        СhooseDirectories(drive, ref directory);
                        break;
                    case 3:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        //Просмотр списка файлов в директории.
                        ViewFiles(directory, ref file);
                        break;
                    case 4:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        ViewFiles(directory, ref file);
                        //Вывод содержимого текстового файла в консоль в кодировке UTF-8.
                        OutputFile(file);
                        break;
                    case 5:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        ViewFiles(directory, ref file);
                        //Вывод содержимого текстового файла в консоль в выбранной пользователем кодировке.
                        OutputFileEncode(file);
                        break;
                    case 6:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        ViewFiles(directory, ref file);
                        //Копирование файла.
                        Copy(file, directory);
                        break;
                    case 7:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        ViewFiles(directory, ref file);
                        //Перемещение файла в выбранную пользователем директорию.
                        Move(file);
                        break;
                    case 8:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        ViewFiles(directory, ref file);
                        //Удаление файла.
                        Delete(file);
                        break;
                    case 9:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        //Создание простого текстового файла в кодировке UTF-8.
                        CreateFile(directory);
                        break;
                    case 10:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        //Создание простого текстового файла в выбранной пользователем кодировке.
                        CreateFileEncode(directory);
                        break;
                    case 11:
                        СhooseDrives(ref drive);
                        СhooseDirectories(drive, ref directory);
                        //Конкатенация содержимого двух или более текстовых файлов и вывод результата в консоль в кодировке UTF - 8.
                        Concatenation(directory);
                        break;
                }
                Console.WriteLine("Нажмите Esc для завершения работы, в противном случае нажмите любую клавишу.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// //Конкатенация содержимого двух или более текстовых файлов и вывод результата в консоль в кодировке UTF - 8.
        /// </summary>
        /// <param name=" track"></param>
        static void Concatenation(string track)
        {
            try
            {
                string[] filesarray = null;
                Console.WriteLine("Введите без расширения название нового файла ");
                string fileName = Console.ReadLine() + ".txt";
                //Существует ли файл.
                if (File.Exists(Path.Combine(track, fileName)))
                {

                    Console.WriteLine("Файл уже существует");
                    Concatenation(track);
                }
                else
                {
                    Console.WriteLine("Введите номера нужных файлов.Если файлы выбраны, нажмите +");
                    filesarray = Directory.GetFiles(track, "*.txt");
                    int k = 0;
                    int number;
                    foreach (var elem in filesarray)
                    {
                        Console.WriteLine(k + "." + elem);
                        k++;
                    }
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (input == "+")
                            break;
                        while (!int.TryParse(input, out number) || (number < 0 || number > k - 1))
                        {
                            Console.WriteLine("Данные не корректны.Введите число снова.");
                            input = Console.ReadLine();
                        }
                        string file = filesarray[number];
                        File.AppendAllText(Path.Combine(track, fileName), File.ReadAllText(file));
                        Console.WriteLine("Продолжайте вводить номера файлов, которые хотите сконкатенировать.Иначе для окончания ввода введите '+'");
                    }


                    track = Path.Combine(track, fileName);
                    Console.WriteLine(track);
                    Console.WriteLine($"Итог" + track);
                    Console.WriteLine(File.ReadAllText(track, Encoding.UTF8));
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
        /// <summary>
        /// Создание простого текстового файла в кодировке UTF-8.
        /// </summary>
        /// <param name="track"></param>
        static void CreateFile(string track)
        {

            Console.WriteLine("Введите новое название файла без расширения");
            string newName = Console.ReadLine() + ".txt";
            if (!File.Exists(Path.Combine(track, newName)))
            {
                Console.WriteLine("Введите текст файла");
                string text = Console.ReadLine();
                File.WriteAllText(Path.Combine(track, newName), text, Encoding.UTF8);
            }
            else
            {
                Console.WriteLine("Данный файл существует");
                CreateFile(track);
            }
        }
        /// <summary>
        ///  Создание простого текстового файла в выбранной пользователем кодировке.
        /// </summary>
        /// <param name=" track"></param>
        static void CreateFileEncode(string track)
        {
            string di = null;
            string direct = null;
            СhooseDrives(ref di);
            СhooseDirectories(di, ref direct);
            Console.WriteLine("Введите новое название файла без расширения");
            string newName = Console.ReadLine() + ".txt";
            if (!File.Exists(Path.Combine(track, newName)))
            {
                Console.WriteLine("Введите текст файла");
                string text = Console.ReadLine();
                int number;
                Console.WriteLine("Выберите нужную кодировку и введите ее номер:");
                Console.WriteLine("1.ASCII");
                Console.WriteLine("2.UTF32");
                Console.WriteLine("3.Unicode");
                while (!int.TryParse(Console.ReadLine(), out number) || (number < 0 || number > 3))
                {
                    Console.WriteLine("Данные ведены некорректно.Введите число снова.");
                }
                if (number == 1)
                    File.WriteAllText(Path.Combine(track, newName), text, Encoding.ASCII);
                if (number == 2)
                    File.WriteAllText(Path.Combine(track, newName), text, Encoding.UTF32);
                if (number == 3)
                    File.WriteAllText(Path.Combine(track, newName), text, Encoding.Unicode);

                Console.WriteLine($"Файл {Path.Combine(track, newName)} создан в указанной кодировке");
            }
            else
            {
                Console.WriteLine("Данный файл существует");
                CreateFileEncode(track);
            }
        }
        /// <summary>
        ///  Удаление файла.
        /// </summary>
        /// <param name=" track"></param>
        static void Delete(string track)
        {
            File.Delete(track);
            Console.WriteLine($"Файл { track} удален");
            //Menu();
            //Select();
        }
        /// <summary>
        /// Перемещение файла в выбранную пользователем директорию.
        /// </summary>
        /// <param name="track"></param>
        static void Move(string track)
        {
            string drivemove = null;
            string directmove = null;
            Console.WriteLine("Выберите нужный путь для перемещения файла:");
            СhooseDrives(ref drivemove);
            СhooseDirectories(drivemove, ref directmove);
            Console.WriteLine("Введите новое имя файла");
            string newFile = Console.ReadLine() + ".txt";
            //Проверка на существование файла.
            if (!File.Exists(Path.Combine(directmove, newFile)))
            {
                File.Move(track, Path.Combine(directmove, newFile));
                Console.WriteLine($"Файл перемещен в {directmove}");
            }
            else
            {
                Console.WriteLine("Данный файл существует");
                Move(track);
            }
        }
        /// <summary>
        /// Копирование файла.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="directory"></param>
        static void Copy(string track, string directory)
        {
            Console.WriteLine("Напишите название файла, который будет копией выбранного файла без расширения");
            string newName = Console.ReadLine() + ".txt";
            if (!File.Exists(Path.Combine(directory, newName)))
            {
                File.Copy(track, Path.Combine(directory, newName));
                Console.WriteLine($"Файл {track} скопирован в директорию");
            }
            else
            {
                Console.WriteLine("Данный файл существует");
                Copy(track, directory);
            }
        }
        /// <summary>
        /// Вывод содержимого текстового файла в консоль в кодировке UTF-8
        /// </summary>
        /// <param name="track"></param>
        static void OutputFile(string track)
        {

            if (File.Exists(track))
            {
                Console.WriteLine(File.ReadAllText(track, Encoding.UTF8));
            }

        }
        /// <summary>
        /// Вывод содержимого текстового файла в консоль в кодировке UTF-8
        /// </summary>
        /// <param name="track"></param>
        static void OutputFileEncode(string track)
        {
            int number;
            if (File.Exists(track))
            {
                Console.WriteLine("Выберите нужную кодировку и введите ее номер:");
                Console.WriteLine("1.ASCII");
                Console.WriteLine("2.UTF32");
                Console.WriteLine("3.Unicode");
                while (!int.TryParse(Console.ReadLine(), out number) || (number < 0 || number > 3))
                {
                    Console.WriteLine("Данные ведены некорректно.Введите число снова.");
                }
                if (number == 1)
                    Console.WriteLine(File.ReadAllText(track, Encoding.ASCII));
                if (number == 2)
                    Console.WriteLine(File.ReadAllText(track, Encoding.UTF32));
                if (number == 3)
                    Console.WriteLine(File.ReadAllText(track, Encoding.Unicode));
            }

        }

        /// <summary>
        /// Просмотр списка файлов в директории.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="file"></param>

        static void ViewFiles(string directory, ref string file)
        {
            int k = 0;
            int number;
            string track = directory;
            string[] allFiles;

            Console.WriteLine("Введите номер выбранного файла");
            allFiles = Directory.GetFiles(track);
            if (allFiles.Length != 0)
            {
                foreach (var element in allFiles)
                {
                    Console.WriteLine(k + "." + element);
                    k++;
                }
                while (!int.TryParse(Console.ReadLine(), out number) || (number < 0 || number > k - 1))
                {
                    Console.WriteLine("Данные ведены некорректно.Введите число снова.");
                }
                file = allFiles[number];
                Console.WriteLine($"Вы выбрали файл {file}.");
            }
            else
            {
                Console.WriteLine("Папка пустая. Файлов нет.");
                Menu();
                Select();
            }

        }
        /// <summary>
        /// Переход в другую директорию (выбор папки).
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="directory"></param>
        static void СhooseDirectories(string drive, ref string directory)
        {

            string[] allDirectories = null;
            string track = drive;
            do
            {
                allDirectories = Directory.GetDirectories(track);
                if (allDirectories.Length == 0)
                {
                    break;
                }
                int k = 0;
                int number;
                foreach (var folder in allDirectories)
                {
                    Console.WriteLine(k + "." + folder);
                    k++;
                }
                while (!int.TryParse(Console.ReadLine(), out number) || (number < 0 || number > k - 1))
                {
                    Console.WriteLine("Данные ведены некорректно.Введите число снова.");
                }
                track = allDirectories[number];
                Console.WriteLine("Нажмите Enter, если Вы дошли до нужной папки. Иначе нажмите другую клавишу, чтобы спуститься в папки ниже (папки внутри папки)");
            } while (Console.ReadKey().Key != ConsoleKey.Enter);
            Console.WriteLine($"Выбранная папка { track}");
            directory = track;
        }
        /// <summary>
        /// Просмотр списка дисков компьютера и выбор диска.
        /// </summary>
        /// <param name="drive"></param>
        static void СhooseDrives(ref string drive)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            bool exist = false;
            foreach (DriveInfo di in allDrives)
            {
                Console.WriteLine("Диск {0}", di.Name);
            }
            Console.WriteLine("Введите название выбранного диска:");
            Console.WriteLine("Например, 'Диск C:' - нужно ввести С (на нужном языке)");
            drive = Console.ReadLine();
            while (!exist)
            {
                foreach (DriveInfo di in allDrives)
                {
                    if (di.Name[0].ToString() == drive)
                    {
                        Console.WriteLine($"Выбран диск {drive}.");
                        exist = true;
                        drive = di.Name;
                        break;
                    }
                }
                if (!exist)
                {
                    Console.WriteLine("Выбранного диска не существует.Введите данные снова:");
                    drive = Console.ReadLine();
                }
            }
        }
    }
}
