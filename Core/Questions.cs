﻿using Microsoft.JSInterop;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace BlHell_per.Core.Questions;
public interface IQuestion<Q> where Q : IQuestion<Q>
{
    public int? Id { get; }
    public int? RId { get; }
    public string[]? Tags { get; }
    public string Title { get; }
    public string[] Answers { get; }
    public static abstract SerializationHandler<Q> Deserialize(string JSON);
    public static abstract Task<SerializationHandler<Q>> DeserializeAsync(string json);
    public static abstract string Serialize(SerializationHandler<Q> handler);
}
public class Question : IQuestion<Question>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public int? Id { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public int? RId { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] public string[]? Tags { get; set; }
    public string Title { get; }
    public string[] Answers { get; set; }
    [JsonConstructor] public Question(int? id, int? rid, string[]? tags, string title, string[] answers) =>
        (Id, RId, Tags, Title, Answers) = (id, rid, tags, title, answers);
    public Question(string title, string[] answers) =>
        (Title, Answers) = (title, answers);
    public void Shuffle()
    {
        string st = this.Answers[this.RId ?? 0];
        int rid = this.RId ?? 0;

        this.Answers.Shuffle();

        for (int i = 0; i < Answers.Length; i++)
            if (this.Answers[i].Equals(st))
            {
                rid = i;
                break;
            };
        this.RId = rid;
    }
    public void InsertAnswer(string answer)
    {
        string[] remembrance = new string[this.Answers.Length + 1];
        Array.Copy(this.Answers, remembrance, this.Answers.Length);
        remembrance[this.Answers.Length] = answer;
        this.Answers = remembrance;
    }
    public static SerializationHandler<Question> Deserialize(string JSON) =>
        JsonSerializer.Deserialize<SerializationHandler<Question>>(JSON, SerializationHandler._options) ??
        throw new ArgumentNullException("Return of Deserialization NULL");
    public static async Task<SerializationHandler<Question>> DeserializeAsync(string json)
    {
        return Question.Deserialize(json);

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

    public override bool Equals(object? obj)
    {
        return obj is Question question &&
               Id == question.Id &&
               RId == question.RId &&
               EqualityComparer<string[]?>.Default.Equals(Tags, question.Tags) &&
               Title == question.Title &&
               EqualityComparer<string[]>.Default.Equals(Answers, question.Answers);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, RId, Tags, Title, Answers);
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
    public static async Task<SerializationHandler<Q>> DeserializeAsync(string path) =>
        await Q.DeserializeAsync(path);
    public SerializationHandler<Q> Merge(SerializationHandler<Q> serialization) => 
        SerializationHandler<Q>.Merge(this, serialization);
    public static SerializationHandler<Q> Merge(SerializationHandler<Q> h0,SerializationHandler<Q> h1)
    {
        bool id = h0.UseIndex || h1.UseIndex;
        bool rid = h0.UseRId || h1.UseRId;
        Q[] qs = new Q[h0.Questions.Length + h1.Questions.Length];
        Array.Copy(h0.Questions, qs, h0.Questions.Length);
        Array.Copy(h1.Questions, 0, qs, h0.Questions.Length, h1.Questions.Length);
        return new(id, rid, qs);
    }
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

