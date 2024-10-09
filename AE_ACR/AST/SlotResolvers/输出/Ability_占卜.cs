#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_占卜 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (占卜.OriginalHookActionReady())
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(占卜.OriginalHook());
    }
}