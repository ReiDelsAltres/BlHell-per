using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlHell_per
{
    public class Question
    {
        public string[] styled = {
        "A","B","C","D","E","F"
        };
        public int index { get; set; }
        public byte rightPos { get; set; }
        public string questiona { get; set; }
        public string[] answersa { get; set; }

        StringBuilder question;
        StringBuilder[] answers;
        byte pos;
        public Question(int index, StringBuilder question)
        {
            this.index = index;
            this.question = question;

            this.rightPos = 0;
            this.pos = 0;
            this.answers = new StringBuilder[5];
        }
        [JsonConstructor]
        public Question(int index, byte rightPos, string questiona, string[] answersa)
        {
            this.index = index;
            this.questiona = questiona;
            this.rightPos = rightPos;
            this.answersa = answersa;
        }
        public Question add(StringBuilder answer, bool isRight)
        {
            //if (isRight) rightPos = pos;
            answers[pos] = answer;
            if (pos != 4) pos++;

            return this;
        }
        public void complete()
        {
            this.questiona = question.ToString();
            this.answersa = Array.ConvertAll<StringBuilder, string>(answers, st =>
            {
                if (st != null) return st.ToString();
                return "";
            });
        }
        public bool isCompleted()
        {
            return answersa[4] == null || answers[4] != null;
        }
    }
}
