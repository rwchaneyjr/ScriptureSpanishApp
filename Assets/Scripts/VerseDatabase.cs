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
    public static VerseRecord[] Load()
    {
        var asset = Resources.Load<TextAsset>("verses");
        if (asset == null) { Debug.LogError("Verse database missing: Assets/Resources/verses.json"); return Array.Empty<VerseRecord>(); }
        var collection = JsonUtility.FromJson<VerseCollection>(asset.text);
        if (collection == null || collection.verses == null) { Debug.LogError("Verse database is invalid JSON."); return Array.Empty<VerseRecord>(); }
        return collection.verses;
    }
}
