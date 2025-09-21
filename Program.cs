using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class TextProcessor
{
    private string text;

    // Nhap van ban tu ban phim
    public void Input()
    {
        Console.WriteLine("Nhap van ban:");
        text = Console.ReadLine();
    }

    // Chuan hoa van ban
    public string Normalize()
    {
        if (string.IsNullOrWhiteSpace(text)) return "";

        // Xoa khoang trang thua
        text = Regex.Replace(text, @"\s+", " ").Trim();

        // Viet hoa ky tu dau moi cau
        char[] delimiters = { '.', '!', '?' };
        string[] sentences = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < sentences.Length; i++)
        {
            string s = sentences[i].Trim();
            if (s.Length > 0)
            {
                sentences[i] = char.ToUpper(s[0]) + (s.Length > 1 ? s.Substring(1) : "");
            }
        }

        // Ghép lai voi dau cham cuoi
        text = string.Join(". ", sentences).Trim();
        if (!text.EndsWith(".")) text += ".";

        return text;
    }

    // Dem tong so tu
    public int CountWords()
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    // Dem so tu khac nhau
    public int CountDistinctWords()
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.ToLower()
                   .Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                   .Distinct()
                   .Count();
    }

    // Tan suat xuat hien
    public Dictionary<string, int> WordFrequency()
    {
        Dictionary<string, int> freq = new Dictionary<string, int>();
        string[] words = text.ToLower()
                             .Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string w in words)
        {
            if (freq.ContainsKey(w)) freq[w]++;
            else freq[w] = 1;
        }
        return freq;
    }

    // Hien thi ket qua
    public void DisplayResult()
    {
        Console.WriteLine("\nVan ban da chuan hoa:");
        Console.WriteLine(text);

        Console.WriteLine($"\nTong so tu: {CountWords()}");
        Console.WriteLine($"So tu khac nhau: {CountDistinctWords()}");

        Console.WriteLine("\nTan suat xuat hien:");
        foreach (var kv in WordFrequency())
        {
            Console.WriteLine($"{kv.Key} : {kv.Value}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TextProcessor tp = new TextProcessor();
        tp.Input();
        tp.Normalize();
        tp.DisplayResult();
    }
}
