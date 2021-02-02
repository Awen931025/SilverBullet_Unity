using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
namespace W
{
    public class U_UIBehavior : MonoBehaviour
    {
    }

    public static class U_UIBehavior_Extren
    {
        public static RectTransform GetRect(this UIBehaviour go)
        {
            return go.GetComponent<RectTransform>();
        }
    }
}

