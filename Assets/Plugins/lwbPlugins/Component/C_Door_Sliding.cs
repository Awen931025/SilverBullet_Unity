/*Name:		 				C_Door_Sliding
 *Description: 				滑动门
 *Author:       			lwb
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		yoodao*/
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace W
{
    [DisallowMultipleComponent]
    public class C_Door_Sliding : MonoBehaviour
    {
        [Separator("C_Door_Sliding")]
        public Vector3 addedLocalPos = new Vector3(1, 0, 0);
        public float animationDuration = 2.5f;
        public Transform doorL;
        public Transform doorR;
        public Vector3 doorLOriPos;
        public Vector3 doorROriPos;
        public event Action EventOpenEd;
        public event Action EventCloseEd;

        [InspectorButton("OpenDoor")]
        public string 开门;
        [InspectorButton("CloseDoor")]
        public string 关门;
        public bool isOpen { get; private set; }

        public virtual void Awake()
        {

        }
        public virtual void Start()
        {
            if (doorL == null)
            {
                doorL = transform.Find("左扇");
            }
            if (doorR == null)
            {

                doorR = transform.Find("右扇");
            }
            doorLOriPos = doorL.localPosition;
            doorROriPos = doorR.localPosition;
        }

        public virtual void PauseOpeningDoor()
        {
            doorL.DOPause();
            doorR.DOPause();
        }
        public virtual void StopSwitching()
        {
            doorL.DOKill();
            doorR.DOKill();
        }

        public virtual void OpenDoor(bool isHandHard = false)
        {
            doorL.DOLocalMove(doorLOriPos + addedLocalPos, animationDuration);
            doorR.DOLocalMove(doorROriPos - addedLocalPos, animationDuration).OnComplete(() => { isOpen = true; if (EventOpenEd != null) { EventOpenEd(); } });
        }
        public virtual void OpenDoor_瞬间(bool isHandHard = false)
        {
            doorL.localPosition = doorLOriPos + addedLocalPos;
            doorR.localPosition = doorROriPos - addedLocalPos;
        }
        public virtual void CloseDoor(bool isHandHard = false)
        {
            doorL.DOLocalMove(doorLOriPos, animationDuration);
            doorR.DOLocalMove(doorROriPos, animationDuration).OnComplete(() => { isOpen = false; if (EventCloseEd != null) { EventCloseEd(); } });
        }

        public virtual void CloseDoor_瞬间(bool isHandHard = false)
        {
            doorL.localPosition = doorLOriPos;
            doorR.localPosition = doorROriPos;
        }

    }
}