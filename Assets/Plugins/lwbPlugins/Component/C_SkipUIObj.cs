/*Name:		 				C_SkipUI	
 *Description: 				
 *Author:       			李文博 
 *Date:         			2019-08-
 *Copyright(C) 2019 by 		智联友道*/
using UnityEngine;
namespace W
{
    public class C_SkipUIObj : MonoBehaviour
    {
        public string showConent;
        protected virtual void OnEnable()
        {
            //if(string.IsNullOrEmpty(showConent) )
            //{
            showConent = gameObject.name;
            //}
        }


    }
}