using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

//小怪钢铁
public class Trust93老3脚本_绿圈 : IResolverScript
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

        const  float distance = 6; 

        foreach (var agent in ECHelper.Objects)
        {
            if (agent.DataId != 16736)
                continue;
            
            map.AddDangerShape(agent.EntityId, new DangerShape(new CircleShape()
            {
                Name = $"DangerFromPM {agent.Name}",
                Origin = agent.Position,
                Radius = distance
            }, TimeHelper.Now(), 100), true);
        }

        // 让角色移动到安全区
        SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
    }
}