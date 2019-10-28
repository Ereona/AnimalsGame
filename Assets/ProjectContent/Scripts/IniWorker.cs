using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IniWorker
{
    public static int ReadIntValue(string key, int defaultValue)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void WriteIntValue(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static string ReadStringValue(string key, string defaultValue)
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public static void WriteStringValue(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
}
