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
            Teacher = "Həsənov Elçin Qafar oğlu"
        };
        public static Subject Programming = new()
        {
            Name = "Programming",
            TranslatedName = "Основы программирования",
            Groups = ["759 ITS"],
            Teacher = "Həsənov Elçin Qafar oğlu"
        };
        public static Subject DifferentialEquations = new()
        {
            Name = "DifferentialEquations",
            TranslatedName = "Дифференциальные уравнения",
            Groups = ["759 ITS","759 KM"],
            Teacher = "Quliyeva Fatimə Ağayar qızı"
        };
        public static Subject InstrumentalPrograms = new()
        {
            Name = "Vusala",
            TranslatedName = "Инструментальные и прикладные программы",
            Groups = ["759 ITS"],
            Teacher = "Гасанова Вюсаля Рамиз кызы"
        };

        private static Subject[] _subjects = [InformationalTechnology,Programming,DifferentialEquations,InstrumentalPrograms];

        public static Subject getSubject(string name) =>
            Array.Find(_subjects, subj => subj.Name.Equals(name));

        public required string Name { get; init; }
        public required string TranslatedName { get; init; }
        public required string[] Groups { get; init; }
        public required string Teacher { get; init; }
    }

}
