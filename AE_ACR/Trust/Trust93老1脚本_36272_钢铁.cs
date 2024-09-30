using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

//小怪钢铁
public class Trust93老1脚本_36272_钢铁 : IResolverScript
{
    public void OnActive(DungeonController dungeonController, ResolverCondParams spellCondParams)
    {

    }

    private long LastCheckTick = 0;
    private const long CheckIntervals = 3 * 1000;

    public void OnUpdate(DungeonController dungeonController)
    {
        // ECHelper.Gauges.Get<NINGauge>().Kazematoi
        
        // 先拿到当前场景的地图管理
        var mapBaseName = Core.Resolve<MemApiZoneInfo>().GetCurrZoneInfo().MapBaseName;
        if (!AvoidManager.Instance.GetMap(mapBaseName, out var map))
        {
            return;
        }

        if (Environment.TickCount64 - LastCheckTick >= CheckIntervals)
        {
            foreach (var gameObject in ECHelper.Objects)
            {
                if (gameObject.DataId != 17314)
                    continue;

                if (gameObject is IBattleChara battleChara)
                {
                    
                    if (battleChara.CurrentCastTime >= 10.5 && battleChara.CurrentCastTime <= 10.6f)
                    {
                        map.AddDangerShape
                        (
                            gameObject.EntityId, new DangerShape
                            (
                                new CircleShape()
                                {
                                    Name = $"DangerFromPM {gameObject.Name}",
                                    Origin = gameObject.Position,
                                    Radius = 15f
                                }, TimeHelper.Now(), 2000
                            ), true
                        );
                        
                        LastCheckTick = Environment.TickCount64;
                    }
                }


            }
        }

        // 让角色移动到安全区
        SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
    }
}