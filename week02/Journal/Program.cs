using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Yo! Ready to journal? Or just pretending to be productive?");
        Console.WriteLine("(No judgment here, I'm just a program)\n");

        var journal = new Jornal();

        while (true)
        {
            Console.WriteLine("\nMain Menu (pick a number, any number):");
            Console.WriteLine("1. Brain dump");
            Console.WriteLine("2. Read past ramblings");
            Console.WriteLine("3. Save to disk (because RAM is flaky)");
            Console.WriteLine("4. Load from disk (if you saved before)");
            Console.WriteLine("5. Exit (I'll pretend I'll miss you)");

            Console.Write("Your move: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteNewEntry();
                    break;
                    
                case "2":
                    journal.ReadOldEntries();
                    break;
                    
                case "3":
                    Console.Write("Filename? (default: 'my_thoughts.txt'): ");
                    string saveFile = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(saveFile))
                    {
                        saveFile = "my_thoughts.txt"; 
                    }
                    journal.SaveEntries(saveFile);
                    break;
                    
                case "4":
                    Console.Write("Filename to load? (default: 'my_thoughts.txt'): ");
                    string loadFile = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(loadFile))
                    {
                        loadFile = "my_thoughts.txt";
                    }
                    journal.LoadEntries(loadFile);
                    break;
                    
                case "5":
                    Console.WriteLine("\nLater! Remember, writing one sentence still counts!");
                    Console.WriteLine("(Press any key to exit)");
                    Console.ReadKey();
                    return;
                    
                default:
                    Console.WriteLine("Seriously? 1 through 5. It's not rocket science.");
                    break;
            }
        }
    }
}

class Jornal  
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    
    private string[] prompts = {
        "What's one thing that didn't suck today?",
        "Who annoyed you least today?",
        "What random thought has been living in your head rent-free?",
        "What's something you'll probably forget but should remember?",
        "Today's weirdest moment:",
        "What did you actually accomplish today? (Be honest)",
        "What's making you anxious right now?",
        "What food did you eat that you'll regret later?",
        "What's a thought you'd never say out loud?"
    };
    
    public void WriteNewEntry()
    {
        var rnd = new Random();
        string prompt = prompts[rnd.Next(prompts.Length)];
        
        Console.WriteLine($"\n{prompt}");
        Console.WriteLine("(Type whatever, hit Enter when done. Or don't.)");
        Console.Write("> ");
        
        string content = Console.ReadLine();
        
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        
        entries.Add(new JournalEntry(prompt, content, date));
        
        Console.WriteLine("\nCool. Saved. Was that so hard?");
    }
    
    public void ReadOldEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("Nothing here yet. Like my motivation on Mondays.");
            return;
        }
        
        Console.WriteLine("\nYour Previous Wisdom (or lack thereof):");
        Console.WriteLine("----------------------------------------");
        
        for (int i = entries.Count - 1; i >= 0; i--)
        {
            entries[i].Print();
            Console.WriteLine("---");
        }
    }
    
    public void SaveEntries(string filename)
    {
        try
        {
            using (var writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Content}");
                }
            }
            Console.WriteLine($"\nSaved {entries.Count} entries to '{filename}'");
            Console.WriteLine("(Don't forget where you put it)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nWelp, that failed: {ex.Message}");
            Console.WriteLine("(Maybe try a different filename?)");
        }
    }
    
    public void LoadEntries(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"\nFile '{filename}' not found. Did you save first?");
            Console.WriteLine("(Or maybe you just forgot where you put it)");
            return;
        }
        
        try
        {
            entries.Clear();
            
            foreach (string line in File.ReadAllLines(filename))
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3) 
                {
                    entries.Add(new JournalEntry(parts[1], parts[2], parts[0]));
                }
            }
            
            Console.WriteLine($"\nLoaded {entries.Count} entries from '{filename}'");
            Console.WriteLine("(Try not to cringe at your past self)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nFailed to load: {ex.Message}");
            Console.WriteLine("(Maybe the file got corrupted? Or you're just unlucky)");
        }
    }
}

class JournalEntry
{
    public string Prompt { get; }
    public string Content { get; }
    public string Date { get; }
    
    public JournalEntry(string prompt, string content, string date)
    {
        Prompt = prompt;
        Content = content;
        Date = date;
    }
    
    public void Print()
    {
        Console.WriteLine($"On {Date}");
        Console.WriteLine($"Q: {Prompt}");
        Console.WriteLine($"A: {Content}");
    }
}
