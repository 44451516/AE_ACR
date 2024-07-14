#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_沥血剑 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手()) return -1;

        if (HasEffect(Buffs.沥血剑BUFFGoringBladeReady)) return 0;

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(沥血剑GoringBlade.OriginalHook());
    }
}