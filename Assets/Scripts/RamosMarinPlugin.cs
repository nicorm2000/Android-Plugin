using UnityEngine;

public class RamosMarinPlugin : MonoBehaviour
{
#if UNITY_ANDROID
    #region PRIVATE_FIELDS
    private AndroidJavaClass unityClass;
    private AndroidJavaObject unityActivity;
    private AndroidJavaObject pluginInstance;
    #endregion

    #region UNITY_CALLS
    void Start()
    {
        InitializePlugin("com.ramosmarin.mylibrary.RamosMarinPlugin");
    }
    #endregion

    #region PUBLIC_METHODS
    public void Toast()
    {
        if (pluginInstance != null)
        {
            pluginInstance.Call("Toast", "Hello user!");
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void InitializePlugin(string pluginName)
    {
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        pluginInstance = new AndroidJavaObject(pluginName);
        if (pluginInstance == null)
        {
            Debug.Log("Failed to initialize plugin");
        }
        else
        {
            pluginInstance.CallStatic("receiveUnityActivity", unityActivity);
        }

    }
    #endregion
#endif
}