//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2020年10月2日 17:39:06
//------------------------------------------------------------

namespace ETModel
{
    public class SendBuffInfoToClientBuffSystem: ABuffSystemBase
    {
        public override void OnExecute()
        {
            SendBuffInfoToClientBuffData sendBuffInfoToClientBuffData = this.GetSelfBuffData<SendBuffInfoToClientBuffData>();

            ABuffSystemBase targetBuffSystem = this.TheUnitBelongto.GetComponent<BuffManagerComponent>()
                    .GetBuffById(
                        (this.BelongtoRuntimeTree.BelongNP_DataSupportor.BuffNodeDataDic[sendBuffInfoToClientBuffData.TargetBuffNodeId.Value] as
                                NormalBuffNodeData).BuffData.BuffId);

            Game.EventSystem.Run(EventIdType.SendBuffInfoToClient,
                new M2C_BuffInfo()
                {
                    UnitId = this.BelongtoRuntimeTree.BelongToUnitId,
                    SkillId = sendBuffInfoToClientBuffData.BelongToSkillId.Value,
                    BBKey = sendBuffInfoToClientBuffData.BBKey.BBKey,
                    TheUnitFromId = this.TheUnitFrom.Id,
                    TheUnitBelongToId = this.TheUnitBelongto.Id,
                    BuffLayers = targetBuffSystem.CurrentOverlay,
                    BuffMaxLimitTime = targetBuffSystem.MaxLimitTime,
                });
            this.BuffState = BuffState.Finished;
        }
    }
}