using AEAssist;
using AEAssist.Avoid;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

public class Trust100老1脚本_36382 : IResolverScript
{
    public void OnActive(DungeonController dungeonController, ResolverCondParams spellCondParams)
    {

    }

    public void OnUpdate(DungeonController dungeonController)
    {
        // 先拿到当前场景的地图管理
        var mapBaseName = Core.Resolve<MemApiZoneInfo>().GetCurrZoneInfo().MapBaseName;
        if (!AvoidManager.Instance.GetMap(mapBaseName, out var map))
        {
            return;
        }

        foreach (var gameObject in ECHelper.Objects)
        {
            if (gameObject.DataId != 16756)
                continue;

            if (gameObject is IBattleChara battleChara)
            {
                if (battleChara.CastActionId == 36382)
                {
                    Core.Me.SetPos(battleChara.Position);
                }
            }
        }
    }
}