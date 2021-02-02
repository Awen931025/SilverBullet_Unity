/*Name:		 				DC_MinmapCursor	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_MinmapCursor : MonoBehaviour
{
    public bl_MiniMap _minimap = null;
    [Header("生成图标的预制体")]
    public GameObject GraphicPrefab = null;
    [Header("Icon")]
    public Sprite Icon = null;

    Transform Target;
    public Vector3 OffSet = Vector3.zero;
    public Color IconColor = new Color32(156, 245, 126, 230);
    public float Size = 70;
    public string InfoItem = "Icon";
    [Space(5)]
    [Header("Settings")]
    public bool OffScreen = true;
    public float BorderOffScreen = 0.01f;
    public float outSizeRate = 0.5f;
    public ItemEffect m_Effect = ItemEffect.None;
    //Privates
    private Image Graphic = null;
    private RectTransform RectRoot;
    private GameObject cacheItem = null;

    public bool m_是否一直显示 = false;

    /// <summary>
    /// Get all requiered component in start
    /// </summary>
    void Start()
    {
        if (bl_MiniMap.MapUIRoot != null)
        {
            CreateIcon();
        }
        else { Debug.Log("You need a MiniMap in scene for use MiniMap Items."); }
    }

    /// <summary>
    /// 
    /// </summary>
    void CreateIcon()
    {
        cacheItem = Instantiate(GraphicPrefab) as GameObject;
        RectRoot = bl_MiniMap.MapUIRoot;
        Graphic = cacheItem.GetComponent<Image>();
        if (Icon != null) { Graphic.sprite = Icon; Graphic.color = IconColor; }
        cacheItem.transform.SetParent(RectRoot.transform, false);
        Graphic.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        if (Target == null) { Target = this.GetComponent<Transform>(); }
        StartEffect();
        bl_IconItem ii = cacheItem.GetComponent<bl_IconItem>();
        if(string.IsNullOrEmpty(InfoItem))
        {
            InfoItem = transform.name;
        }
        ii.GetInfoItem(InfoItem);
    }
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        //If a component missing, return for avoid bugs.
        if (Target == null)
            return;
        if (Graphic == null)
            return;
        //Get the Rect of Target UI
        RectTransform rt = Graphic.GetComponent<RectTransform>();
        Vector3 CorrectPosition = TargetPosition + OffSet;
        Vector2 vp2 = bl_MiniMap.MiniMapCamera.WorldToViewportPoint(CorrectPosition);
        Vector2 position = new Vector2((vp2.x * RectRoot.sizeDelta.x) - (RectRoot.sizeDelta.x * 0.5f),
            (vp2.y * RectRoot.sizeDelta.y) - (RectRoot.sizeDelta.y * 0.5f));
        if (OffScreen)
        {
            position.x = Mathf.Clamp(position.x, -((RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen), ((RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen));
            position.y = Mathf.Clamp(position.y, -((RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen), ((RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen));
        }
        float size = Size;
        if (m_miniMap.useCompassRotation)
        {
            Vector3 screenPos = Vector3.zero;
            Vector3 forward = Target.position - m_miniMap.TargetPosition;
            Vector3 cameraRelativeDir = bl_MiniMap.MiniMapCamera.transform.InverseTransformDirection(forward);
            cameraRelativeDir.z = 0;
            cameraRelativeDir = cameraRelativeDir.normalized / 2;
            float posPositiveX = Mathf.Abs(position.x);
            float posPositiveY = Mathf.Abs(position.y);
            float relativePositiveX = Mathf.Abs((0.5f + (cameraRelativeDir.x * 1454.545f)));
            float retaivePositiveY = Mathf.Abs(0.5f + cameraRelativeDir.y * 818f);

            if (posPositiveX >= relativePositiveX || posPositiveY >= retaivePositiveY)
            {
                //screenPos.x = 0.5f + (cameraRelativeDir.x * 1454.545f);
                //screenPos.y = 0.5f + (cameraRelativeDir.y * 818.1818f);

                screenPos.x =cameraRelativeDir.x * 1454.545f;
                screenPos.y =cameraRelativeDir.y * 818.1818f;

                position = screenPos;

                size = Size * outSizeRate;

            }
            else
            {
                size = Size;
            }
        }
        else
        {
            if (position.x == (RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen || position.y == (RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen ||
                position.x == -(RectRoot.sizeDelta.x * 0.5f) - BorderOffScreen || -position.y == (RectRoot.sizeDelta.y * 0.5f) - BorderOffScreen)
            {
                size = Size * outSizeRate;
            }
            else
            {
                size = Size;
            }
        }
        rt.anchoredPosition = position;
        rt.sizeDelta = new Vector2(size, size);
        Quaternion r = Quaternion.identity;
        r.x = Target.rotation.x;
        rt.localRotation = r;
    }



    void StartEffect()
    {
        Animation a = Graphic.GetComponent<Animation>();
        if (m_Effect == ItemEffect.Pulsing)
        {
            a.Play("Pulsing");
        }
        else if (m_Effect == ItemEffect.Fade)
        {
            a.Play("Fade");
        }
    }


    public void DestroyItem(bool inmediate)
    {
        if (Graphic == null)
        {
            Debug.Log("Graphic Item of " + this.name + " not exist in scene");
            return;
        }

        if (inmediate)
        {
            Graphic.GetComponent<bl_IconItem>().DestroyIcon(inmediate);
        }
    }
    public void HideItem()
    {
        if (cacheItem != null)
        {
            cacheItem.SetActive(false);
        }
        else
        {
            Debug.Log("There is no item to disable.");
        }
    }
    public void ShowItem()
    {
        if (cacheItem != null)
        {
            cacheItem.SetActive(true);
        }
        else
        {
            Debug.Log("There is no item to active.");
        }
    }

    public Vector3 TargetPosition
    {
        get
        {
            if (Target == null)
            {
                return Vector3.zero;
            }

            return new Vector3(Target.position.x, 0, Target.position.z);
        }
    }


    private bl_MiniMap m_miniMap
    {
        get
        {
            if (_minimap == null)
            {
                _minimap = this.cacheItem.transform.root.GetComponentInChildren<bl_MiniMap>();
            }
            return _minimap;
        }
    }

}
