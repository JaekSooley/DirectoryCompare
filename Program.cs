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
        UI.Header("Main Menu");
        UI.Write("Compare the contents of two folders");
        UI.Write("");

        string tempDir0 = Input.GetDirectory();

        if (tempDir0 != "")
        {
            dir0 = tempDir0;


            UI.Write("Input second directory");
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
    UI.Header("Compare Contents");

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

    // Show duplicate files
    UI.Write("Duplicate Files:");
    int duplicateCount = 0;

    foreach (string key in results.Keys)
    {
        if (results[key].count > 1)
        {
            UI.Write($"\tName: {key}");

            duplicateCount++;
        }
    }

    UI.Write("");
    UI.Write("");

    // Show unique files, and their path
    UI.Write("Unique Files:");
    int uniqueCount = 0;
    foreach (string key in results.Keys)
    {
        if (results[key].count <= 1)
        {
            UI.Write($"\tName: {key} \n\tPath: \"{results[key].path}\"");
            UI.Write("");

            uniqueCount++;
        }
    }

    UI.Write("");
    UI.Write("");

    UI.Write($"Duplicate files: {duplicateCount}");
    UI.Write($"Unique files: {uniqueCount}");

    UI.Pause();
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

