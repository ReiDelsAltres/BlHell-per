using MudBlazor;

namespace BlHell_per.Core;
public class Info
{
    public struct Subject : IEquatable<Subject>
    {
        public static Subject InformationalTechnology = new()
        {
            Name = "FunOfIT",
            TranslatedNameAz = "Informasiya texnologiyalarının əsasları",
            TranslatedName = "Информационные технологии",
            Groups = ["759 ITS"],
            Teacher = "Həsənov Elçin Qafar oğlu",
            YellowAllerts = [],
            RedAllerts = []
        };
        public static Subject Programming = new()
        {
            Name = "Programming",
            TranslatedNameAz = "Proqramlaşdırmanın əsasları",
            TranslatedName = "Основы программирования",
            Groups = ["759 ITS"],
            Teacher = "Həsənov Elçin Qafar oğlu",
            YellowAllerts = [],
            RedAllerts = []
        };
        public static Subject DifferentialEquations = new()
        {
            Name = "DifferentialEquations",
            TranslatedNameAz = "Differensial tənliklər",
            TranslatedName = "Дифференциальные уравнения",
            Groups = ["759 ITS","759 KM"],
            Teacher = "Quliyeva Fatimə Ağayar qızı",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = [],
            IsExtended = true
        };
        public static Subject LinearAlgebra = new()
        {
            Name = "LinearAlgebra",
            TranslatedNameAz = "Xətti cəbr və analtik həndisə",
            TranslatedName = "Линейная Алгебра",
            Groups = ["759 ITS", "759 KM"],
            Teacher = "Quliyeva Fatimə Ağayar qızı",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = [],
            IsExtended = true
        };
        public static Subject InstrumentalPrograms = new()
        {
            Name = "Vusala",
            TranslatedNameAz = "İnstrumental və tətbiqi programlar",
            TranslatedName = "Инструментальные и прикладные программы",
            Groups = ["759 ITS"],
            Teacher = "Гасанова Вюсаля Рамиз кызы",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = []
        };
        public static Subject Physics = new()
        {
            Name = "Physics",
            TranslatedNameAz = "Fizika",
            TranslatedName = "Физика",
            Groups = ["759 ITS", "759 KM"],
            Teacher = "Ализаде Лейла Эльдар кызы",
            YellowAllerts = ["Вопросы могут не соответствовать вопросам на экзаменах"],
            RedAllerts = [],
            IsExtended = true
        };
        public static Subject English = new()
        {
            Name = "Eng",
            TranslatedNameAz = "Xarici dildə və akademik kommunikasiya",
            TranslatedName = "Английский",
            Groups = ["759 ITS", "759 KM"],
            Teacher = "Ismayılova Aybəniz Arif",
            YellowAllerts = ["Вопросы соответствуют экзаменационным, но ответы могут различатся"],
            RedAllerts = []
        };

        public static Subject[] Subjects = [InformationalTechnology,Programming,DifferentialEquations,InstrumentalPrograms,Physics,LinearAlgebra,English];

        public static Subject getSubject(string name) =>
            Array.Find(Subjects, subj => subj.Name.Equals(name));

        public Subject()
        {

        }

        public bool Equals(Subject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Name == other.Name;
        }

        public override bool Equals(object? obj) => 
            obj is Subject state && Equals(state);

        public required string Name { get; init; }
        public required string TranslatedNameAz { get; init; }
        public required string TranslatedName { get; init; }
        public required string[] Groups { get; init; }
        public required string Teacher { get; init; }
        public string[] YellowAllerts { get; init; }
        public string[] RedAllerts { get; init; }
        public bool IsExtended = false;
    }

}
