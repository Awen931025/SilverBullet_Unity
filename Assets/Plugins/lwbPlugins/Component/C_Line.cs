using UnityEngine;

namespace W
{
    [RequireComponent(typeof(LineRenderer))]
    public class C_Line : MonoBehaviour
    {
        public bool drawLine = true;
        LineRenderer line;
        public Transform startTran;
        public Transform endTran;
        public float width = 0.001f;
        [Header("是否带颜色控制")]
        public bool changeColor = false;
        public Color color = Color.blue;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            DrawLine();
        }




        void Init()
        {
            line = GetComponent<LineRenderer>();
            if (startTran == null)
            {
                startTran = transform;
            }
            line.positionCount = 2;
            line.startWidth = width;
            line.endWidth = width;

        }

        private void DrawLine()
        {
            if (drawLine)
            {
                Vector3[] poisS = new Vector3[] { startTran.position, endTran.position };
                line.SetPositions(poisS);
                if (changeColor)
                {
                    line.material.color = color;
                }
            }
        }

        public void SetEnable(bool draw)
        {
            if (draw)
            {
                drawLine = true;
                enabled = true;
            }
            else
            {
                drawLine = false;
                enabled = false;
            }
        }
    }
}