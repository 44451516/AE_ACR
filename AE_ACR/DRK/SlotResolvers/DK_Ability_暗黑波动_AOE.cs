#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_暗黑波动_AOE : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {
        if (DKData.暗黑波动AOE.IsUnlock()) return -1;
        if (GCDHelper.GetGCDCooldown() < 600) return -2;

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

                if (Core.Me.CurrentMp >= 9200 && LastBaseGcd == DKData.单体2SyphonStrike) return 0;
                if (Core.Me.CurrentMp >= 8600 && LastBaseGcd == DKData.单体2SyphonStrike && Core.Me.HasAura(DKData.Buffs.嗜血BloodWeapon)) return 0;
                if (Core.Me.CurrentMp >= 9200 && Core.Me.HasAura(DKData.Buffs.嗜血BloodWeapon)) return 0;
                if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds) return -1;

                //泄蓝
                if (DKData.血乱Delirium.GetCooldownRemainingTime() > 40 && Core.Me.CurrentMp > DKSettings.Instance.保留蓝量 + 3000) return 0;
            }
        }

        return -3;
    }

    public void Build(Slot slot)
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.暗黑波动AOE).GetSpell();
        slot.Add(spell);
    }
}