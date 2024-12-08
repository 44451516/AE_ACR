#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

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

        if (!getQTValue(PLDQTKey.战逃安魂))
        {
            return Flag_QT;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (CanWeave())
        {
            if (安魂祈祷Requiescat.OriginalHookActionReady())
            {
                if (安魂祈祷Requiescatv2.IsUnlock() == false && 和目标的距离() > PLDSettings.Instance.近战最大攻击距离)
                {
                    return Flag_超出攻击距离;
                }
                
                if (战逃反应FightOrFlight.RecentlyUsed())
                {
                    return 0;
                }
                
                if (WasLastAction(战逃反应FightOrFlight))
                {
                    return 0;
                }

                if (HasEffect(Buffs.FightOrFlight))
                {
                    return 0;
                }
                
                if (HasEffect(Buffs.荣耀之剑预备))
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