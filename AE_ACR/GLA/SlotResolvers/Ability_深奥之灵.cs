using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

namespace AE_ACR.GLA.SlotResolvers;

public class Ability_深奥之灵 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (深奥之灵SpiritsWithin.ActionReady())
            {
                return 0;
            }
        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(深奥之灵SpiritsWithin.OriginalHook());
    }
}