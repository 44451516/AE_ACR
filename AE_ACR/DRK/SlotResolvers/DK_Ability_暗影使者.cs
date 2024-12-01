#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_暗影使者 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (getQTValue(BaseQTKey.攒资源))
        {
            return Flag_攒资源;
        }


        if (!getQTValue(DRKQTKey.暗影使者))
        {
            return Flag_QT;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }

        if (DKSettings.Instance.日常模式)
        {
            if (DKSettings.Instance.日常模式_残血不打爆发)
            {
                if (战斗爽() == false)
                {
                    return Flag_残血不打爆发;
                }
            }
        }


        if (!CanWeave())
        {
            return -1;
        }

        if (LastSpell == Shadowbringer暗影使者)
        {
            return -1;
        }


        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0)
            return -2;

        if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
            return -1;


        if (Shadowbringer暗影使者.ActionReady())
        {
            if (Core.Me.TargetObject is IBattleChara chara)
            {
                if (chara.CurrentHp <= DKSettings.Instance.get爆发目标血量())
                {
                    return 0;
                }
            }

            
            if (费雷时间 is > 0 and <= 10_000)
            {
                return 0;
            }
            
            if (Shadowbringer暗影使者.GetCooldownRemainingTime() == 0)
            {
                if (掠影示现.GetCooldownRemainingTime() >= 50 && 掠影示现.GetCooldownRemainingTime() < 70)
                {
                    return 0;
                }
            } 
            
            //如果是高难模式
            // if (Data.IsInHighEndDuty == true)
            // {
            //
            //    
            // }
            // else
            // {
            //     if (Shadowbringer暗影使者.GetCooldownRemainingTime() == 0)
            //     {
            //         return 0;
            //     } 
            // }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(Shadowbringer暗影使者.OriginalHook());
    }
}