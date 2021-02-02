using UnityEngine;
using UnityEngine.UI;
using BaiduAI;
using System;

/// <summary>
/// 语言识别
/// </summary>
public class BaiduAsr : MonoBehaviour
{
    #region Ins
    private static BaiduAsr _instance;
    public static BaiduAsr Ins
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    string APIKey = "bLDR54fGzNnhxFMqRputdQUz";
    string SecretKey = "FdtAMaYINOw4cLz5YGexkaGQHrVgExIP";
    private AudioClip _clipRecord;
    private Asr _asr;

    // Microphone is not supported in Webgl
#if !UNITY_WEBGL

    void Start()
    {
        _asr = new Asr(APIKey, SecretKey);
        StartCoroutine(_asr.GetAccessToken());
    }

    public void StartRecord()
    {
        _clipRecord = Microphone.Start(null, false, 30, 16000);
        if (Eve开始录音 != null)
        {
            Eve开始录音();
        }
    }
    public string lastContent = "";
    public void StopAndGetResult()
    {
        string result = "";
        Microphone.End(null);
        var data = Asr.ConvertAudioClipToPCM16(_clipRecord);
        StartCoroutine(_asr.Recognize(data, s =>
        {
            result = s.result != null && s.result.Length > 0 ? s.result[0] : "未识别到声音";
            lastContent = result;
           if(Eve解析完成!=null&& result!= "未识别到声音")
            {
                Eve解析完成(result);
            }
           else if (Eve未检测到声音!=null&&result == "未识别到声音")
            {
                Eve未检测到声音();
            }
            //Debug.Log("结束录音：" + result);
        }));
        if(Eve结束录音!=null)
        {
            Eve结束录音();
        }
    }
    public event Action<string> Eve解析完成;
    public event Action Eve未检测到声音;
    public event Action Eve开始录音;
    public event Action Eve结束录音;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        StartRecord();
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        StopAndGetResult();
    //    }
    //}


#endif
}