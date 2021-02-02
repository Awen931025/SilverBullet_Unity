using UnityEngine;
using UnityEditor;
namespace W
{

    public class ShortcutKey_LockDir : ScriptableObject
    {
        public const string To_Resource_UI_Prefab = @"W_Select/Resources-UI-prefabs &d";
        [MenuItem(To_Resource_UI_Prefab, false, 0)]
        static void Go_Resource_UI_Prefab()
        {
            Select("Assets/Resources/UI/Prefabs/Tool.prefab");
        }


        public const string To_W_ = @"W_Select/Plugins-W_";
        [MenuItem(To_W_)]
        static void Go_W_()
        {
            Select("Assets/Plugins/W_/W_Enum.cs");
        }

        public const string To_lwb = @"W_Select/lwb";
        [MenuItem(To_lwb)]
        static void Go_lwb()
        {
            Select("Assets/___________________lwb/Confs.cs");
        }


        public const string To_StreamingAssets = @"W_Select/StreamingAssets";
        [MenuItem(To_StreamingAssets)]
        static void Go_To_StreamingAssets()
        {
            Select("Assets/StreamingAssets/Conf");
        }



        public static void Select(string path)
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(path);
            if (!obj) return;
            EditorGUIUtility.PingObject(obj);
            //Selection.activeObject = obj;
        }
    }
}
