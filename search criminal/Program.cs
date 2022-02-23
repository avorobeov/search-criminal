using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace search_criminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            string userInput;
            bool isExit = false;

            while (isExit == false)
            {
                ShowMenu();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        SetParametersSearch(database);
                        break;

                    case "2":
                        isExit = true;
                        break;
                }

            }
        }

        private static void SetParametersSearch(Database database)
        {
            int growth = GetNumber("Ведите  рост преступника:");

            int weight = GetNumber("Ведите  вес преступника:");

            Console.WriteLine("Ведите национальность  преступника:");

            string nationality = Console.ReadLine();

            database.TryFindCriminal(nationality, growth, weight);
        }

        private static void ShowMenu()
        {
            Console.WriteLine("\nДля поиска по ключам нажмите 1\n" +
                              "Для выхода нажмите 2");
        }

        private static int GetNumber(string Text)
        {
            int meaning = 0;
            bool isCorrect = false;

            while (isCorrect == false)
            {
                Console.WriteLine(Text);

                string userInput = Console.ReadLine();

                if (Int32.TryParse(userInput, out meaning))
                {
                    return meaning;
                }
                else
                {
                    Console.WriteLine("Вы вели вместо числа строку");
                }

                userInput = Console.ReadLine();
            }

            return meaning;
        }
    }

    class Criminal
    {
        public string Surname { get; private set; }
        public string Name { get; private set; }
        public string Patronymic { get; private set; }
        public string Nationality { get; private set; }
        public bool IsPrisoner { get; private set; }
        public int Growth { get; private set; }
        public int Weight { get; private set; }

        public Criminal(string surname, string name, string patronymic, string nationality, bool isPrisoner,int growth,int weight)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Nationality = nationality;
            IsPrisoner = isPrisoner;
            Growth = growth;
            Weight = weight;
        }
    }

    class Database
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public Database()
        {
            _criminals.Add(new Criminal("Агеев", "Матвей", "Иванович", "украинец",true,171,65));
            _criminals.Add(new Criminal("Агеев", "Михаил", "Львович", "украинец", false, 171, 76));
            _criminals.Add(new Criminal("Агеева", "Дарья", "Антоновна", "белорус", false, 171, 76));
            _criminals.Add(new Criminal("Акимов", "Владимир", "Ильич", "белорус",false,199,130));
            _criminals.Add(new Criminal("Акимова", "Мия", "Михайловна", "россиянин", true, 184,78));
            _criminals.Add(new Criminal("Александрова", "Вероника", "Матвеевна", "россиянин", false,150,45));
            _criminals.Add(new Criminal("Алексеев", "Роман", "Ильич", "россиянин", false, 171, 65));
            _criminals.Add(new Criminal("Андреева", "Виктория", "Тимофеевна", "россиянин",false, 171, 76));
            _criminals.Add(new Criminal("Антипов", "Всеволод", "Иванович", "белорус", false, 171, 76));
            _criminals.Add(new Criminal("Антонов", "Николай", "Матвеевич", "украинец", true, 171, 65));
        }

        public void TryFindCriminal(string nationality, int growth, int weight)
        {
            var criminals = _criminals.Where(criminal => criminal.Nationality == nationality
                               && criminal.Growth == growth
                               && criminal.Weight == weight
                               && criminal.IsPrisoner == false).ToList();

            if (criminals.Count != 0)
            {
                ShowListCriminals(criminals);
            }
            else
            {
                ShowMessage("\nПо вашему запросу нет ни одного совпадения", ConsoleColor.Yellow);
            }
        }

        private void ShowListCriminals(List<Criminal> criminals)
        {
            ShowMessage("\n\n\nСписок преступников\n\n\\n", ConsoleColor.Green);

            foreach (Criminal criminal in criminals)
            {
                ShowMessage($"\n{criminal.Surname} {criminal.Name} {criminal.Patronymic} рост:{criminal.Growth} вес:{criminal.Weight} национальность:{criminal.Nationality}", ConsoleColor.Red);
            }
        }
     
        private void ShowMessage(string message, ConsoleColor color)
        {
            ConsoleColor preliminaryColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message + "\n");

            Console.ForegroundColor = preliminaryColor;
        }
    }
}
