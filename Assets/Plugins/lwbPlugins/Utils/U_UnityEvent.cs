/*Name:		 				U_UnityEvent	
 *Description: 				
 *Author:       			lwb
 *Date:         			2019-06-
 *Copyright(C) 2019 by 		company@zhiwyl.com*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace W
{
    public class U_UnityEvent
    {


    }

    [Serializable]
    public class W__UnityEvent : UnityEvent { }

    [Serializable]
    public class W_Int_UnityEvent : UnityEvent<int> { }

    [Serializable]
    public class W_Transform_UnityEvent : UnityEvent<Transform> { }
}