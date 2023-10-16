class Note
{
    public string Title { get; set; } // Заголовок заметки
    public string Description { get; set; } // Описание заметки
    public DateTime Date { get; set; } // Дата заметки
}

class DailyPlanner
{
    private Dictionary<DateTime, List<Note>> notesByDate; // Словарь для хранения заметок по датам
    private int currentNoteIndex; // Текущий индекс открытой заметки
    private DateTime currentDate; // Текущая дата

    public DailyPlanner() // Инициализация словаря
    {
        notesByDate = new Dictionary<DateTime, List<Note>>();
        currentDate = DateTime.Today; // Установка начальной даты (сегодняшней даты)
    }

    public void AddNoteOnDate(Note note, DateTime date)
    {
        if (notesByDate.ContainsKey(date))
        {
            notesByDate[date].Add(note); // Если в словаре уже есть заметки на заданную дату, добавляем заметку в список заметок этой даты
        }
        else
        {
            List<Note> notes = new List<Note> { note }; // Если в словаре нет заметок на заданную дату, создаем новый список заметок и добавляем в него заметку
            notesByDate.Add(date, notes);  // Добавляем список заметок в словарь по заданной дате
        }
    }

    public void CreateNote()
    {
        Console.Clear();
        Console.WriteLine("Создание новой заметки:");

        Console.Write("Название: ");
        string title = Console.ReadLine();

        Console.Write("Описание: ");
        string description = Console.ReadLine();

        Note newNote = new Note { Title = title, Description = description, Date = currentDate };
        AddNoteOnDate(newNote, currentDate); // Добавляем новую заметку на текущую дату

        Console.WriteLine("Новая заметка добавлена. Нажмите Enter, чтобы продолжить...");
        Console.ReadLine();
    }

    public void SwitchDate(bool moveForward)
    {
        if (moveForward)
        {
            currentDate = currentDate.AddDays(1); // Переход на следующую дату
        }
        else
        {
            currentDate = currentDate.AddDays(-1); // Переход на предыдущую дату
        }

        currentNoteIndex = 0; // Сброс текущего индекса заметки
    }

    public void OpenNote()
    {
        Console.Clear();
        if (notesByDate.ContainsKey(currentDate))
        {
            Note currentNote = notesByDate[currentDate][currentNoteIndex]; // Получаем текущую заметку по текущей дате и текущему индексу
            Console.WriteLine("Заметка:");
            Console.WriteLine($"Описание: {currentNote.Description}");  // Выводим описание текущей заметки
        }
        Console.WriteLine($"Дата: {currentDate.ToString("dd.MM.yy")}"); // Выводим текущую дату
        Console.ReadLine();
    }

    public void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine($"Дневник на {currentDate.Date.ToString("dd.MM.yy")}");

        if (notesByDate.ContainsKey(currentDate))
        {
            List<Note> notes = notesByDate[currentDate]; // Получаем список заметок по текущей дате


            for (int i = 0; i < notes.Count; i++)
            {
                string noteTitle = notes[i].Title; // Получаем заголовок заметки
                if (i == currentNoteIndex)
                {
                    Console.WriteLine($"-> {noteTitle}"); // Если это текущая заметка, выводим со стрелкой
                }
                else
                {
                    Console.WriteLine($"   {noteTitle}"); // Иначе просто выводим заголовок
                }
            }
        }
    }

    // Метод для обработки ввода пользователя
    public void HandleInput()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        ConsoleKey key = keyInfo.Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (notesByDate.ContainsKey(currentDate))
                {
                    currentNoteIndex = (currentNoteIndex - 1 + notesByDate[currentDate].Count) % notesByDate[currentDate].Count;
                }
                break;
            case ConsoleKey.DownArrow:
                if (notesByDate.ContainsKey(currentDate))
                {
                    currentNoteIndex = (currentNoteIndex + 1) % notesByDate[currentDate].Count;
                }
                break;
            case ConsoleKey.LeftArrow:
                SwitchDate(false);
                break;
            case ConsoleKey.RightArrow:
                SwitchDate(true);
                break;
            case ConsoleKey.Enter:
                OpenNote();
                break;
            case ConsoleKey.Escape:
                Environment.Exit(0);
                break;
            case ConsoleKey.N:
                CreateNote();
                break;
        }
    }
}

// Тело программы
class Program
{
    static void Main(string[] args)
    {
        DailyPlanner planner = new DailyPlanner();

        Note note1 = new Note { Title = "Заметка 1", Description = "Покушать", Date = new DateTime(2023, 10, 16) };
        Note note2 = new Note { Title = "Заметка 2", Description = "Магазин", Date = new DateTime(2023, 10, 17) };
        Note note3 = new Note { Title = "Заметка 3", Description = "Техникум", Date = new DateTime(2023, 10, 20) };
        Note note4 = new Note { Title = "Заметка 5", Description = "Интернет магазин", Date = new DateTime(2023, 10, 20) };
        Note note5 = new Note { Title = "Заметка 6", Description = "Каникулы", Date = new DateTime(2023, 10, 20) };

        planner.AddNoteOnDate(note1, note1.Date);
        planner.AddNoteOnDate(note2, note2.Date);
        planner.AddNoteOnDate(note3, note3.Date);
        planner.AddNoteOnDate(note4, note4.Date);
        planner.AddNoteOnDate(note5, note5.Date);

        while (true)
        {
            planner.DisplayMenu();
            planner.HandleInput();
        }
    }
}

