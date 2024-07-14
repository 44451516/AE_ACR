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


        if (CanWeave())
        {
            if (是否使用突进())
            {
                if (调停Intervene.ActionReady() && !WasLastAction(调停Intervene))
                {
                    if (HasEffect(Buffs.FightOrFlight))
                    {
                        return 0;
                    }

                    if (RaidBuff.爆发期())
                    {
                        return 0;
                    }

                    if (调停Intervene.GetCooldownRemainingTime() == 0)
                    {
                        return 0;
                    }
                }
            }

        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(调停Intervene.OriginalHook());
    }
}