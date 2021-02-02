using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

/// <summary>
/// This attribute can only be applied to fields because its
/// associated PropertyDrawer only operates on fields (either
/// public or tagged with the [SerializeField] attribute) in
/// the target MonoBehaviour.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorFoldAttribute : PropertyAttribute
{
    public static float kDefaultButtonWidth = 150;

    public readonly string MethodName;
    public readonly Color btnColor;

    private float _buttonWidth = kDefaultButtonWidth;
    public float ButtonWidth
    {
        get { return _buttonWidth; }
        set { _buttonWidth = value; }
    }
    public InspectorFoldAttribute(string MethodName)
    {
        this.MethodName = MethodName;
        this.btnColor = Color.yellow;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MethodName"></param>
    /// <param name="colorIndex">0黄,1白，2灰，3紫，4绿，5蓝，6青，7黑，8红</param>
    public InspectorFoldAttribute(string MethodName, int colorIndex)
    {
        this.MethodName = MethodName;
        switch (colorIndex)
        {
            case 0: this.btnColor = Color.yellow; break;
            case 1: this.btnColor = Color.white; break;
            case 2: this.btnColor = Color.grey; break;
            case 3: this.btnColor = Color.magenta; break;
            case 4: this.btnColor = Color.green; break;
            case 5: this.btnColor = Color.blue; break;
            case 6: this.btnColor = Color.cyan; break;
            case 7: this.btnColor = Color.black; break;
            case 8: this.btnColor = Color.red; break;
        }
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorFoldDrawer : PropertyDrawer
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {

        InspectorButtonAttribute inspectorButtonAttribute = (InspectorButtonAttribute)attribute;
        GUI.color = inspectorButtonAttribute.btnColor;

        Rect buttonRect = new Rect(position.x + (position.width - inspectorButtonAttribute.ButtonWidth) * 0.5f, position.y, inspectorButtonAttribute.ButtonWidth, position.height + 5);
        if (GUI.Button(buttonRect, label.text))
        {
            System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
            string eventName = inspectorButtonAttribute.MethodName;

            if (_eventMethodInfo == null)
                _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (_eventMethodInfo != null)
                _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
            else
                Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
        }
        GUI.color = Color.white;
    }
}
#endif