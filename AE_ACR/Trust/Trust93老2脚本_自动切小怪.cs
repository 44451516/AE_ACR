using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

//小怪钢铁
public class Trust93老2脚本_自动切小怪 : IResolverScript
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
            if (gameObject.DataId != 16734)
                continue;

            if (gameObject is IBattleChara battleChara)
            {
                if (battleChara.CurrentHp > 0)
                {
                    if (TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me) <= 5)
                    {
                        Core.Resolve<MemApiTarget>().SetTarget(battleChara);
                    }
                }
            }
        }
    }
}