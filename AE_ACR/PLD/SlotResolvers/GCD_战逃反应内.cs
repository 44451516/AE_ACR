using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_战逃反应内 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手()) return -1;

        if (HasEffect(Buffs.FightOrFlight)) return 0;

        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        return 先锋剑FastBlade.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}