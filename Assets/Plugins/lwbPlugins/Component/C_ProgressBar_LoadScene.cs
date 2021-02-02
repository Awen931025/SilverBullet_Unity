/*Name:		 				W_LoadSceneWithProgressBar	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2018-07-
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace W
{
    public class C_ProgressBar_LoadScene : C_ProgressBar
    {

        //public string nameOfSceneToLoad = "";
        AsyncOperation operation;
        public string sceneName;
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (operation == null)
                return;
            if (operation.progress >= 0.9f && rate > 0.95f)
            {
                operation.allowSceneActivation = true;
            }
        }
        protected override void Start()
        {
            base.Start();
            StartCoroutine(IAsynToScene());
            isRun = true;
        }
        IEnumerator IAsynToScene()
        {
            operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            yield return operation;
        }

    }
}



//private AsyncOperation async = null;
//IEnumerator LoadScene(string SceneName)
//{
//    yield return new WaitForEndOfFrame();
//    async = SceneManager.LoadSceneAsync(SceneName);
//    async.allowSceneActivation = false;
//    while (!async.isDone)
//    {
//        if (async.progress < 0.9f)
//            progressValue = async.progress;
//        else
//            progressValue = 1.0f;

//        slider.fillAmount = progressValue;
//        progress.text = (int)(slider.fillAmount * 100) + " %";

//        if (progressValue >= 0.9)
//        {
//            async.allowSceneActivation = true;
//            //progress.text = "按任意键继续";
//            //if (Input.anyKeyDown)
//            //{
//            //}
//        }
//        yield return null;
//    }