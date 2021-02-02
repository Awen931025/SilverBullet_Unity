using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SeparatorAttribute:PropertyAttribute
{
    public readonly string title;
    public  Color color=Color.yellow;

    public SeparatorAttribute(string _title)
    {
        this.title = _title;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_title"></param>
    /// <param name="colorIndex">0黄,1白，2灰，3紫，4绿，5蓝，6青，7黑，8红</param>
    public SeparatorAttribute(string _title,int colorIndex)
    {
        this.title = _title;
        switch (colorIndex)
        {
            case 0: this.color = Color.yellow; break;
            case 1: this.color = Color.white; break;
            case 2: this.color = Color.grey; break;
            case 3: this.color = Color.magenta; break;
            case 4: this.color = Color.green; break;
            case 5: this.color = Color.blue; break;
            case 6: this.color = Color.cyan; break;
            case 7: this.color = Color.black; break;
            case 8: this.color = Color.red; break;
        }
    }
}




#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SeparatorAttribute))]
public class SeparatorDrawer : DecoratorDrawer
{
    SeparatorAttribute separatorAttribute { get { return ((SeparatorAttribute)attribute); } }
    public override void OnGUI(Rect _position)
    {
        if (separatorAttribute.title == "")
        {
            _position.height = 1;
            _position.y += 13;
            GUI.Box(_position, "");
        }
        else
        {
            Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(separatorAttribute.title));
            //textSize = textSize * 2;
            textSize = textSize * 1.2f;
            float separatorWidth = (_position.width - textSize.x) / 2.0f - 5.0f;
            _position.y += 13;

            GUI.Box(new Rect(_position.xMin, _position.yMin, separatorWidth, 3), "");
            GUIStyle titleStyle2 = new GUIStyle();
            titleStyle2.fontStyle = FontStyle.Bold;
            titleStyle2.normal.textColor = separatorAttribute.color;
            titleStyle2.alignment = TextAnchor.MiddleCenter;
            titleStyle2.fontSize = 12;
            GUI.Box(new Rect(_position.xMin + separatorWidth + 5.0f, _position.yMin - 8.0f, textSize.x, 20), separatorAttribute.title, titleStyle2);
            GUI.Box(new Rect(_position.xMin + separatorWidth + 10.0f + textSize.x, _position.yMin, separatorWidth, 3), "");
        }
    }

    public override float GetHeight()
    {
        return 30f;
    }
}
#endif