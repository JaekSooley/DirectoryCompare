﻿using ConsoleUI;
using System;
using System.IO;


MainMenu();


void MainMenu()
{
    bool active = true;
    while (active)
    {
        UI.Header("Main Menu");
        UI.Write("Compare the contents of two folders");
        UI.Option("[N]AME", "Compare files by name.");
        UI.Option("[D]ETAILS", "Compare files by size.");
        UI.Option("");
        UI.Option("[S]ETTINGS");
        UI.Write("");

        string input = Input.GetString();

        switch (input.ToUpper())
        {
            case "N":
            case "NAME":
                CompareMenu();
                break;
            case "S":
            case "SETTINGS":
                break;
            default:
                break;
        }
    }
}


void CompareMenu()
{
    string dir0;
    string dir1;

    bool active = true;
    while (active)
    {
        UI.Header("Compare From");
        UI.Write("Enter path of first directory...");

        string input = Input.GetDirectory();

        if (Directory.Exists(input))
        {
            dir0 = input;

            UI.Header("Compare to...");
            UI.Write("Enter path of second directory...");
            string input1 = Input.GetDirectory();

            if (Directory.Exists(input1))
            {
                dir1 = input1;

                List<string> files0 = Directory.GetFiles(dir0).ToList();
                List<string> files1 = Directory.GetFiles(dir1).ToList();

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

                // Get duplicate files
                List<string> duplicateFiles = new();

                foreach (string key in results.Keys)
                {
                    if (results[key].count > 1)
                    {
                        duplicateFiles.Add(key);
                    }
                }

                // Get unique files
                List<string> uniqueFiles0 = new();
                List<string> uniqueFiles1 = new();

                foreach (string key in results.Keys)
                {
                    if (results[key].count <= 1)
                    {
                        string? dir = Path.GetDirectoryName(results[key].path);

                        if (dir != null)
                        {
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
                }


                UI.Header("Done");
                UI.Write("Show duplicates?");
                UI.Option("[Y]ES");
                UI.Option("[N]O");

                bool showDuplicates = true;

                bool loop = true;
                while (loop)
                {
                    string showDuplicatesInput = Input.GetString("Y");

                    switch (showDuplicatesInput.ToUpper())
                    {
                        case "Y":
                        case "YES":
                            showDuplicates = true;
                            loop = false;
                            break;
                        case "N":
                        case "NO":
                            showDuplicates = false;
                            loop = false;
                            break;
                        default:
                            break;
                    }
                }

                // DUPLICATE FILES

                UI.Header("Compare Contents");
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
                active = false;
            }
        }
        else
        {
            active = false;
        }
    }
}


void CompareFileDetails()
{
    string dir0;
    string dir1;

    bool active = true;
    while (active)
    {
        UI.Header("Compare From");
        UI.Write("Enter path of first directory...");

        string input = Input.GetDirectory();

        if (Directory.Exists(input))
        {
            dir0 = input;

            UI.Header("Compare to...");
            UI.Write("Enter path of second directory...");
            string input1 = Input.GetDirectory();

            if (Directory.Exists(input1))
            {
                dir1 = input1;

                List<string> files0 = Directory.GetFiles(dir0).ToList();
                List<string> files1 = Directory.GetFiles(dir1).ToList();

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

                // Get duplicate files
                List<string> duplicateFiles = new();

                foreach (string key in results.Keys)
                {
                    if (results[key].count > 1)
                    {
                        duplicateFiles.Add(key);
                    }
                }

                // Get unique files
                List<string> uniqueFiles0 = new();
                List<string> uniqueFiles1 = new();

                foreach (string key in results.Keys)
                {
                    if (results[key].count <= 1)
                    {
                        string? dir = Path.GetDirectoryName(results[key].path);

                        if (dir != null)
                        {
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
                }


                UI.Header("Done");
                UI.Write("Show duplicates?");
                UI.Option("[Y]ES");
                UI.Option("[N]O");

                bool showDuplicates = true;

                bool loop = true;
                while (loop)
                {
                    string showDuplicatesInput = Input.GetString("Y");

                    switch (showDuplicatesInput.ToUpper())
                    {
                        case "Y":
                        case "YES":
                            showDuplicates = true;
                            loop = false;
                            break;
                        case "N":
                        case "NO":
                            showDuplicates = false;
                            loop = false;
                            break;
                        default:
                            break;
                    }
                }

                // DUPLICATE FILES

                UI.Header("Compare Contents");
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
                active = false;
            }
        }
        else
        {
            active = false;
        }
    }
}


void ExportReport()
{
    // Bad case of CBF with this one.
}


public class FileEntry
{
    public string path = "";
    public string name = "";
    public string size = "";
    public int count = 1;
}

