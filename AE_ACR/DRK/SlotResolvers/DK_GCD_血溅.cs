#region

using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR_DRK.SlotResolvers;

public class DK_GCD_血溅 : DRKBaseSlotResolvers
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
        
        if (!血溅Bloodspiller.IsUnlock())
        {
            return -1;
        }

        var Blood = Core.Resolve<JobApi_DarkKnight>().Blood;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;
        if (darksideTimeRemaining == 0)
        {
            return -1;
        }

        if (Core.Me.TargetObject is IBattleChara chara)
        {
            if (chara.CurrentHp <= DKSettings.Instance.get爆发目标血量())
            {
                if (Blood > 50 || Core.Me.HasAura(Buffs.血乱Delirium))
                {
                    // AELoggerUtils.Log("血溅1");
                    return 0;
                }
            }
        }


        if (DKSettings.Instance.GCD爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
        {
            return -1;
        }


        if (Blood > 50 || Core.Me.HasAura(Buffs.血乱Delirium))
        {
            //防止血溅没有打完
            if (Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, Buffs.血乱Delirium, true) < 8000)
            {
                return 0;
            }

            if (RaidBuff.爆发期_120())
            {
                return 0;
            }


            if (Blood >= 70 && Core.Me.HasAura(Buffs.嗜血BloodWeapon))
            {
                return 0;
            }


            return -1;
        }


        return -2;
    }


    // 将指定技能加入技能队列中
    public override void Build(Slot slot)
    {
        var spell = GetBaseGCD();
        slot.Add(spell);
    }

    public static Spell GetBaseGCD()
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(血溅Bloodspiller).GetSpell();
        return spell;
    }
}