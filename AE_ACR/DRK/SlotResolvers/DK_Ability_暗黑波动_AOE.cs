#region

using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_暗黑波动_AOE : DRKBaseSlotResolvers
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
        
        
        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }

        
        if (!暗黑波动AOE.IsUnlock())
        {
            return -1;
        }

        if (!CanWeave())
        {
            return -1;
        }

        var gauge = Core.Resolve<JobApi_DarkKnight>();
        var darksideTimeRemaining = gauge.DarksideTimeRemaining;
        if (Core.Me.CurrentMp >= 3000 || gauge.HasDarkArts)
        {
            var battleChara = Core.Me.GetCurrTarget();
            if (TargetHelper.GetNearbyEnemyCount(battleChara, 10, 4) >= 3)
            {
                if (darksideTimeRemaining <= 10 * 1000)
                    return 0;

                if (Core.Me.CurrentMp >= 9800)
                    return 0;

                if (Core.Me.CurrentMp >= 9800)
                    return 0;

                if (Core.Me.CurrentMp >= 9200 && lastComboActionID == 单体2SyphonStrike)
                    return 0;

                if (Core.Me.CurrentMp >= 9200 && lastComboActionID == 释放Unleash)
                    return 0;

                if (Core.Me.CurrentMp >= 8600 && lastComboActionID == 释放Unleash && Core.Me.HasAura(Buffs.嗜血BloodWeapon))
                    return 0;

                if (Core.Me.CurrentMp >= 8600 && lastComboActionID == 单体2SyphonStrike && Core.Me.HasAura(Buffs.嗜血BloodWeapon))
                    return 0;
                if (Core.Me.CurrentMp >= 9200 && Core.Me.HasAura(Buffs.嗜血BloodWeapon))
                    return 0;
                if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
                    return -1;

                //泄蓝
                if (血乱Delirium.GetCooldownRemainingTime() > 40 && 暗黑波动AOE.ActionReady() && Core.Me.CurrentMp > DKSettings.Instance.保留蓝量 + 3000)
                {
                    return 0;
                }
            }
        }

        return -3;
    }

    public override void Build(Slot slot)
    {
        slot.Add(暗黑波动AOE.OriginalHook());
    }
}