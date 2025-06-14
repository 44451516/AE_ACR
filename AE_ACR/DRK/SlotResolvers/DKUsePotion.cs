﻿#region

using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DKUsePotion : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!getQTValue(BaseQTKey.爆发药))
        {
            return Flag_爆发药;
        }

        if (BattleCharaUtils.爆发药Id() == 0)
        {
            return Flag_没有设置爆发药;
        }  
        
        if (BattleCharaUtils.爆发药数量() == 0)
        {
            return Flag_没有爆发药数量为0;
        }


        if (CanWeave())
        {
            if (爆发药冷却时间() == 0)
            {
                if (LivingShadow.ActionReady())
                {
                    return 0;
                }

                if (LivingShadow.MyIsUnlock() && GetCooldownRemainingTime(LivingShadow) < 5F)
                {
                    return 0;
                }
            }
        }


        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(Spell.CreatePotion());
    }
}