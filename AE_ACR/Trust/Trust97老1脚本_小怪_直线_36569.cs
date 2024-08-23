using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;


public class Trust97老1脚本_小怪_直线_36569 : IResolverScript
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
            if (gameObject.DataId != 16828)
                continue;

            if (gameObject is IBattleChara battleChara)
            {
                if (battleChara.CastActionId == 36569)
                {
                    if (battleChara.CurrentCastTime >= 4 && battleChara.CurrentCastTime <= 4.5f)
                    {
                        var bossPos = gameObject.Position;
                        var dir = gameObject.Rotation.GetDirV3(0);
                        bossPos = bossPos - dir * 2; // 防止脚底
                        var 矩形长度 = 30f;
                        var 矩形宽度 = 5.1f;

                        map.AddDangerShape
                        (
                            gameObject.EntityId, new DangerShape
                            (
                                RectShape.Create(bossPos, bossPos + dir * 矩形长度, 矩形宽度), TimeHelper.Now(), 2000
                            ), true
                        );
                    }
                }
             
            }


        }

        // 让角色移动到安全区
        SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
    }
}