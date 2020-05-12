using UnityEngine;
using System.IO;

public class AndroidPathManager : MonoBehaviour
{
    public static string SampleFilePath
    {
        get
        {
            return Path.Combine(GetFriendlyFilesPath(), "Path/To/File");
        }
    }

    public static string SampleCachePath
    {
        get
        {
            return Path.Combine(GetFriendlyCachePath(), "Path/To/Cache");
        }
    }

    public static string GetFriendlyCachePath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject applicationContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject path = applicationContext.Call<AndroidJavaObject>("getCacheDir");
        string filesPath = path.Call<string>("getCanonicalPath");
        return filesPath;
#else
        return Application.temporaryCachePath;
#endif
    }

    public static string GetFriendlyFilesPath()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject applicationContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject path = applicationContext.Call<AndroidJavaObject>("getFilesDir");
        string filesPath = path.Call<string>("getCanonicalPath");
        return filesPath;
#else
        return Application.persistentDataPath;
#endif
    }
}