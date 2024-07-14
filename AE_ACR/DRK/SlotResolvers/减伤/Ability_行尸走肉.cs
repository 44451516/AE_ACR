#region

using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_行尸走肉 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {
            if (行尸走肉.ActionReady() && attackMeCount() >= 5 && Core.Me.CurrentHpPercent() < 0.20f)
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(行尸走肉.OriginalHook());
    }
}