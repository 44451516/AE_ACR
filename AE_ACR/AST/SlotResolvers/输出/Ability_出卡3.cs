#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_出卡3 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        
        
        if (Play3.OriginalHook().Id == Play3)
        {
            return -1;
        }

        if (CanWeave())
        {
            if (Play3.OriginalHookActionReady())
            {
                IBattleChara? tankHpOrderByPercent = getTankHpOrderByPercent();
                if (tankHpOrderByPercent != null)
                {
                    return 0;
                }
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        IBattleChara? tankHpOrderByPercent = getTankHpOrderByPercent();
        if (tankHpOrderByPercent != null)
        {
            slot.Add(new Spell(Play3.OriginalHook().Id, tankHpOrderByPercent));
        }
        
    }
}