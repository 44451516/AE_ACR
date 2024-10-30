#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_暗黑锋 : DRKBaseSlotResolvers
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

        if (getQTValue(DRKQTKey.不打暗影峰))
        {
            return Flag_攒资源;
        }
        
        if (!暗黑锋.ActionReadyAE())
        {
            return -1;
        }

        if (!CanWeave())
        {
            return -1;
        }


        if (LastSpell == Core.Resolve<MemApiSpell>().CheckActionChange(暗黑锋).GetSpell().Id)
        {
            return -1;
        }


        var gauge = Core.Resolve<JobApi_DarkKnight>();

        var darksideTimeRemaining = gauge.DarksideTimeRemaining;

        if (Core.Me.CurrentMp >= 3000 || gauge.HasDarkArts)
        {
            if (和目标的距离() > DKSettings.Instance.近战最大攻击距离)
            {
                return Flag_超出攻击距离;
            }
            
            if (getQTValue(BaseQTKey.倾泻资源))
            {
                return 1;
            }
            
            if (getQTValue(DRKQTKey.卸掉豆子) && gauge.HasDarkArts)
            {
                return 2;
            }
            
            if (darksideTimeRemaining <= 6 * 1000)
            {
                return 0;
            }

          
            if (Core.Me.CurrentMp >= 9800)
            {
                return 0;
            }

            if (Core.Me.CurrentMp >= 9800)
            {
                return 0;
            }

            if (Core.Me.CurrentMp >= 9200 && lastComboActionID == 单体1HardSlash)
            {
                return 0;
            }

            if (Core.Me.CurrentMp >= 8600 && lastComboActionID == 单体1HardSlash && Core.Me.HasAura(Buffs.嗜血BloodWeapon))
            {
                return 0;
            }

            if (Core.Me.CurrentMp >= 9200 && Core.Me.HasAura(Buffs.嗜血BloodWeapon))
            {
                return 0;
            }

            if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
            {
                return -1;
            }


            if (RaidBuff.爆发期_120() && Core.Me.CurrentMp > DKSettings.Instance.保留蓝量 + 3000)
            {
                return 0;
            }
            
            if (Core.Me.TargetObject is IBattleChara chara)
            {
                if (chara.CurrentHp <= DKSettings.Instance.get爆发目标血量())
                {
                    return 0;
                }
            }

            //泄蓝
            if (血乱Delirium.GetCooldownRemainingTime() > 40 && 暗黑锋.ActionReady() && Core.Me.CurrentMp > DKSettings.Instance.保留蓝量 + 3000)
            {
                return 0;
            }
        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(暗黑锋.OriginalHook());
    }
}