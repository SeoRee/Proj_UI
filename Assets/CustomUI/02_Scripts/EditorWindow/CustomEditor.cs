using UnityEditor;
using UnityEngine;

public class CustomEditor : EditorWindow
{
    [MenuItem("GameObject/Custom UI/Button")]
    static void CreateButton()
    {
        Debug.Log("Create button");
    }

    [MenuItem("GameObject/Custom UI/Button with text")]
    static void CreateButtonWithText()
    {
        Debug.Log("Create button with text");
    }
}
