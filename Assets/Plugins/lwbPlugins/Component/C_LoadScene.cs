/*Name:		 				C_LoadScene	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace W
{
    public class C_LoadScene : MonoBehaviour
    {

        public string sceneName;
        public float progressValue;
        public Text progress;

        private AsyncOperation async = null;
        IEnumerator LoadScene(string SceneName)
        {
            yield return new WaitForEndOfFrame();
            async = SceneManager.LoadSceneAsync(SceneName);
            async.allowSceneActivation = false;
            Debug.Log(async.progress);
            while (!async.isDone)
            {

                Debug.Log(async.progress);
                if (async.progress < 0.9f)
                    progressValue = async.progress;
                else
                    progressValue = 1.0f;

                progress.text = progressValue.ToString();

                if (progressValue >= 0.9)
                {
                    async.allowSceneActivation = true;
                    //progress.text = "按任意键继续";
                    //if (Input.anyKeyDown)
                    //{
                    //}
                }
                yield return null;
            }
        }
    }
}