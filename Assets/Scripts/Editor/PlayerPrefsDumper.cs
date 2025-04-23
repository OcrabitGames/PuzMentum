using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

public class PlayerPrefsDumper
{
    [MenuItem("Tools/Log All PlayerPrefs (Readable)")]
    public static void LogAllPrefsReadable()
    {
        string plistPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
            $"Library/Preferences/unity.{Application.companyName}.{Application.productName}.plist"
        );

        if (!File.Exists(plistPath))
        {
            Debug.LogWarning("PlayerPrefs plist not found.");
            return;
        }

        // Create a temporary file for the XML
        string xmlPath = Path.Combine(Application.temporaryCachePath, "temp_prefs.xml");

        Process plistConvert = new Process();
        plistConvert.StartInfo.FileName = "/usr/bin/plutil";
        plistConvert.StartInfo.Arguments = $"-convert xml1 \"{plistPath}\" -o \"{xmlPath}\"";
        plistConvert.StartInfo.UseShellExecute = false;
        plistConvert.StartInfo.CreateNoWindow = true;
        plistConvert.Start();
        plistConvert.WaitForExit();

        if (!File.Exists(xmlPath))
        {
            Debug.LogError("Failed to convert plist to XML.");
            return;
        }

        string xmlContents = File.ReadAllText(xmlPath);
        Debug.Log($"🧾 PlayerPrefs XML:\n{xmlContents}");
    }
}