#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;

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

        if (!getQTValue(PLDQTKey.战逃安魂))
        {
            return Flag_QT;
        }


        if (PLDSettings.Instance.日常模式)
        {
            if (PLDSettings.Instance.日常模式_残血不打爆发)
            {
                if (战斗爽() == false)
                {
                    return Flag_残血不打爆发;
                }
            }
        }
        
        
        if (CanWeave())
        {
            if (战逃反应FightOrFlight.ActionReady())
            {
                if (getQTValue(PLDQTKey.即刻战逃))
                {
                    return 0;
                }

                //没有学习 只用用吧
                if (!王权剑RoyalAuthority.MyIsUnlock())
                {
                    return 0;
                }

                if (lastComboActionID == 全蚀斩TotalEclipse)
                {
                    var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
                    if (aoeCount >= 2)
                    {
                        return 0;
                    }
                }


                if (王权剑RoyalAuthority.MyIsUnlock())
                {
                    //  战逃内 打三下赎罪剑
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

                    //2024年11月5日20:24:50 
                    if (lastGCDActionID == 赎罪剑Atonement3 && lastComboActionID == 暴乱剑RiotBlade && comboTime >= 16)
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
        Spell spell = new Spell(战逃反应FightOrFlight,Core.Me);
        slot.Add(spell);
    }
}