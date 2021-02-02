/*Name:		 				W_Enum	
 *Description: 				所有大类枚举，都在这里。
 *Author:       			李文博 
 *Date:         			2018-09-
 *Copyright(C) 2018 by 		智网易联*/

namespace W_Enum
    {
    public enum Corner { 左上,左中,左下,上,中,下,右上,右中,右下 }
    public enum CZMode { 任意拆, 顺序拆 }

    public enum LookAtMode { 看向,Y轴跟着旋转,无}
    public enum WE_TransformRange { Self, SelfSon, SelfChildren, SelfBrother, Son, Children, Brother }

    /// <summary>
    /// 包含或者排除
    /// </summary>
    public enum IncludeOrExcept { include, except }
    public enum W_Axis { none, localX, localY, localZ, worldX, worldY, worldZ }
    public enum W_UpdateMode { FixedUpdate, Update, LateUpdate }
    public enum MainButton { 左键, 右键, 中键,无}

    public enum MoveMode { None,步行,飞行,路径}

    public enum TransformRange { Self, SelfSon, SelfChildren, SelfBrother, Son, Children, Brother }
    public enum LeftRight { None, Left, Right, Both }
    public enum WColor { 绿, 蓝, 黄, 灰, 黑, 紫, 白, 红 };

}


