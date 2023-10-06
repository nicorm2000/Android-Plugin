using TMPro;
using UnityEngine;

public class TestPlugin : MonoBehaviour
{
    const string packageName = "com.ramosmarin.mylibrary";
    const string className = packageName + ".RamosMarinLogger";

#if UNITY_ANDROID
    AndroidJavaClass _pluginClass;
    AndroidJavaObject _pluginInstance;

    public TextMeshProUGUI label;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _pluginClass = new AndroidJavaClass(className);
            _pluginInstance = _pluginClass.CallStatic<AndroidJavaObject>("getInstance");
        }
    }

    public void RunPlugin()
    {
        Debug.Log("Run Plugin");
        if (Application.platform == RuntimePlatform.Android)
        {
            label.text = _pluginInstance.Call<string>("getLogTag");
        }
    }
#endif
}