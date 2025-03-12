using OpenAI.Assistants;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace BlHell_per.Base.Questions;
public interface IQuestion<Q> where Q : IQuestion<Q>
{
    public int? Id { get; init; }
    public int? RId { get; init; }
    public string Title { get; }
    public string[] Answers { get; }
    public static abstract SerializationHandler<Q> Deserialize(string JSON);
    public static abstract Task<SerializationHandler<Q>> DeserializeAsync(string path,HttpClient client);
    public static abstract string Serialize(SerializationHandler<Q> handler);
}
public class Question : IQuestion<Question>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public int? Id { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public int? RId { get; init; }
    public string Title { get; }
    public string[] Answers { get; }
    [JsonConstructor] public Question(int? id, int? rid, string title, string[] answers) =>
        (Id, RId, Title, Answers) = (id, rid, title, answers);
    public Question(string title, string[] answers) =>
        (Title, Answers) = (title, answers);

    public static SerializationHandler<Question> Deserialize(string JSON) =>
        JsonSerializer.Deserialize<SerializationHandler<Question>>(JSON, SerializationHandler._options) ??
        throw new ArgumentNullException("Return of Deserialization NULL");
    public static async Task<SerializationHandler<Question>> DeserializeAsync(string path, HttpClient client)
    {
        var str = await client.GetStringAsync(path);
        return Question.Deserialize(str);
        /*await client.GetFromJsonAsync<SerializationHandler<Question>>(path, SerializationHandler._options) ??
        throw new ArgumentNullException("Return of Deserialization NULL");*/
    }

    public static string Serialize(SerializationHandler<Question> handler)
    {
        bool doSomething = handler.UseIndex || handler.UseRId;
        SerializationHandler<Question>? transHandler = doSomething ? null : handler;
        if (doSomething)
        {
            Question[] trans = new Question[handler.Questions.Length];
            for (int i = 0; i < handler.Questions.Length; i++)
            {
                Question question = handler.Questions[i];
                trans[i] = new Question(question.Title, question.Answers)
                {
                    Id = handler.UseIndex ? question.Id ?? i : null,
                    RId = handler.UseRId ? question.RId ?? 0 : null,
                };
            }
            transHandler = new(handler.UseIndex, handler.UseRId, trans);
        }
        return JsonSerializer.Serialize(transHandler, SerializationHandler._options);
    }
    public override string ToString()
    {
        string answers = "\n";
        foreach (string str in Answers)
            answers += str + "\n";
        return $"Id: {Id} \n" +
               $"RId: {RId} \n" +
               $"Title: {Title} \n" +
               $"Answers: ---{answers}---";
    }
}
public class SerializationHandler
{
    public static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}
public class SerializationHandler<Q> : SerializationHandler where Q : IQuestion<Q>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] public bool UseIndex { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] public bool UseRId { get; init; }

    public Q[] Questions { get; }
    [JsonConstructor] public SerializationHandler(bool useIndex, bool useRid, Q[] questions) => 
        (UseIndex, UseRId, Questions) = (useIndex, useRid, questions);
    public SerializationHandler(Q[] questions) => 
        Questions = questions;
    public string Serialize() => 
        Q.Serialize(this);
    public static SerializationHandler<Q> Deserialize(string JSON) => 
        Q.Deserialize(JSON);
    public static async Task<SerializationHandler<Q>> DeserializeAsync(string path, HttpClient client) =>
        await Q.DeserializeAsync(path, client);
    public override string ToString()
    {
        string questions = "\n";
        foreach (Q ques in Questions)
            questions += ques + "\n";
        return $"UseIndex: {UseIndex} \n" +
               $"UseRId: {UseRId} \n" +
               $"Questions: ---{questions}---";
    }
}

