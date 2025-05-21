namespace BlHell_per.Core;
public class Info
{
    public struct Subject
    {
        public static Subject InformationalTechnology = new()
        {
            Name = "FunOfIT",
            TranslatedName = "Информационные технологии",
            Groups = ["759 ITS"],
            Teacher = "Həsənov Elçin Qafar oğlu",
            YellowAllerts = [],
            RedAllerts = []
        };
        public static Subject Programming = new()
        {
            Name = "Programming",
            TranslatedName = "Основы программирования",
            Groups = ["759 ITS"],
            Teacher = "Həsənov Elçin Qafar oğlu",
            YellowAllerts = [],
            RedAllerts = []
        };
        public static Subject DifferentialEquations = new()
        {
            Name = "DifferentialEquations",
            TranslatedName = "Дифференциальные уравнения",
            Groups = ["759 ITS","759 KM"],
            Teacher = "Quliyeva Fatimə Ağayar qızı",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = []
        };
        public static Subject LinearAlgebra = new()
        {
            Name = "LinearAlgebra",
            TranslatedName = "Линейная Алгебра",
            Groups = ["759 ITS", "759 KM"],
            Teacher = "Quliyeva Fatimə Ağayar qızı",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = ["Тест не закончен"]
        };
        public static Subject InstrumentalPrograms = new()
        {
            Name = "Vusala",
            TranslatedName = "Инструментальные и прикладные программы",
            Groups = ["759 ITS"],
            Teacher = "Гасанова Вюсаля Рамиз кызы",
            YellowAllerts = [],
            RedAllerts = []
        };
        public static Subject Physics = new()
        {
            Name = "Physics",
            TranslatedName = "Физика",
            Groups = ["759 ITS", "759 KM"],
            Teacher = "Ализаде Лейла Эльдар кызы",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = []
        };

        private static Subject[] _subjects = [InformationalTechnology,Programming,DifferentialEquations,InstrumentalPrograms,Physics,LinearAlgebra];

        public static Subject getSubject(string name) =>
            Array.Find(_subjects, subj => subj.Name.Equals(name));

        public required string Name { get; init; }
        public required string TranslatedName { get; init; }
        public required string[] Groups { get; init; }
        public required string Teacher { get; init; }
        public string[] YellowAllerts { get; init; }
        public string[] RedAllerts { get; init; }
    }

}
