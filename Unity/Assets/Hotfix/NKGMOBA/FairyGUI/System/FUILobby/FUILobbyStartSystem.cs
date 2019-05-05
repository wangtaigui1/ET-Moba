//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年5月2日 17:09:27
//------------------------------------------------------------

using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class FUILobbyStartSystem: StartSystem<FUILobby.FUILobby>
    {
        public override void Start(FUILobby.FUILobby self)
        {
            self.shop.self.onClick.Add(() =>
            {
                Log.Info("发送了测试消息");
                Game.Scene.GetComponent<SessionComponent>().Session.Send(new PlayerInfo());
            });
        }
    }
}