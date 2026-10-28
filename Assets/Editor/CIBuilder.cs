#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using UnityEngine;

public class CIBuilder
{
    public static void BuildGame()
    {
        // Tự động tìm tất cả các file màn chơi trong dự án
        string[] scenes = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = scenes[i].Replace("\\", "/");
        }

        if (scenes.Length == 0)
        {
            Debug.LogError("====== LOI: KHONG TIM THAY FILE MAN CHOI (.unity) NAO ======");
            EditorApplication.Exit(1);
        }

        BuildPipeline.BuildPlayer(scenes, "build/StandaloneWindows64/Game.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("====== CI/CD: DONG GOI THANH CONG! ======");
        EditorApplication.Exit(0);
    }
}
#endif
