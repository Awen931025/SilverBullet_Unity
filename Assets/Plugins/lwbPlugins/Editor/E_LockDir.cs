using UnityEngine;
using UnityEditor;
namespace W
{
    public class E_LockDir : ScriptableObject
    {
        public const string To_W_ = @"W_Select/lwbPlugins";
        [MenuItem(To_W_)]
        static void Go_W_()
        {
            Select("Assets/Plugins/lwbPlugins/Utils");
        }
        public const string To_Assets = @"W_Select/Assets";
        [MenuItem(To_Assets)]
        static void Go_Assets()
        {
            Select("Assets");
        }

        public const string To_Resources = @"W_Select/Resources";
        [MenuItem(To_Resources)]
        static void Go_To_Resources()
        {
            Select("Assets/Resources");
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
        }



        //public const string To_Resource_UI_Prefab = @"W_Select/Resources-UI-prefabs &d";
        //[MenuItem(To_Resource_UI_Prefab, false, 0)]
        //static void Go_Resource_UI_Prefab()
        //{
        //    Select("Assets/Resources/UI/Prefabs/Tool.prefab");
        //}
        //public const string To_lwb = @"W_Select/_lwb";
        //[MenuItem(To_lwb)]
        //static void Go_lwb()
        //{
        //    Select("Assets/_lwb/lwb.unity");
        //}
    }
}
