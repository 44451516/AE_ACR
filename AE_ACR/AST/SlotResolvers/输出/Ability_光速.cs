#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_光速 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (光速.MyIsUnlock() == false)
        {
            return Flag_没有解锁;
        }

        
        if (CanWeave())
        {
            if (光速.OriginalHookActionReady())
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(光速.OriginalHook());
    }
}