using System.Collections.Generic;
using UnityEditor;

public class CreateBaseFolder
{
    [MenuItem("Tools/Create Base Folders")]
    static void CreateFolderIfNotExists()
    {
        List<string> folderNames = new List<string> {"02_Scripts", "03_ScriptableObjects", "04_Prefabs", "05_Sprites", "06_Animations", "07_Audios", "08_Fonts" };
        List<string> inScriptsFolderNames = new List<string>{"Framework", "Gameplay"};

        foreach (var folderName in folderNames)
        {
            if (AssetDatabase.IsValidFolder($"Assets/{folderName}")) continue;
            AssetDatabase.CreateFolder("Assets", folderName);
        }

        foreach (var inScriptsFolderName in inScriptsFolderNames)
        {
            if (AssetDatabase.IsValidFolder($"Assets/02_Scripts/{inScriptsFolderName}")) continue;
            AssetDatabase.CreateFolder("Assets/02_Scripts", inScriptsFolderName);
        }

        if (AssetDatabase.IsValidFolder("Assets/Scenes"))
        {
            AssetDatabase.RenameAsset("Assets/Scenes", "01_Scenes");
        }

        AssetDatabase.Refresh();
    }
}
