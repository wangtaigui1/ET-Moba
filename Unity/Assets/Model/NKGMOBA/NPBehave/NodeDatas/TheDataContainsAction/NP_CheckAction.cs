//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年9月7日 10:29:38
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using ETModel.TheDataContainsAction;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;

namespace ETModel
{
    /// <summary>
    /// 检查技能是否能释放
    /// </summary>
    public class NP_CheckAction: NP_ClassForStoreAction
    {
        [LabelText("要引用的的数据结点ID")]
        public long dataId;

        [HideInEditorMode]
        public NodeDataForStartSkill m_NodeDataForStartSkill;

        [LabelText("将要检查的技能ID（QWER：0123）")]
        public int theSkillIDBelongTo;

        public override Func<bool> GetFunc1ToBeDone()
        {
            this.m_NodeDataForStartSkill = (NodeDataForStartSkill) Game.Scene.GetComponent<NP_TreeDataRepository>().GetNP_TreeData(Game.Scene
                    .GetComponent<UnitComponent>()
                    .Get(this.Unitid).GetComponent<NP_RuntimeTreeManager>()
                    .GetTree(this.RuntimeTreeID).theNP_DataSupportIdBelongTo).mSkillDataDic[dataId];
            this.m_Func1 = this.CheckCostToSpanSkill;
            return this.m_Func1;
        }

        private bool CheckCostToSpanSkill()
        {
            //TODO 相关状态检测，例如沉默，眩晕等,下面是示例代码
            /*
            if (Game.Scene.GetComponent<UnitComponent>().Get(this.Unitid).GetComponent<BuffManagerComponent>()
                    .FindBuffByWorkType(BuffWorkTypes.Silence))
            {
                return false;
            }
            */
            HeroDataComponent heroDataComponent = Game.Scene.GetComponent<UnitComponent>().Get(this.Unitid).GetComponent<HeroDataComponent>();
            switch (m_NodeDataForStartSkill.SkillCostTypes)
            {
                case SkillCostTypes.MagicValue:
                    //依据技能具体消耗来进行属性改变操作
                    if (heroDataComponent.CurrentMagicValue > m_NodeDataForStartSkill.SkillCost[heroDataComponent.GetSkillLevel(theSkillIDBelongTo)])
                        return true;
                    else
                    {
                        return false;
                    }
                case SkillCostTypes.Other:
                    return true;
                case SkillCostTypes.HPValue:
                    if (heroDataComponent.CurrentLifeValue > m_NodeDataForStartSkill.SkillCost[heroDataComponent.GetSkillLevel(theSkillIDBelongTo)])
                        return true;
                    else
                    {
                        return false;
                    }
                default:
                    return true;
            }
        }
    }
}