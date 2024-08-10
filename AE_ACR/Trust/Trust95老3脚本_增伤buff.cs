using System.Numerics;
using AEAssist;
using AEAssist.Avoid;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;
using Trust.DungeonController.Dungeon;
using Trust.DungeonController.SpellReaction;

namespace ScriptTest;

public class Trust95老3脚本_增伤buff : IResolverScript
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

        IBattleChara? boss = null;

        foreach (var gameObject in ECHelper.Objects)
        {
            if (gameObject.DataId == 16735)
            {
                if (gameObject is IBattleChara battleChara)
                {
                    boss = battleChara;
                }
            }
        }

        float speed = 0.1f;   
        
        foreach (var gameObject in ECHelper.Objects)
        {
            if (gameObject.DataId == 16738)
            {

                if (boss != null)
                {
                    
                    float distance = Vector3.Distance(boss.Position, gameObject.Position);
                    if (distance <= 10)
                    {
                        // 计算从小怪A到圆心B的方向向量
                        Vector3 direction = (boss.Position - gameObject.Position);
                        // 根据方向和速度计算小怪A的下一秒位置
                        Vector3 newPosition = gameObject.Position + direction * speed;
                        
                        Core.Me.SetPos(newPosition);
                    }
                }

            }
        }
    }
}