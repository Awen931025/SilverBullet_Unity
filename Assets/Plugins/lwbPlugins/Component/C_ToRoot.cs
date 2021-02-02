/*Name:		 				C_放到根节点	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using UnityEngine;

namespace W
{
    public class C_ToRoot : MonoBehaviour
    {
        [InspectorButton("ToNullParetnt")]
        public string 放到根节点 = "";

        public void ToNullParetnt()
        {
            transform.SetParent(null);
        }


    }
}