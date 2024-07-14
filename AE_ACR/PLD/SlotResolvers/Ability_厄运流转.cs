#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_厄运流转 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (CanWeave())
            if (厄运流转CircleOfScorn.OriginalHookActionReady() && TargetHelper.GetNearbyEnemyCount(5) > 0)
            {
                if (HasEffect(Buffs.FightOrFlight))
                {
                    if (安魂祈祷Requiescat.IsUnlock())
                    {
                        if (GetCooldownRemainingTime(安魂祈祷Requiescat) > 40) return 0;
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (GetCooldownRemainingTime(战逃反应FightOrFlight) > 20 && GetCooldownRemainingTime(战逃反应FightOrFlight) < 40)
                    return 0;
            }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(厄运流转CircleOfScorn.OriginalHook());
    }
}