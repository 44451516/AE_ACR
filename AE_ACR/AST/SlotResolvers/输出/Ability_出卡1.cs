#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_出卡1 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (Play1.OriginalHook().Id == Play1)
        {
            return -1;
        }

        if (CanWeave())
        {
            if (Play1.OriginalHookActionReady())
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(Play1.OriginalHook());
    }
}