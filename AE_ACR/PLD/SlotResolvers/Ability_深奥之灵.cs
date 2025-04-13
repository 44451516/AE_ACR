#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_深奥之灵 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }
        
        
        if (!getQTValue(PLDQTKey.深奥之灵))
        {
            return Flag_QT;
        }




        if (CanWeave())
        {
            if (深奥之灵SpiritsWithin.ActionReady2())
            {
                if (和目标的距离() > PLDSettings.Instance.近战最大攻击距离)
                {
                    return Flag_超出攻击距离;
                }

                if (getQTValue(PLDQTKey.即刻厄运_深奥))
                {
                    return 1000;
                }
                
                if (HasEffect(Buffs.FightOrFlight))
                {
                    if (安魂祈祷Requiescat.MyIsUnlock())
                    {
                        if (GetCooldownRemainingTime(安魂祈祷Requiescat) > 40)
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (GetCooldownRemainingTime(战逃反应FightOrFlight) > 10 && GetCooldownRemainingTime(战逃反应FightOrFlight) < 40)
                {
                    return 0;
                }
            }
        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(深奥之灵SpiritsWithin.OriginalHook());
    }
}