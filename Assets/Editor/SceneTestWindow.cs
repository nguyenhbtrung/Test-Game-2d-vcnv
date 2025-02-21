using UnityEditor;
using UnityEngine;

public class SceneTestWindow : EditorWindow
{
    private int sceneIndex;

    [MenuItem("Window/Scene Test Settings")]
    public static void ShowWindow()
    {
        GetWindow<SceneTestWindow>("Scene Test Settings");
    }

    private void OnGUI()
    {
        GUILayout.Label("Chọn Scene Index để kiểm tra:", EditorStyles.boldLabel);
        sceneIndex = EditorGUILayout.IntField("Scene Index", sceneIndex);

        if (GUILayout.Button("Lưu"))
        {
            SceneTestSettings.SceneIndex = sceneIndex;
            Debug.Log("Scene Index đã được lưu vào tệp: " + sceneIndex);
        }
    }
}
