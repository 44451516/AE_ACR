#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_地星 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (地星.ActionReady())
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(地星.OriginalHook());
    }
}