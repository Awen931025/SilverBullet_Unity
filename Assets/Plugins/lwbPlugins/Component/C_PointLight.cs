/*Name:		 				YY_PointLight
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		yoodao*/
using UnityEngine;
using DG.Tweening;

namespace W
{
    public class C_PointLight : MonoBehaviour
    {
        [Separator("默认是他自己")]
        public Renderer render;
        [Separator("不设置的话，默认变成跟他的颜色相同的光")]
        public Color targetLight;
        Color higLight = new Color32(0, 0, 0, 0);

        public bool openOnAwake = false;
        public bool flashOnAwake = false;
        public float flashDuration = 0.5f;
        private void Awake()
        {
            if (render == null)
            {
                render = GetComponentInChildren<Renderer>();
            }
        }
        private void Start()
        {

            if (targetLight == new Color(0, 0, 0, 0))
            {
                targetLight = render.material.color;
            }
            if (openOnAwake)
            {
                SetOpen();
            }
            if (flashOnAwake)
            {
                SetFlash(flashDuration);
            }
        }
        public void SetColor(Color color)
        {
            targetLight = color;
        }
        void Stop()
        {
            if (render == null)
            {
                render = GetComponentInChildren<Renderer>(true);
            }
            render.material.DOKill();
        }
        public void SetOpen()
        {
            Stop();
            render.material.EnableKeyword("_EMISSION");
            render.material.SetColor("_EmissionColor", targetLight);
        }
        public void SetOpen(float duration)
        {
            Stop();
            render.material.EnableKeyword("_EMISSION");
            render.material.DOColor(targetLight, "_EmissionColor", duration);
        }
        public void SetClose()
        {
            Stop();
            render.material.SetColor("_EmissionColor", Color.clear);
            render.material.DisableKeyword("_EMISSION");
        }
        public void SetClose(float duration)
        {
            Stop();
            render.material.DOColor(Color.clear, "_EmissionColor", duration / 1.5f)
                .OnComplete(() =>
            {
                render.material.EnableKeyword("_EMISSION");
            });
        }
        public void SetFlash()
        {
            int times = 0;
            render.material.EnableKeyword("_EMISSION");
            render.material.DOColor(targetLight, "_EmissionColor", 0.5f).OnComplete(
                () =>
                {
                    render.material.DOColor(Color.clear, "_EmissionColor", 0.5f / 1.5f).OnComplete(
                () =>
                {
                    times++;
                    SetFlash(0.5f);
                });
                });
        }
        public void SetFlash(float duration)
        {
            int times = 0;
            render.material.EnableKeyword("_EMISSION");

            render.material.DOColor(targetLight, "_EmissionColor", duration).OnComplete(
                () =>
                {
                    render.material.DOColor(Color.clear, "_EmissionColor", duration / 1.5f).OnComplete(
                () =>
                {
                    times++;
                    SetFlash(duration);
                });
                });
        }
        //递归+回调
        public void SetFlash(float duration, int times, bool stopOnOpen = true)
        {
            int flashTime = 0;
            render.material.EnableKeyword("_EMISSION");
            render.material.DOColor(targetLight, "_EmissionColor", duration).OnComplete(
                () =>
                {
                    if (stopOnOpen && flashTime > times - 1)
                    {
                        return;
                    }
                    render.material.DOColor(Color.clear, "_EmissionColor", duration / 1.5f).OnComplete(
                        () =>
                        {
                            flashTime++;
                            if (!stopOnOpen)
                            {
                                if (flashTime < times)
                                {
                                    SetFlash(duration, times - flashTime, stopOnOpen);
                                }
                                else
                                {
                                    SetClose();
                                }
                            }
                            else
                            {
                                SetFlash(duration, times - flashTime, stopOnOpen);
                            }
                        });
                });
        }
        void ToTarget(float duration)
        {
            render.material.EnableKeyword("_EMISSION");
            render.material.DOColor(targetLight, "_EmissionColor", duration);
        }
        void ToCloseOri(float duration)
        {
            render.material.DOColor(Color.clear, "_EmissionColor", duration / 1.5f);
        }
    }
}