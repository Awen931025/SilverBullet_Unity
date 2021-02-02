/*Name:		 				C_MonitorScreen
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-10-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_MonitorScreen : MonoBehaviour
    {

        public List<RawImage> rawImageS = new List<RawImage>();
        public List<int> textureIndex = new List<int>();
        [Header("子物体的图片随机播放这里边的图片，但不重复")]
        public List<Texture> textureS = new List<Texture>();

        Transform 四分屏;
        Transform 单个屏;
        private void Awake()
        {
            ObjInit();
        }
        void ObjInit()
        {
            四分屏 = transform.GetChild(0);
            单个屏 = transform.GetChild(1);
            rawImageS = U_List.ArrayToList(四分屏.GetComponentsInChildren<RawImage>(true));
        }
        void ShowOneTime()
        {
            int maxValue = textureS.Count;
            int minValue = 0;
            textureIndex.Clear();
            foreach (var item in rawImageS)
            {
                int index = Random.Range(minValue, maxValue);
                while (textureIndex.Contains(index))
                {
                    index = Random.Range(minValue, maxValue);
                }
                item.texture = textureS[index];
                textureIndex.Add(index);
            }
        }
        public float changeInterval = 3;
        float timer;
        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = changeInterval;
                ShowOneTime();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ShowOneTime();
            }
        }

        public void SetOpen()
        {
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);

        }
        public void SetClose()
        {
            if (gameObject.activeInHierarchy)
                gameObject.SetActive(false);
        }

        public void To只展示一个摄像头()
        {
            gameObject.SetActive(true);
            单个屏.gameObject.SetActive(true);
            四分屏.gameObject.SetActive(false);
        }

        public void To四分屏切换()
        {
            四分屏.gameObject.SetActive(true);
            单个屏.gameObject.SetActive(false);
        }
    }
}