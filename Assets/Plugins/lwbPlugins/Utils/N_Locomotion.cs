/*Name:		 				W_Locomotion	
 *Description: 				运动类
 *Author:       			李文博 
 *Date:         			2018-07-31
 *Copyright(C) 2018 by 		北京兆泰源信息技术有限公司*/

namespace U_Locomotion
{
    public delegate void W_MoveOnStart(ref int whichOne);
    public delegate void W_MoveOnComplete(ref int whichOne);

    public delegate void W_RotateOnStart(ref int whichOne);
    public delegate void W_RotateOnComplete(ref int whichOne);
    public enum W_Axis { none, localX, localY, localZ, worldX, worldY, worldZ }
    public enum W_UpdateMode { FixedUpdate, Update, LateUpdate }
}



