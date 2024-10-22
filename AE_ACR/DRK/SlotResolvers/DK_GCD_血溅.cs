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
        
        if (getQTValue(DRKQTKey.不打血溅))
        {
            return Flag_攒资源;
        }

        if (!血溅Bloodspiller.MyIsUnlock())
        {
            return -1;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }

        
        if (和目标的距离() > DKSettings.Instance.近战最大攻击距离)
        {
            return Flag_超出攻击距离;
        }

        var Blood = Core.Resolve<JobApi_DarkKnight>().Blood;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;
        if (darksideTimeRemaining == 0)
        {
            return -1;
        }

    

        if (DKSettings.Instance.GCD爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
        {
            return -1;
        }


        if (Blood >= 50 || Core.Me.HasAura(Buffs.血乱Delirium1) || Core.Me.HasAura(Buffs.血乱Delirium2))
        {
            
            if (getQTValue(BaseQTKey.倾斜资源))
            {
                return 1;
            }
            
            
            if (getQTValue(BaseQTKey.倾泻资源))
            {
                return 1;
            }
            
            if (Core.Me.TargetObject is IBattleChara chara)
            {
                if (chara.CurrentHp <= DKSettings.Instance.get爆发目标血量())
                {
                        return 0;
                }
            }

            
            //防止血溅没有打完
            if (Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, Buffs.血乱Delirium1, true) < 8000)
            {
                return 0;
            }

            //防止血溅没有打完
            if (Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, Buffs.血乱Delirium2, true) < 8000)
            {
                return 0;
            }

            if (RaidBuff.爆发期_120() && 掠影示现.GetCooldownRemainingTime() > 80)
            {
                return 0;
            }


            if (Blood >= 90 && Core.Me.GetAuraStack(Buffs.嗜血BloodWeapon) >= 2)
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