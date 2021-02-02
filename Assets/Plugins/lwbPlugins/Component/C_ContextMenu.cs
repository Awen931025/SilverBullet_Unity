/*Name:		 				DC_ContextMenu	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace W
{
    public class C_ContextMenu : MonoBehaviour
    {
        [Separator("Collider")]
        public bool addColliderAwake = false;
        [InspectorButton("AddBox")]
        public string 添加BoxCollider = "";
        [InspectorButton("AddMesh")]
        public string 添加MeshCollider = "";
        [InspectorButton("MeshColliderToConvex")]
        public string MeshCollider设置为Convex = "";
        [InspectorButton("RemoveAllInvalidCollider")]
        public string 清除所有没用的Collider = "";
        [InspectorButton("RemoveCollider")]
        public string 删除所有Collider = "";

        [Separator("LodGroup")]
        public bool addLodOnAwake = false;
        [Range(0, 1)]
        public float firstRate = 0.01f;
        [InspectorButton("CreateChildrenLodGroup")]
        public string 添加Lod = "";
        [InspectorButton("RemoveAllLod")]
        public string 删除所有Lod = "";

        [Separator("Outline")]
        public bool addOutlineAwake = false;
        [Header("是否激活Outline")]
        public bool outLineShow = false;

        public void MeshColliderToConvex()
        {
            MeshCollider[] renderers = transform.GetComponentsInChildren<MeshCollider>(true);
            int i = 0;
            foreach (MeshCollider item in renderers)
            {
                i++;
                item.convex = true;
                Debug.Log("设置了 " + i + " 个convex");
            }
        }
        private void Awake()
        {
            Init();
        }

        void IEInitCollider()
        {
            if (!ieAddCollider)
                return;
            StartCoroutine(IEInit());
        }

        void Init()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            int j = 0;
            int h = 0;

            IEInitCollider();

            foreach (Renderer ren in renderers)
            {
                if (addOutlineAwake && ren.GetComponent<OutlineObj>() == null)
                {
                    i++;
                    ren.gameObject.AddComponent<OutlineObj>();
                    ren.GetComponent<OutlineObj>().enabled = outLineShow;
                }

                if (addColliderAwake && ren.GetComponent<MeshCollider>() == null)
                {
                    j++;
                    //MeshCollider collider= 
                    ren.gameObject.AddComponent<MeshCollider>();
                    //collider.enabled = false;
                }
                if (addLodOnAwake && ren.GetComponent<LODGroup>() == null)
                {
                    h++;
                    LODGroup group = ren.gameObject.AddComponent<LODGroup>();
                    LOD[] lods = new LOD[1];
                    lods[0] = new LOD(firstRate, new Renderer[] { ren });
                    group.SetLODs(lods);
                }
            }
            if (i + j + h > 0)
            {
                Debug.Log("添加了  " + i + "   个Outline；   " + j + "   个MeshCollider；  " + h + "个LodGroup；");

            }
        }





        [ContextMenu("给所有带render的加上lod")]
        public void CreateChildrenLodGroup()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            foreach (Renderer ren in renderers)
            {
                i++;
                LODGroup group = ren.gameObject.GetComponent<LODGroup>();
                if (group == null)
                    group = ren.gameObject.AddComponent<LODGroup>();
                LOD[] lods = new LOD[1];
                lods[0] = new LOD(firstRate, new Renderer[] { ren });
                group.SetLODs(lods);
            }
            Debug.Log("添加了  " + i + "   个LodGroup");
        }

        [ContextMenu("删掉所有Lod")]
        public void RemoveAllLod()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            foreach (Renderer ren in renderers)
            {
                if (ren.GetComponent<LODGroup>() != null)
                {
                    i++;
                    DestroyImmediate(ren.GetComponent<LODGroup>());
                }
            }
            Debug.Log("移除了  " + i + "   个LodGroup");
        }
        [ContextMenu("添加高光")]
        public void A_Outline()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            foreach (Renderer ren in renderers)
            {
                if (ren.GetComponent<OutlineObj>() == null)
                {
                    i++;
                    ren.gameObject.AddComponent<OutlineObj>();
                    ren.GetComponent<OutlineObj>().enabled = outLineShow;
                }
            }
            Debug.Log("添加了  " + i + "   个Outline");
        }



        [ContextMenu("删除高光")]
        public void D_Outline()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            foreach (Renderer ren in renderers)
            {
                if (ren.GetComponent<OutlineObj>() != null)
                {
                    i++;
                    DestroyImmediate(ren.GetComponent<OutlineObj>());
                }
            }
            Debug.Log("删除了  " + i + "   个Outline");
        }


        public void RemoveAllInvalidCollider()
        {
            Transform[] renderers = transform.GetComponentsInChildren<Transform>(true);
            int i = 0;
            foreach (Transform ren in renderers)
            {
                if (ren.GetComponent<MeshFilter>() == null)
                {
                    if (ren.GetComponent<MeshCollider>() != null)
                    {
                        i++;
                        DestroyImmediate(ren.gameObject.GetComponent<MeshCollider>());
                    }
                }
            }
            Debug.Log("删除了  " + i + "   个没用的MeshCollider");
        }


        public void RemoveCollider()
        {
            Transform[] renderers = transform.GetComponentsInChildren<Transform>(true);
            int i = 0;
            foreach (Transform ren in renderers)
            {
                if (ren.GetComponent<Collider>() != null)
                {
                    i++;
                    DestroyImmediate(ren.gameObject.GetComponent<Collider>());
                }
            }
            Debug.Log("删除了  " + i + "   个Collider");
        }


        public void AddBox()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            foreach (Renderer ren in renderers)
            {
                if (ren.GetComponent<BoxCollider>() == null)
                {
                    i++;
                    ren.gameObject.AddComponent<BoxCollider>();
                }
            }
            Debug.Log("添加了  " + i + "   个BoxCollider");
        }


        public void AddMesh()
        {
            //mfy-修改为只给有MeshFilter的加meshcollider，别的加也没用
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int i = 0;
            foreach (Renderer ren in renderers)
            {
                if (ren.GetComponent<MeshCollider>() == null)
                {
                    i++;
                    ren.gameObject.AddComponent<MeshCollider>();
                }
            }
            //Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            //int i = 0;
            //foreach (Renderer ren in renderers)
            //{
            //    if (ren.GetComponent<MeshCollider>() == null)
            //    {
            //        i++;
            //        ren.gameObject.AddComponent<MeshCollider>();
            //    }
            //}
            Debug.Log("添加了  " + i + "   个MeshCollider");
        }



        [Header("协程加载Collider")]
        public bool ieAddCollider = false;
        [Header("每帧加多少个")]
        public int countPreSecond = 100;
        IEnumerator IEInit()
        {
            Renderer[] renderers = transform.GetComponentsInChildren<Renderer>(true);
            int geshu = 0;

            int zhenshu = 0;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (i % countPreSecond == 0)
                {
                    yield return null;
                    zhenshu++;
                    //yield return new WaitForSeconds(9);
                }
                //yield return new WaitForEndOfFrame();
                //yield return new WaitForFixedUpdate();
                //yield return new wait();

                if (zhenshu == 3)
                {

                    zhenshu = 0;

                    if (renderers[i].GetComponent<MeshCollider>() == null)
                    {
                        geshu++;
                        Debug.Log("加一个");
                        renderers[i].gameObject.AddComponent<MeshCollider>();
                        //Debug.Log(renderers[i]);
                        //ren.gameObject.AddComponent<BoxCollider>();
                    }
                }


            }
            Debug.Log("协程加载了    " + geshu + "   个MeshCollider");
        }
    }
}