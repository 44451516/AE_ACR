#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_安魂祈祷 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (CanWeave())
        {
            if (安魂祈祷Requiescat.OriginalHookActionReady())
            {
                if (WasLastAction(战逃反应FightOrFlight))
                {
                    return 0;
                }

                if (HasEffect(Buffs.FightOrFlight))
                {
                    return 0;
                }
            }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(安魂祈祷Requiescat.OriginalHook());
    }
}