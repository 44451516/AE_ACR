using AEAssist;
using AEAssist.Avoid;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
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

            foreach (var C in mapBaseName)
            {
                
            }

            foreach (var keyValuePair in TargetMgr.Instance.EnemysIn20)
            {
                IBattleChara battleChara = keyValuePair.Value;
                Core.Me.SetPos(battleChara.Position); 
            }


        }
        
       

  
        // 让角色移动到安全区
        // SpellResolverMgr.Instance.AllResolvers[SpellResolverType.Move2SafeArea].OnUpdate(dungeonController, null);
    }
}