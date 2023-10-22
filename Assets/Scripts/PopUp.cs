using UnityEngine;

public class PopUp : MonoBehaviour
{
    class AlertViewCallBack : AndroidJavaProxy
    {
        private System.Action<int> alertHandler = null;

        public AlertViewCallBack(System.Action<int> alertHandlerIn) : base(packName + "." + loggerClassName + "$AlertViewCallBack")
        {
            alertHandler = alertHandlerIn;
        }

        public void OnButtonTapped(int index)
        {
            Debug.Log("Button tapped: " + index);
            alertHandler?.Invoke(index);
        }
    }

    private const string packName = "com.ramosmarin.mylibrary";
    private const string loggerClassName = "PopUp";

    private static AndroidJavaClass popupController = null;
    private static AndroidJavaObject popupControllerInstance = null;

    private string title = "TitleText";
    private string message = "MessageText";
    private string button1 = "Close";

    public void ShowPopup()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (popupControllerInstance == null)
        {
            Init();
        }
        ShowAlertDialog(new string[] { title + Application.version, message, button1 }, (int obj) =>
        {
            Debug.Log("Local Handler called: " + obj);
        });
#endif
    }

    public void ShowAlertDialog(string[] strings, System.Action<int> handler = null)
    {
        if (strings.Length < 3)
        {
            Debug.LogError("AlertView requires at least 3 strings");
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            popupControllerInstance?.Call("ShowAlertView", new object[] { strings, new AlertViewCallBack(handler) });
        }
        else
        {
            Debug.LogWarning("AlertView not supported on this platform");
        }
    }

    private static void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        popupController = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        popupController.SetStatic("mainActivity", activity);

        popupControllerInstance = popupController.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }
}