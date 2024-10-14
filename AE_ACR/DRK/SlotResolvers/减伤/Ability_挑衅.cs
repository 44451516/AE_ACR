#region

using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class Ability_挑衅 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否日常模式())
        {
            return Flag_减伤;
        }

        if (DKSettings.Instance.挑衅 == false)
        {
            return Flag_减伤;
        }


        if (挑衅.GetCooldownRemainingTime() != 0)
        {
            return Flag_CD;
        }

        if (PartyHelper.Party.Count < 2)
        {
            return Flag_小队人数不够;
        }
        
        if (Core.Resolve<MemApiDuty>().IsBoundByDuty() == false)
        {
            return Flag_不在副本里面;
        }

        if (Data.IsInHighEndDuty == true)
        {
            return Flag_不是日常本;
        }
        
        if (!挑衅.ActionReadyAE())
        {
            return Flag_CD;
        }

        if (CanWeave())
        {
            if (CombatTime.Instance.CombatEngageDuration().TotalSeconds >= 5)
            {
                if (挑衅.ActionReady())
                {
                    foreach (var keyValuePair in TargetMgr.Instance.EnemysIn25)
                    {
                        var battleChara = keyValuePair.Value;
                        if (battleChara.仇恨是否在自己身上() == false)
                        {
                            return 0;
                        }
                    }
                }

            }
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        foreach (var keyValuePair in TargetMgr.Instance.EnemysIn25)
        {
            var battleChara = keyValuePair.Value;
            if (battleChara.仇恨是否在自己身上() == false)
            {
                slot.Add(new Spell(挑衅, battleChara));
            }
        }


    }
}