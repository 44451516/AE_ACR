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
        
        
        if (!getQTValue(PLDQTKey.厄运流转))
        {
            return Flag_QT;
        }

        if (CanWeave())
        {
            if (厄运流转CircleOfScorn.OriginalHookActionReady2() && TargetHelper.GetNearbyEnemyCount(5) > 0)
            {
                
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
        slot.Add(厄运流转CircleOfScorn.OriginalHook());
    }
}