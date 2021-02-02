using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace W
{
    public class C_PathManager : MonoBehaviour
    {
        public Transform player;
        public List<C_Path> cPathS = new List<C_Path>();
        [HideInInspector]
        public C_Path curPath;
        private void Start()
        {
            if (player == null)
            {
                //player = WVR.Ins.obj.cameraRig;
            }
        }

        [InspectorButton("GetSonPath")]
        public string 获取子物体路径 = "";
        /// 上边调用了
        public void GetSonPath()
        {
            cPathS.Clear();
            cPathS.AddRange(transform.GetComponentsInChildren<C_Path>());
        }
        /// 结束
        public void StopPath()
        {
            //原工程
            //WVR_Methord.OpenConsPointer_R();
            player.DOKill();
            stopStepIndex = 0;
        }
        /// 暂停
        public void PausePath()
        {
            //原工程
            //WVR_Methord.OpenConsPointer_R();
            player.DOPause();
        }
        /// 继续
        public void ContinuePath()
        {
            //原工程
            //WVR_Methord.CloseConsPointer_R();
            //try
            //{
            //    OutlineCamera.Constant(showEdEquip.transform, false);
            //}
            //catch { }
            player.DOPlay();
        }

        /// 通过儿子的名称
        public void StartPath(string songName)
        {
            //原工程
            //WVR_Methord.CloseConsPointer_R();
            stopStepIndex = 0;
            Transform son = transform.Find(songName);
            if (son == null)
            {
                return;
            }
            C_Path path = son.GetComponent<C_Path>();
            if (path == null)
            {
                return;
            }
            StartPath(path);
        }
        /// 重头开始一条路径
        public void StartPath(C_Path cPath)
        {
            Vector3[] posS = cPath.GetPath();
            curPath = cPath;
            float duration = cPath.duration;
            bool close = cPath.closePath;
            float delay = cPath.delay;
            int loop = cPath.loop;
            float lookAhead = cPath.lookAhead;
            curPath = cPath;
            //先按路径点算着，相信dotween的优化
            SetPath(posS, duration, close, delay, loop, lookAhead);
            //SetPath根据位置点灵活的选择看向(posS, duration, close);
            //if (cPath.lookFor == null)
            //{
            //SetPath(posS, duration, close, delay, loop, lookAhead);
            //}
            //else
            //{
            //    SetPath(posS, duration, close, cPath.lookFor, delay, loop);
            //}
        }

        public int stopStepIndex;
        //原工程 //原工程
        //private DC_EquipObj showEdEquip;

        public void OnStep(int step)
        {
            if (step < 1)
                return;
            if (curPath.stopList[step] == true)
            {
                stopStepIndex++;
                //原工程
                //DCS.Ins.ToWalkMode();
                //DC_EquipObj eq = curPath.pointS[step].target.GetComponent<DC_EquipObj>();
                //原工程
                //DCS.Ins.equipMenu.AutoShow(eq);
                //DCS.Ins.cameraRig.LookAt(eq.transform);
                //showEdEquip = eq;
                //OutlineCamera.Constant(eq.transform, true);
                PausePath();
            }
        }

        void SetPath(Vector3[] posS, float duration, bool close, float delay = 0, int loop = -1, float lookAhead = 0.2f)
        {
            player.DOKill();
            player.position = posS[0];
            player.DOPath(posS, duration, PathType.Linear)
                .OnWaypointChange(OnStep)
                .SetOptions(close, AxisConstraint.None, AxisConstraint.None)
                .SetLookAt(lookAhead)
                .SetLoops(loop)
                .SetEase(Ease.Linear)
          ;
        }
        void SetPath(Vector3[] posS, float duration, bool close, Transform lookFor, float delay = 0, int loop = -1)
        {
            player.DOKill();
            player.position = posS[0];
            player.DOPath(posS, duration, PathType.Linear)
                  .OnWaypointChange(OnStep)
                 .SetOptions(close, AxisConstraint.None, AxisConstraint.None)
                 .SetLookAt(lookFor)
                 .SetLoops(loop)
                 .SetEase(Ease.Linear)
           ;
        }
    }
}