using AEAssist;
using AEAssist.Avoid;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

//小怪钢铁
public class Trust91老2脚本_36482 : IResolverScript
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
            if (gameObject.DataId != 16796)
                continue;

            if (gameObject is IBattleChara battleChara)
            {
                if (battleChara.CurrentCastTime >= 0.1 && battleChara.CurrentCastTime <= 0.5f)
                {
                    var bossPos = gameObject.Position;
                    var dir = gameObject.Rotation.GetDirV3(0);
                    bossPos = bossPos - dir * 2; // 防止脚底
                    var 矩形长度 = 60;
                    var 矩形宽度 = 16.1f;

                    map.AddDangerShape
                    (
                        gameObject.EntityId, new DangerShape
                        (
                            RectShape.Create(bossPos, bossPos + dir * 矩形长度, 矩形宽度), TimeHelper.Now(), 7000
                        ), true
                    );
                }
            }


        }

        // 让角色移动到安全区
        SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
    }
}