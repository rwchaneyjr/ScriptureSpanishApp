using System;
using UnityEngine;

[Serializable]
public class VerseRecord
{
    public string id;
    public string reference;
    public string translation;
    public string text;
    public string category;
    public int difficulty;
}

[Serializable]
public class VerseCollection { public VerseRecord[] verses; }

public static class VerseDatabase
{
    public static bool UseOldTestament { get; set; }
    public static bool RandomizeVerses { get; private set; }
    static VerseRecord[] cachedVerses;
    static string cachedFileName;

    public static void SetRandomMode(bool randomize)
    {
        RandomizeVerses = randomize;
        cachedVerses = null;
        cachedFileName = null;
    }

    public static VerseRecord[] Load()
    {
        var fileName = UseOldTestament ? "old_testament_500_web" : "new_testament_web";
        if (cachedVerses != null && cachedFileName == fileName) return cachedVerses;
        var asset = Resources.Load<TextAsset>(fileName);
        if (asset == null) { Debug.LogError("Verse database missing: Assets/Resources/" + fileName + ".json"); return Array.Empty<VerseRecord>(); }
        var collection = JsonUtility.FromJson<VerseCollection>(asset.text);
        if (collection == null || collection.verses == null) { Debug.LogError("Verse database is invalid JSON."); return Array.Empty<VerseRecord>(); }
        cachedVerses = collection.verses;
        cachedFileName = fileName;
        if (RandomizeVerses)
        {
            for (int i = cachedVerses.Length - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);
                var temp = cachedVerses[i]; cachedVerses[i] = cachedVerses[j]; cachedVerses[j] = temp;
            }
        }
        return cachedVerses;
    }
}

