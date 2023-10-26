using UnityEngine;

public class FileManager : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text tmp = null;
    [SerializeField] private PopUp popUp = null;

    private const string packName = "com.ramosmarin.mylibrary";
    private const string loggerClassName = "FileManager";

    private AndroidJavaClass fileManager = null;
    private AndroidJavaObject fileManagerInstance = null;

    public void ReadFile()
    {
        if (fileManagerInstance == null)
        {
            Init();
        }
        string txt = fileManagerInstance?.Call<string>("ReadFile");
        tmp.text = txt;
    }

    public void WriteFile(string data)
    {
        if (fileManagerInstance == null)
        {
            Init();
        }
        fileManagerInstance?.Call("WriteFile", data);
    }

    public void DeleteFile()
    {
        popUp.ShowAlertDialog(new string[] { "Are you sure you want to delete logs?", "deleting logs.txt", "Delete", "Cancel" },
            (index) =>
            {
                Debug.Log("Index of button: " + index);
                if (index == -3)
                {
                    if (fileManagerInstance == null)
                    {
                        Init();
                    }
                    fileManagerInstance?.Call("DeleteFiles");
                }
            });
    }

    private void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        fileManager = new AndroidJavaClass(packName + "." + loggerClassName);
        AndroidJavaClass unityJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityJC.GetStatic<AndroidJavaObject>("currentActivity");
        fileManager.SetStatic("mainAct", activity);

        fileManagerInstance = fileManager.CallStatic<AndroidJavaObject>("GetInstance");
#endif
    }
}