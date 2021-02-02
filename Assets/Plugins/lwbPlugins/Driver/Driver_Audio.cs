/*Name:		 				AudioEventDriver
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public delegate void OnUpdateEventHandle(float percentage);
public delegate void OnCompleteEventHandle();
public class Driver_Audio : MonoBehaviour
{
    public OnUpdateEventHandle mOnUpdate = null;
    public OnCompleteEventHandle mOnComplete = null;

    private AudioSource mAudioSource = null;

    public void OnUpdate(AudioSource audioSource, OnUpdateEventHandle handler)
    {
        mAudioSource = audioSource;
        mOnUpdate = handler;
    }
    public void OnComplete(AudioSource audioSource, OnCompleteEventHandle handler)
    {
        mAudioSource = audioSource;
        mOnComplete = handler;
    }

    //有可能那一帧太慢了，没检测到time没能>=leghth
    float time提前 = 0.05f;
    private void Update()
    {
        if (mOnUpdate != null && mAudioSource != null)
        {
            if (mAudioSource.isPlaying)
            {
                mOnUpdate(mAudioSource.time / mAudioSource.clip.length);
            }
        }
        if (mOnComplete != null && mAudioSource.time >= mAudioSource.clip.length - time提前)
        {
            mAudioSource.time = 0;
            mAudioSource.Stop();
            //YYS.Ins.Notify("音频结束");
            //Debug.Log("音频结束");
            mOnComplete();
        }
    }
}


public static class AudioSourceExtention
{
    private static Driver_Audio GetDriver(AudioSource audio)
    {

        Driver_Audio driver = audio.GetComponent<Driver_Audio>();
        if (driver == null)
        {
            driver = audio.gameObject.AddComponent<Driver_Audio>();
        }
        return driver;
    }
    public static AudioSource OnUpdate(this AudioSource audio, OnUpdateEventHandle handler)
    {
        GetDriver(audio).OnUpdate(audio, handler);
        return audio;
    }
    public static AudioSource OnComplete(this AudioSource audio, OnCompleteEventHandle handler)
    {
        GetDriver(audio).OnComplete(audio, handler);
        return audio;
    }
}
