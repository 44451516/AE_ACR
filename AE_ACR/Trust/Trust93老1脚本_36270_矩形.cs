using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

//小怪直线
public class Trust93老1脚本_36270_矩形 : IResolverScript
{
    public void OnActive(DungeonController dungeonController, ResolverCondParams spellCondParams)
    {

    }
    
    private long LastCheckTick = 0;
    private const long CheckIntervals = 3 * 1000;
    
    public void OnUpdate(DungeonController dungeonController)
    {
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
                if (gameObject.DataId != 16731)
                    continue;

                if (gameObject is IBattleChara battleChara)
                {
                    if (battleChara.CurrentCastTime >= 10.5 && battleChara.CurrentCastTime <= 11f)
                    {

                        var bossPos = gameObject.Position;
                        var dir = gameObject.Rotation.GetDirV3(0);
                        bossPos = bossPos - dir * 2; // 防止脚底
                        var 矩形长度 = 20;
                        var 矩形宽度 = 20;

                        map.AddDangerShape
                        (
                            gameObject.EntityId, new DangerShape
                            (
                                RectShape.Create(bossPos, bossPos + dir * 矩形长度, 矩形宽度), TimeHelper.Now(), 2000
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