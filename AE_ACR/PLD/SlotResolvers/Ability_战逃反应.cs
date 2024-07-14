﻿#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_战逃反应 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (CanWeave())
            if (战逃反应FightOrFlight.ActionReady())
            {
                if (!王权剑RoyalAuthority.IsUnlock()) return 0;

                if (王权剑RoyalAuthority.IsUnlock())
                {
                    //  战逃内 打三下赎罪剑
                    if (GetBuffRemainingTime(Buffs.赎罪剑Atonement1BUFF) >= 14) return 0;

                    //  战逃内 圣灵 3 圣灵
                    if (GetBuffRemainingTime(Buffs.DivineMight) >= 14 && lastComboActionID == 暴乱剑RiotBlade && comboTime >= 16) return 0;

                    //  战逃内 赎罪剑3 3 圣灵
                    if (GetBuffRemainingTime(Buffs.赎罪剑Atonement3BUFF) >= 14 && lastComboActionID == 暴乱剑RiotBlade && comboTime >= 16) return 0;

                    //  战逃内 赎罪剑 2 3 圣灵
                    if (GetBuffRemainingTime(Buffs.DivineMight) >= 14 && GetBuffRemainingTime(Buffs.赎罪剑Atonement2BUFF) >= 14) return 0;
                }
            }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(战逃反应FightOrFlight.GetSpell());
    }
}