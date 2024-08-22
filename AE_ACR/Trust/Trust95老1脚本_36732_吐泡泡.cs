using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

public class Trust95老1脚本_36732_吐泡泡 : IResolverScript
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

        // 将队友视为危险区 由于队友在不断的移动 所以我们的做法是每帧创建一个持续时间短的危险区
        float distance = 3; // 认为队友周围6米内都是危险的

        foreach (var agent in ECHelper.Objects)
        {
            if (agent.DataId != 16852)
                continue;

            // 这里因为队友的id本来就具有唯一性 所以直接用这个id 而且它的值一般比较大 除此之丸可以使用脚本最上面的GenId
            // 同一个id的危险区会互相顶掉 即同id的危险区再次加上时，之前这个id的危险区就自动注销了
            // 具体添加怎样类型的危险区 请查看Shape的子类
            // 目前需要圆形的 所以传递CircleShape
            map.AddDangerShape(agent.EntityId, new DangerShape(new CircleShape()
            {
                Name = $"DangerFromPM {agent.Name}",
                Origin = agent.Position,
                Radius = distance
            }, TimeHelper.Now(), 50), true);
        }

        // 让角色移动到安全区
        SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
    }
}