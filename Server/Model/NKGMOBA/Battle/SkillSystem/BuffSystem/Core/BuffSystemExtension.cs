//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2020年12月14日 20:29:08
//------------------------------------------------------------

namespace ETModel
{
    /// <summary>
    /// Buff系统实用函数
    /// </summary>
    public static class BuffSystemUtility
    {
        /// <summary>
        /// 获取Buff 目标，之所以需要这个拓展方法，因为ABuffSystemBase的TheUnitFrom和TheUnitBelongto被赋值于Buff链起源的地方，具体值取决于那个起源Buff
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Unit GetBuffTarget(this ABuffSystemBase self)
        {
            return self.BuffData.BuffTargetTypes == BuffTargetTypes.Self? self.TheUnitFrom : self.TheUnitBelongto;
        }

        /// <summary>
        /// 获取BuffSystem对应的BuffData，封装一下转型操作
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetSelfBuffData<T>(this ABuffSystemBase self) where T : BuffDataBase
        {
            return self.BuffData as T;
        }
    }
}