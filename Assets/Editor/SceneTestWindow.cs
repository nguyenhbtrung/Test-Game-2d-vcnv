using UnityEditor;
using UnityEngine;
using System.Linq; 

public class SceneTestWindow : EditorWindow
{
    private int sceneIndex = 0;
    private string[] sceneNames;

    [MenuItem("Window/Scene Test Settings")]
    public static void ShowWindow()
    {
        GetWindow<SceneTestWindow>("Scene Test Settings");
    }

    private void OnEnable()
    {
        sceneNames = EditorBuildSettings.scenes
            .Select(scene => System.IO.Path.GetFileNameWithoutExtension(scene.path))
            .ToArray();
    }

    private void OnGUI()
    {
        GUILayout.Label("Chọn Scene để kiểm tra:", EditorStyles.boldLabel);

        if (sceneNames == null || sceneNames.Length == 0)
        {
            EditorGUILayout.HelpBox("Không có scene nào trong Build Settings.", MessageType.Warning);
            return;
        }

        sceneIndex = EditorGUILayout.Popup("Scene", sceneIndex, sceneNames);

        if (GUILayout.Button("Lưu"))
        {
            SceneTestSettings.SceneIndex = sceneIndex;
            Debug.Log("Scene Index đã được lưu: " + sceneIndex);
        }
    }
}
