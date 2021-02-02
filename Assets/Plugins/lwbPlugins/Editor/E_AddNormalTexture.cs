using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace W
{
#endif
    public class E_AddNormalTexture : MonoBehaviour
    {
        public enum TextureType { _MainTex, _BumpMap, _MetallicGlossMap, _Cube }
        List<Material> matS = new List<Material>();
        List<string> textureS = new List<string>();
        List<Texture> tempTex = new List<Texture>();
        private Material tempMat;

        //_N    _NRM    _NORMAL
        public string form = ".jpg";
        public List<string> siffixS = new List<string>() { "_N", "_NRM", "_NORMAL" };
        public string path = "测试.fbm";
        public TextureType textureType = TextureType._BumpMap;


        [Header("金属效果")]
        public float matalicValue = 0.5f;
        [Header("反光效果")]
        public float smoothnessValue = 0.4f;
        [Header("法线深度")]
        public float normalValue = 1;

        [InspectorButton("ImportBySuffix")]
        public string 加载 = "";


#if UNITY_EDITOR
        public void ImportBySuffix()
        {
            foreach (string suffix in siffixS)
            {

                if (matS != null)
                { matS.Clear(); }
                if (textureS != null)
                { textureS.Clear(); }
                Object[] selection = Selection.GetFiltered(typeof(Material), SelectionMode.Editable | SelectionMode.TopLevel);
                if (selection.Length == 0) return;
                foreach (Material mat in selection)
                {
                    if (mat.mainTexture != null)
                    {
                        matS.Add(mat);

                        string matName;
                        if (mat.name.Contains("_"))
                        {
                            string[] strS = U_String.SplitString(mat.name, "_");
                            string last_Name = "_" + strS[strS.Length - 1];
                            matName = mat.name.Replace(last_Name, "");
                        }
                        else
                        {
                            matName = mat.name;
                        }
                        textureS.Add(path + "/" + matName + suffix + form);
                    }
                    else
                    {
                        matS.Add(null);
                        textureS.Add("0");
                    }
                }
                for (int i = 0; i < matS.Count; i++)
                {
                    if (!textureS[i].Equals("0"))
                    {
                        TextureImporter texture = (TextureImporter)AssetImporter.GetAtPath("Assets/Resources" + "/" + textureS[i]);
                        if (texture != null)
                        {
                            texture.textureType = TextureImporterType.NormalMap;
                            AssetDatabase.ImportAsset("Assets/Resources" + "/" + textureS[i]);
                            string picName = textureS[i].Replace(".jpg", "");
                            Texture temp = Resources.Load(picName) as Texture;
                            matS[i].SetTexture(textureType.ToString(), temp);
                            matS[i].SetTexture(textureType.ToString(), temp);
                            matS[i].SetFloat("_Metallic", matalicValue);
                            matS[i].SetFloat("_Glossiness", smoothnessValue);
                            try
                            {
                                matS[i].SetFloat("_BumpScale", normalValue);
                            }
                            catch
                            {
                                Debug.Log("没有法线贴图");
                            }
                        }
                    }
                }
                AssetDatabase.Refresh();
            }
        }
#endif
    }
}