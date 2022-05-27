using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OpenWindow
{
    [MenuItem("Tool/Koyo")] 
    public static void Open()
    {
        var temp = EditorWindow.GetWindow<KoyoWindow>();
    }
}
