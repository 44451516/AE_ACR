#region

using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLDUsePotion : PLDBaseSlotResolvers
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
                if (lastComboActionID is 暴乱剑RiotBlade && 战逃反应FightOrFlight.ActionReady() && 安魂祈祷Requiescat.ActionReady())
                {
                    return 1;
                }

                if (战逃反应FightOrFlight.GetCooldownRemainingTime() <= 5)
                {
                    if (GetBuffRemainingTime(Buffs.赎罪剑Atonement1BUFF) >= 14)
                    {
                        return 0;
                    }

                    //  战逃内 圣灵 3 圣灵
                    if (GetBuffRemainingTime(Buffs.DivineMight) >= 14 && lastComboActionID == 暴乱剑RiotBlade && comboTime >= 16)
                    {
                        return 0;
                    }

                    //  战逃内 赎罪剑3 3 圣灵
                    if (GetBuffRemainingTime(Buffs.赎罪剑Atonement3BUFF) >= 14 && lastComboActionID == 暴乱剑RiotBlade && comboTime >= 16)
                    {
                        return 0;
                    }

                    //  战逃内 赎罪剑 2 3 圣灵
                    if (GetBuffRemainingTime(Buffs.DivineMight) >= 14 && GetBuffRemainingTime(Buffs.赎罪剑Atonement2BUFF) >= 14)
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
        if (Check() == 1)
        {
            slot.Add2NdWindowAbility(Spell.CreatePotion());
            // slot.Wait2NextGcd = true; 
        }
        else
        {
            slot.Add(Spell.CreatePotion());
        }

    }
}