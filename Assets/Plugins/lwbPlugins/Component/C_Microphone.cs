/*Name:		 				Test_MKF
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-09-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace W
{
    public class C_Microphone : MonoBehaviour
    {

        #region Ins
        private static C_Microphone _instance;
        public static C_Microphone Ins
        {
            get { return _instance; }
        }
        private void Awake()
        {
            _instance = this;
        }
        #endregion

        AudioSource audioSource;
        bool haveMicroPhone;
        string device;

        void Start()
        {
            Init();
        }

        public bool IsHaveMicroPhone { get { return haveMicroPhone; } }
        void Init()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
            }

            string[] devices = Microphone.devices;

            if (devices.Length > 0)
            {
                haveMicroPhone = true;
                device = devices[0];
            }
            else
            {
                haveMicroPhone = false;
                Debug.LogError("没有检测到麦克风");
            }
        }
        public void StartRecord(bool isPlay = true, float volume = 1, int length = 3599, int frequency = 44100)
        {
            Init();
            if (!haveMicroPhone)
                return;
            audioSource.clip = Microphone.Start(device, true, length, frequency);
            if (isPlay)
            {
                StartPlay(volume);
            }
        }
        public void StopRecord()
        {
            if (!haveMicroPhone)
                return;
            Microphone.End(device);
            StopPlay();
        }


        //开始播放按钮
        public void StartPlay(float volume = 1)
        {
            audioSource.Play();
            audioSource.volume = volume;
            audioSource.timeSamples = Microphone.GetPosition(device);//这里设置了之后就会近乎实时同步
            Microphone.GetDeviceCaps(device, out _, out _);
        }
        public void StopPlay()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}