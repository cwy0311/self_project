/*
 * Internationalization 
 * 
 * 1. Add this File to you Project
 * 
 * 2. Add the language files to the folder Assets/Resources/I18n. (Filesnames: en.txt, es.txt, pt.txt, de.txt, and so on)
 *    Format: en.txt:           es.txt:
 *           =============== =================
 *           |hello=Hello  | |hello=Hola     |
 *           |world=World  | |world=Mundo    |
 *           |...          | |...            |
 *           =============== =================
 *           
 * 3. Use it! 
 *    Debug.Log(I18n.Fields["hello"] + " " + I18n.Fields["world"]); //"Hello World" or "Hola Mundo"
 * 
 * Use \n for new lines. Fallback language is "en"
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class I18n
{
    /// <summary>
    /// Text Fields
    /// Useage: Fields[key]
    /// Example: I18n.Fields["world"]
    /// </summary>
    public static Dictionary<String, String> Fields { get; private set; }

    /// <summary>
    /// Init on first use
    /// </summary>
    static I18n()
    {
        LoadLanguage();
    }

    /// <summary>
    /// Load language files from ressources
    /// </summary>
    private static void LoadLanguage()
    {
        if (Fields == null)
            Fields = new Dictionary<string, string>();

        Fields.Clear();
        string lang = Get2LetterISOCodeFromSystemLanguage().ToLower();
        //lang = "de";
        var textAsset = Resources.Load(@"I18n/" + lang); //no .txt needed
        if (textAsset == null)
            textAsset = Resources.Load(@"I18n/en") as TextAsset; //no .txt needed
        if (textAsset == null)
            Debug.LogError("File not found for I18n: Assets/Resources/I18n/" + lang + ".txt");
        string[] lines = (textAsset as TextAsset).text.Split(new string[] { "\r\n", "\n" },
            StringSplitOptions.None);
        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, lines[i].IndexOf("="));
                value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                        lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Fields.Add(key, value);
            }
        }
    }

    /// <summary>
    /// get the current language
    /// </summary>
    /// <returns></returns>
    public static string GetLanguage()
    {
        return Get2LetterISOCodeFromSystemLanguage().ToLower();
    }

    /// <summary>
    /// Helps to convert Unity's Application.systemLanguage to a 
    /// 2 letter ISO country code. There is unfortunately not more
    /// countries available as Unity's enum does not enclose all
    /// countries.
    /// </summary>
    /// <returns>The 2-letter ISO code from system language.</returns>
    public static string Get2LetterISOCodeFromSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;
        string res = "EN";
        switch (lang)
        {
            case SystemLanguage.Chinese: res = "TW"; break;
            case SystemLanguage.ChineseSimplified: res = "CN"; break;
            case SystemLanguage.ChineseTraditional: res = "TW"; break;
            case SystemLanguage.English: res = "EN"; break;
        }
        // Debug.Log("Lang: " + res);
        return res;
    }

}