using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.GLA.SlotResolvers;

public class DK_GCD_血溅 : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    // 返回>=0表示检测通过 即将调用Build方法
    public int Check()
    {
        if (DKData.血溅Bloodspiller.IsUnlock())
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
                if (Blood > 50 || GameObjectExtension.HasAura(Core.Me, DKData.Buffs.血乱Delirium, 0))
                {
                    return 0;
                }
            }
        }


        if (DKSettings.Instance.GCD爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
        {
            return -1;
        }


        if (Blood > 50 || GameObjectExtension.HasAura(Core.Me, DKData.Buffs.血乱Delirium, 0))
        {
            //防止血溅没有打完
            if (Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, DKData.Buffs.血乱Delirium, true) < 8000)
            {
                return 0;
            }

            if (RaidBuff.爆发期())
            {
                return 0;
            }


            if (Blood >= 70 && GameObjectExtension.HasAura(Core.Me, DKData.Buffs.嗜血BloodWeapon))
            {
                return 0;
            }
            

            return -1;
        }


        return -2;
    }

    public static Spell GetBaseGCD()
    {
        Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.血溅Bloodspiller).GetSpell();
        return spell;
    }


    // 将指定技能加入技能队列中
    public void Build(Slot slot)
    {
        Spell spell = GetBaseGCD();
        slot.Add(spell);
    }
}