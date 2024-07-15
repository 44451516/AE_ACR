#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_醒梦 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (醒梦.ActionReady())
            {
                if (Core.Me.CurrentMp <= 7000)
                {
                    return 0;
                }
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(醒梦.OriginalHook());
    }
}