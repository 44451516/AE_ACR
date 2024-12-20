﻿#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_弃明投暗 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {
            if (暗影墙.ActionReady())
            {
                return -1;
            }

            if (暗影墙.GetCooldownRemainingTime() > 115)
            {
                return -1;
            }

            
            if (Buffs.暗影墙v2.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.暗影墙v2.GetBuffRemainingTime() > 500)
                return -1;

            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 500)
                return -1;

            if (弃明投暗.ActionReady() && attackMeCount() >= 5 && Core.Me.CurrentHpPercent() < 0.89f)
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(弃明投暗.OriginalHook());
    }
}