#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using UnityEngine;

public class CIBuilder
{
    public static void BuildGame()
    {
        string[] scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        if (scenes.Length == 0)
        {
            Debug.LogError("====== LOI CI/CD: CHUA ADD SCENE VAO BUILD SETTINGS! ======");
            EditorApplication.Exit(1); // Ép GitHub báo lỗi màu đỏ
        }

        BuildPipeline.BuildPlayer(scenes, "build/StandaloneWindows64/Game.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("====== CI/CD: DONG GOI THANH CONG! ======");
        EditorApplication.Exit(0);
    }
}
#endif