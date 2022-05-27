using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class KoyoWindow : EditorWindow
{

    static readonly string TemplateTextPath = "KoyoWindow.cs";

    static readonly string[] mvpReadFilePath = new string[3] { "TemplateMono_ModelBase.txt", "TemplateMono_PresenterBase.txt", "TemplateMono_ViewBase.txt" };
    static string[] mvpExportFileNames = new string[3] { string.Empty, string.Empty, string.Empty };

    static string templateFolderPath = string.Empty;
    static string exportFolderPath = string.Empty;
    static string type = string.Empty;
    static string currentSelectAssetPath = string.Empty;

    static string[] systemReadFileRelativePaths = new string[3] { string.Empty, string.Empty, string.Empty };

    private void OnEnable()
    {

        var mono = MonoScript.FromScriptableObject(this);

        var path = AssetDatabase.GetAssetPath(mono).Replace(TemplateTextPath, "");

        templateFolderPath = path + "Template/";
    }

    private void Update()
    {
        CurrentSelectAssetPathUpdate();

        if (IsTypeReplacable())
        {
            MVPTemplateTypeReplace();
            VariableFinalize();
        }
    }

    private void OnInspectorUpdate()
    {
        exportFolderPath = currentSelectAssetPath;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("最強のTool    [ K O Y O ]");

        using (var hori = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("型名");

            type = GUILayout.TextField(type);
        }

        using (var hori = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Model名");

            mvpExportFileNames[0] = GUILayout.TextField(mvpExportFileNames[0]);
        }

        using (var hori = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Presenter名");

            mvpExportFileNames[1] = GUILayout.TextField(mvpExportFileNames[1]);
        }

        using (var hori = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("View名");

            mvpExportFileNames[2] = GUILayout.TextField(mvpExportFileNames[2]);
        }

        using (var hori = new EditorGUILayout.HorizontalScope())
        {
            EditorGUILayout.LabelField("Export先");

            exportFolderPath = GUILayout.TextField(exportFolderPath);
        }


        GUILayout.Space(10);
        bool isPush = GUILayout.Button("生成");

        if (isPush)
        {
            MVPTemplateGenerator();
        }

    }

    private static void MVPTemplateGenerator()
    {
        for (int i = 0; i < 3; i++)
        {
            string readPath = templateFolderPath + mvpReadFilePath[i];
            string writePath = exportFolderPath + mvpExportFileNames[i] + ".cs";
            systemReadFileRelativePaths[i] = writePath;
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(readPath, writePath);
        }

        AssetDatabase.Refresh();
    }

    private void CurrentSelectAssetPathUpdate()
    {
        if (Selection.assetGUIDs == null || Selection.assetGUIDs.Length <= 0) { return; }

        currentSelectAssetPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]) + "/";
    }

    private static bool IsTypeReplacable()
    {
        if (systemReadFileRelativePaths[0].Equals(string.Empty)) { return false; }
        if (systemReadFileRelativePaths[1].Equals(string.Empty)) { return false; }
        if (systemReadFileRelativePaths[2].Equals(string.Empty)) { return false; }

        if (!File.Exists(Path.GetFullPath(systemReadFileRelativePaths[0]))) { return false; }
        if (!File.Exists(Path.GetFullPath(systemReadFileRelativePaths[1]))) { return false; }
        if (!File.Exists(Path.GetFullPath(systemReadFileRelativePaths[2]))) { return false; }

        return true;
    }

    private static void MVPTemplateTypeReplace()
    {
        string tempStr = string.Empty;

        for (int i = 0; i < 3; i++)
        {
            string systemReadPath = Path.GetFullPath(systemReadFileRelativePaths[i]);

            using (StreamReader sr = new StreamReader(systemReadPath, Encoding.GetEncoding("Shift_JIS")))
            {
                tempStr = sr.ReadToEnd();
            }

            tempStr = tempStr.Replace("#TypeName#", type);

            using (StreamWriter sw = new StreamWriter(systemReadPath, false, Encoding.GetEncoding("Shift_JIS")))
            {
                sw.Write(tempStr);
            }
        }

        AssetDatabase.Refresh();
    }

    private static void VariableFinalize()
    {
        for (int i = 0; i < 3; i++)
        {
            mvpExportFileNames[i] = string.Empty;
            systemReadFileRelativePaths[i] = string.Empty;
        }

        templateFolderPath = string.Empty;
        exportFolderPath = string.Empty;
        type = string.Empty;
    }
}
