using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace W
{
    public class C_随机滑动的UI : MonoBehaviour
    {
        public Vector3[] localPath = new Vector3[] {
            new Vector3(321.26f,0,0),
            new Vector3(-14f,-169f,0),
            new Vector3(-316f,11,0),
            new Vector3(-35f,170,0), };
        Tween tweenr;
        public void OnEnable()
        {
            Play();
        }


        public void Stop()
        {
            if (tweenr != null)
            {
                tweenr.Pause();
                tweenr.Kill();
            }
        }
        public void Pause()
        {
            if (tweenr != null)
            {
                tweenr.Pause();
            }
        }
        void Play()
        {
            tweenr = transform.DOLocalPath(localPath, 30).SetOptions(true);
            tweenr.SetLoops(-1);
            tweenr.SetEase(Ease.Linear);
        }

    }
}