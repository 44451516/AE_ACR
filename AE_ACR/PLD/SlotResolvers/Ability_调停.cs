#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_调停 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!是否使用突进())
        {
            return Flag_QT;
        }

        if (CanWeave())
        {
            if (调停Intervene.ActionReady() && !WasLastAction(调停Intervene))
            {
                if (HasEffect(Buffs.FightOrFlight))
                {
                    return 0;
                }

                if (RaidBuff.爆发期_120() && GetCooldownRemainingTime(战逃反应FightOrFlight) >= 15)
                {
                    return 0;
                }

                // if (调停Intervene.GetCooldownRemainingTime() == 0)
                // {
                //     return 0;
                // }

            }

        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(调停Intervene.OriginalHook());
    }
}