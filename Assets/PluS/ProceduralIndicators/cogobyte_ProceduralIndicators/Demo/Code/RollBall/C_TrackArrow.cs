using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cogobyte.ProceduralIndicators;

public class C_TrackArrow : MonoBehaviour
{

    #region Ins
    private C_TrackArrow() { }

    private static C_TrackArrow _instance;
    public static C_TrackArrow Ins
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    public bool active;
    ArrowObject arrowObject;
    public Transform startTarget;
    public Transform endTarget;
    private MeshRenderer meshrenderer;
    public bool isToTargetMat = false;
    public Vector3 targetOffset = new Vector3(0, 0.3f, 0);
    bool arrowWidthSettingFlag = false;
    public float defaultScale = 0.03f;
    public float defaultBrokenLineLenght = 0.2f;
    public float defaultBrokenLenght = 0.3f;
    void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
        arrowObject = GetComponent<ArrowObject>();
        SetUnitLength(defaultBrokenLenght, defaultBrokenLineLenght);
        SetScale(defaultScale);
    }
    private void LateUpdate()
    {
        if (startTarget != null)
            transform.position = startTarget.position;
    }
    void Update()
    {
        if (!active)
            return;
        if (endTarget == null)
            return;

        arrowObject.arrowPath.endPoint = -transform.position + endTarget.position + targetOffset;
        arrowObject.updateArrowMesh();

        if (isToTargetMat && endTarget.GetComponentInChildren<MeshRenderer>().material != meshrenderer.material)
        {
            meshrenderer.material = endTarget.GetComponentInChildren<MeshRenderer>().material;
        }
    }
    public void SetClose()
    {
        active = false;
        gameObject.SetActive(false);
    }

    public void SetOpen()
    {
        gameObject.SetActive(true);
        active = true;
    }
    public void SetOpen(Transform target)
    {
        SetOpen();
        this.endTarget = target;
    }

    public void SetSwitch()
    {
        if (active)
        {
            SetClose();
        }
        else
        {
            SetOpen();
        }
    }
    public void SetTarget(Transform target)
    {
        this.endTarget = target;
    }
    public void SetScale(float width)
    {
        width = Mathf.Clamp(width, 0.03f, 1f);
        float oriWidth = arrowObject.arrowPath.widthFunction.keys[1].value;
        float oriHeight = arrowObject.arrowPath.heightFunction.keys[1].value;
        float kuoda = (width / oriWidth);
        arrowObject.arrowPath.widthFunction = AnimationCurve.Linear(0, width, 1, width);
        arrowObject.arrowPath.heightFunction = AnimationCurve.Linear(0, oriHeight * kuoda, 1, oriHeight * kuoda);
        if (!arrowWidthSettingFlag)
        {
            arrowWidthSettingFlag = true;
            kuoda *= 1.5f;
        }

        arrowObject.arrowHead.size = kuoda * arrowObject.arrowHead.size;
        arrowObject.arrowTail.size = kuoda * arrowObject.arrowTail.size;
    }
    public void SetUnitLength(float brokenLineLength, float brakeLength)
    {
        arrowObject.arrowPath.brokenLineLength = brokenLineLength;
        arrowObject.arrowPath.brakeLength = brakeLength;
    }
}
