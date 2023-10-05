using TMPro;
using UnityEngine;

public class TestPlugin : MonoBehaviour
{
    public TextMeshProUGUI label;
    public void RunPlugin()
    {
        Debug.Log("Run Plugin");
        label.text = "Test";
    }
}