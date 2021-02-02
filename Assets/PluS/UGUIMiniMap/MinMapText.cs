/*Name:		 				MinMapText	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMapText : MonoBehaviour
{
    [Header("Target")]
    public GameObject GraphicPrefab = null;

    public Vector3 OffSet = Vector3.zero;
    [Space(5)]
    [Header("Icon")]
    public Sprite Icon = null;
    public Sprite DeathIcon = null;
    public Color IconColor = new Color(1, 1, 1, 0.9f);
    public float Size = 40;
    [Header("宽高比")]
    public float widthHeighRate = 1.618f;
    [Header("若为空，默认自己")]
    public Transform Target = null;
    [Header("若为空，默认自己的名字")]
    public string InfoItem = "";
    [Space(5)]
    [Header("Settings")]

    public ItemEffect m_Effect = ItemEffect.None;
    //Privates
    private Image Graphic = null;
    private RectTransform RectRoot;
    private RectTransform RectMask;
    private GameObject cacheItem = null;
    [Header("所属楼层")]
    public int store = 1;



    //public bool 是否一直显示 = false;



    /// 怎么才能保证地图不激活，他照样使用呢
    void Start()
    {
        if (string.IsNullOrEmpty(InfoItem))
        {
            try
            {
                //原项目给设备名称
                //InfoItem = transform.GetComponent<DC_EquipObj>().m_设备实体类.设备名称;
                InfoItem = "对应的名称";
            }
            catch
            {
                InfoItem = transform.name;
            }
        }
        if (Target == null)
        {
            Target = transform;
        }


        //if (bl_MiniMap.MapUIRoot != null)
        if (bl_MiniMap.MapUIRoot != null)
        {
            CreateIcon();
        }
        else { Debug.Log("You need a MiniMap in scene for use MiniMap Items."); }
    }
    void CreateIcon()
    {
        cacheItem = Instantiate(GraphicPrefab) as GameObject;
        RectRoot = bl_MiniMap.MapUIRoot;
        RectMask = bl_MiniMap.MaskUI;
        Graphic = cacheItem.GetComponent<Image>();
        if (Icon != null) { Graphic.sprite = Icon; Graphic.color = IconColor; }
        cacheItem.transform.SetParent(RectRoot, false);
        Graphic.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        StartEffect();
        bl_IconItem ii = cacheItem.GetComponent<bl_IconItem>();
        ii.GetInfoItem(InfoItem);
        cacheItem.transform.SetParent(RectMask, false);
    }
    bool showFlag = false;
    bool hideFlag = false;

    public void ShowIcon()
    {
        ShowItem();
        showFlag = true;
        hideFlag = false;
    }

    public void HideIcon()
    {
        HideItem();
        hideFlag = true;
        showFlag = false;
    }
    void FixedUpdate()
    {
        if (!hideFlag
            //原项目判断楼层
            //&&store != DCS.Ins.store
            )
        {
            HideIcon();
            return;
        }
        else if (!showFlag
            //原项目判断楼层
            //&&store == DCS.Ins.store
            )
        {
            ShowIcon();
            return;
        }
        UpdatePostion();
    }
    [InspectorButton("SetStore")]
    public string 设置楼层 = "";
    public void SetStore()
    {
        //原项目判断楼层
        //store = DCS.JudeStore(transform);
    }

    private void UpdatePostion()
    {
        if (Target == null || Graphic == null)
            return;
        RectTransform rt = Graphic.GetComponent<RectTransform>();
        Vector3 CorrectPosition = TargetPosition + OffSet;
        Vector2 vp2 = bl_MiniMap.MiniMapCamera.WorldToViewportPoint(CorrectPosition);
        Vector2 position = new Vector2((vp2.x * RectRoot.sizeDelta.x) - (RectRoot.sizeDelta.x * 0.5f),
            (vp2.y * RectRoot.sizeDelta.y) - (RectRoot.sizeDelta.y * 0.5f));

        float size = Size;
        rt.anchoredPosition = position;
        rt.sizeDelta = Vector2.Lerp(rt.sizeDelta, new Vector2(size * widthHeighRate, size), Time.deltaTime * 8);
        Quaternion r = Quaternion.identity;
        //这个为啥要跟？
        //r.x = Target.rotation.x;
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

        if (DeathIcon == null || inmediate)
        {
            Graphic.GetComponent<bl_IconItem>().DestroyIcon(inmediate);
        }
        else
        {
            Graphic.GetComponent<bl_IconItem>().DestroyIcon(inmediate, DeathIcon);
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










    [Header("小地图控制器")]
    public bl_MiniMap _minimap = null;
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


















// //If a component missing, return for avoid bugs.
//        if (Target == null)
//            return;
//        if (Graphic == null)
//            return;
//        //Get the Rect of Target UI
//        RectTransform rt = Graphic.GetComponent<RectTransform>();
////Setting the modify position
//Vector3 CorrectPosition = TargetPosition + OffSet;
////Convert the position of target in ViewPortPoint
//Vector2 vp2 = bl_MiniMap.MiniMapCamera.WorldToViewportPoint(CorrectPosition);
////Calculate the position of target and convert into position of screen
//Vector2 position = new Vector2((vp2.x * RectRoot.sizeDelta.x) - (RectRoot.sizeDelta.x * 0.5f),
//    (vp2.y * RectRoot.sizeDelta.y) - (RectRoot.sizeDelta.y * 0.5f));
//        //if show off screen
//        if (OffScreen)
//        {
//            //Calculate the max and min distance to move the UI
//            //this clamp in the RectRoot sizeDela for border
//            position.x = Mathf.Clamp(position.x, -((RectRoot.sizeDelta.x* 0.5f) - BorderOffScreen), ((RectRoot.sizeDelta.x* 0.5f) - BorderOffScreen));
//            position.y = Mathf.Clamp(position.y, -((RectRoot.sizeDelta.y* 0.5f) - BorderOffScreen), ((RectRoot.sizeDelta.y* 0.5f) - BorderOffScreen));
//        }

//        //calculate the position of UI again, determine if offscreen
//        //if offscreen reduce the size
//        float size = Size;
//        //Use this (useCompassRotation when have a circle miniMap)
//        if (m_miniMap.useCompassRotation)
//        {
//            //Compass Rotation
//            Vector3 screenPos = Vector3.zero;
////Calculate diference
//Vector3 forward = Target.position - m_miniMap.TargetPosition;
////Position of target from camera
//Vector3 cameraRelativeDir = bl_MiniMap.MiniMapCamera.transform.InverseTransformDirection(forward);
////normalize values for screen fix
//cameraRelativeDir.z = 0;
//            cameraRelativeDir = cameraRelativeDir.normalized / 2;
//            //Convert values to positive for calculate area OnScreen and OffScreen.
//            float posPositiveX = Mathf.Abs(position.x);
//float relativePositiveX = Mathf.Abs((0.5f + (cameraRelativeDir.x * m_miniMap.CompassSize)));
//            if (posPositiveX >= relativePositiveX)
//            {
//                screenPos.x = 0.5f + (cameraRelativeDir.x* m_miniMap.CompassSize)/*/ Camera.main.aspect*/;
//                screenPos.y = 0.5f + (cameraRelativeDir.y* m_miniMap.CompassSize);
//                position = screenPos;
//                size = OffScreenSize;
//            }
//            else
//            {
//                size = Size;
//            }
//        }
//        else
//        {
//            if (position.x == (RectRoot.sizeDelta.x* 0.5f) - BorderOffScreen || position.y == (RectRoot.sizeDelta.y* 0.5f) - BorderOffScreen ||
//                position.x == -(RectRoot.sizeDelta.x* 0.5f) - BorderOffScreen || -position.y == (RectRoot.sizeDelta.y* 0.5f) - BorderOffScreen)
//            {
//                size = OffScreenSize;
//            }
//            else
//            {
//                size = Size;
//            }
//        }
//        rt.anchoredPosition = position;
//        rt.sizeDelta = Vector2.Lerp(rt.sizeDelta, new Vector2(size* widthHeighRate, size), Time.deltaTime* 8);
//        Quaternion r = Quaternion.identity;
//r.x = Target.rotation.x;


//        rt.localRotation = r;