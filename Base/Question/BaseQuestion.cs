namespace BlHell_per.Base.Questions;

public class BaseQuestion : Question
{
    private static char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    /*new public string Title
    {
        get
        {
            return
        }
    }*/
    /*new public string[] Answers
    {
        get
        {
            string[] strs = new string[base.Answers.Length];
            for (int i = 0; i < base.Answers.Length; i++)
            {
                strs[i] = $"{_alphabet[i]}.{base.Answers[i]}";
            }
            return strs;
        }
    }*/

    public Question Immutable { get; }
    public BaseQuestion Shuffle
    {
        get
        {
            base.Answers.Shuffle();
            return this;
        }
    }

    public BaseQuestion(Question question) : 
        base(question.Id, question.RId, question.Title,question.Answers) =>
        this.Immutable = question;

    public static BaseQuestion[] Trans(Question[] questions)
    {
        BaseQuestion[] bases = new BaseQuestion[questions.Length];
        for (int i = 0; i < questions.Length; i++)
        {
            Question ques = questions[i];
            bases[i] = new BaseQuestion(ques)
            {
                Id = ques.Id ?? i,
                RId = ques.RId ?? 0
            };
        }
        return bases;
    }
    public static BaseQuestion[] Trans(Question[] questions,int min,int max)
    {
        BaseQuestion[] bases = new BaseQuestion[questions.Length];
        for (int i = min; i < max; i++)
        {
            Question ques = questions[i];
            bases[i] = new BaseQuestion(ques)
            {
                Id = ques.Id ?? i,
                RId = ques.RId ?? 0
            };
        }
        return bases;
    }

}
