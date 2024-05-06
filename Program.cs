using ConsoleUI;
using System;
using System.IO;


string dir0;
string dir1;


MainMenu();


void MainMenu()
{
    bool active = true;
    while (active)
    {
        dir0 = "";
        dir1 = "";

        UI.Header("Main Menu");
        UI.Write("Compare the contents of two folders");
        UI.Write("");
        UI.Write("Enter path of first directory...");

        string tempDir0 = Input.GetDirectory();

        if (tempDir0 != "")
        {
            dir0 = tempDir0;

            UI.Header("Compare to...");
            UI.Write("Enter path of second directory...");
            string tempDir1 = Input.GetDirectory();

            if (tempDir1 != "")
            {
                dir1 = tempDir1;

                CompareContents();
            }
        }
    }
}


void CompareContents()
{
    string[] files0 = Directory.GetFiles(dir0);
    string[] files1 = Directory.GetFiles(dir1);

    Dictionary<string, FileEntry> results = new();

    foreach (string file in files0)
    {
        string path = file;
        string name = Path.GetFileName(path);

        FileEntry entry = new();

        entry.path = path;
        entry.name = name;
        entry.count = 1;

        results[name] = entry;
    }
    
    foreach (string file in files1)
    {
        string path = file;
        string name = Path.GetFileName(path);

        FileEntry entry = new();

        entry.path = path;
        entry.name = name;

        if (results.ContainsKey(name))
        {
            results[name].count++;
        }
        else
        {
            results[name] = entry;
        }
    }

    // Duplicate files
    List<string> duplicateFiles = new();

    foreach (string key in results.Keys)
    {
        if (results[key].count > 1)
        {
            duplicateFiles.Add(key);
        }
    }

    // Unique files
    List<string> uniqueFiles0 = new();
    List<string> uniqueFiles1 = new();

    foreach (string key in results.Keys )
    {
        if (results[key].count <= 1)
        {
            string dir = Path.GetDirectoryName(results[key].path);
            if (dir == dir0)
            {
                uniqueFiles0.Add(key);
            }
            else if (dir == dir1)
            {
                uniqueFiles1.Add(key);
            }
            else
            {
                UI.Error("You done fucked up the code something fierce.");
            }
        }
    }


    UI.Header("Done");
    UI.Write("Show duplicates?");
    UI.Option("true", "Yes");
    UI.Option("false", "No");

    bool showDuplicates = Input.GetBoolean(false);

    // DUPLICATE FILES

    UI.Header("Compare Contents");

    UI.Write("");
    UI.Write($"{duplicateFiles.Count} duplicate files found");


    if (showDuplicates)
    {
        UI.Write("");

        foreach (string key in duplicateFiles)
        {
            UI.Write($"\t\"{key}\"");
        }
    }

    UI.Write("");
    UI.Write("");

    UI.Write("---------------------------------------------------");


    // UNIQUE FILES

    UI.Write("");
    UI.Write("");
    UI.Write($"{uniqueFiles0.Count} unique files in \"{dir0}\"");
    UI.Write("");
    foreach (string key in uniqueFiles0)
    {
        UI.Write($"\t\"{key}\"");
    }
    UI.Write("");
    UI.Write("");

    UI.Write("---------------------------------------------------");

    UI.Write("");
    UI.Write("");
    UI.Write($"{uniqueFiles1.Count} unique files in \"{dir1}\"");
    UI.Write("");
    foreach (string key in uniqueFiles1)
    {
        UI.Write($"\t\"{key}\"");
    }

    UI.Write("");
    UI.Write("");

    // TOTALS

    int uniqueCount = uniqueFiles0.Count + uniqueFiles1.Count;

    UI.Write($"{uniqueCount} unique files, {duplicateFiles.Count} duplicates.");
    UI.Write($"Total count: {uniqueCount + duplicateFiles.Count}");


    UI.Pause();

    duplicateFiles.Clear();
    uniqueFiles0.Clear();
    uniqueFiles1.Clear();
}


void ExportReport()
{
    // Bad case of CBF with this one.
}


public class FileEntry
{
    public string path = "";
    public string name = "";
    public int count = 1;
}

