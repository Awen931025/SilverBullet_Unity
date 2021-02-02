using UnityEngine;
using UnityEngine.UI;
using BaiduAI;
using System;

[RequireComponent(typeof(AudioSource))]
/// 语音合成
public class BaiduTts : MonoBehaviour
{
    #region Ins
    private static BaiduTts _instance;
    public static BaiduTts Ins
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    //WondCiSwY0RzHYc7hGS2bFoc
    //NjaCdrsUKPi9xiB6X6F6T46B2TdT8ZcT
    string APIKey = "bLDR54fGzNnhxFMqRputdQUz";
    string SecretKey = "FdtAMaYINOw4cLz5YGexkaGQHrVgExIP";

    private Tts tts;
    public AudioSource audioSource { get; private set; }
    public bool isPlaying { get; set; }

    public bool isNetWorkConnected = true;

    public string content = "欢迎来到智联友道科技有限公司";
    public event Action EventNetWorkError;

    void Start()
    {
        tts = new Tts(APIKey, SecretKey);
        StartCoroutine(tts.GetAccessToken());

        tts.EventNetWorkError += Tts_EventNetWorkError;

        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Tts_EventNetWorkError()
    {
        if(EventNetWorkError!=null)
        {
            EventNetWorkError();
        }
        isNetWorkConnected = false;
    }

    public BaiduTts Play(string content)
    {
        StartCoroutine(tts.Synthesis(content, s =>
        {
            if (s.Success)
            {
                audioSource.clip = s.clip;
                audioSource.Play();

                isPlaying = true;
            }
            else
            {
            }
        }));
        return this;
    }

    public BaiduTts Play(string content, Tts.Pronouncer  voice,int volumn=5)
    {
        StartCoroutine(tts.Synthesis(content, s =>
        {
            if (s.Success)
            {
                audioSource.clip = s.clip;
                audioSource.Play();

                isPlaying = true;
            }
            else
            {
            }
        },
          5,  5, volumn, voice
        )
            
            );
        return this;
    }


    public void Stop()
    {
        if(audioSource != null)
        {
            audioSource.Stop();
        }
    }
    public event Action onComplete;
    void Update()
    {
        if (isPlaying)
        {
            if (!audioSource.isPlaying)
            {
                isPlaying = false;
                if (onComplete != null)
                {
                    onComplete();
                    onComplete = null;
                }
                //Debug.Log("百度语音播放完毕");
            }
        }
    }
}

public static class BaiduTtsExterntion
{
    public static void OnComplete(this BaiduTts tts, Action eventHandle)
    {
        tts.onComplete += eventHandle;
    }
}