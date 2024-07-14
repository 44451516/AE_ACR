using AE_ACR_DRK_Setting;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace AE_ACR_DRK.SlotResolvers;

public class DK_GCD_AOE_Base : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == DKData.单体2SyphonStrike) return -1;

        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == DKData.释放Unleash) return 0;


        if (TargetHelper.GetNearbyEnemyCount(5) >= 2) return 0;

        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        if (LastBaseGcd == DKData.释放Unleash && DKSettings.Instance.留资源 == false)
            if (Core.Resolve<JobApi_DarkKnight>().Blood >= 80 && DKData.寂灭Quietus.IsUnlock())
            {
                var spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.寂灭Quietus).GetSpell();

                return spell;
            }

        if (LastBaseGcd == DKData.释放Unleash && DKData.刚魂StalwartSoul.IsUnlock())
            return DKData.刚魂StalwartSoul.GetSpell();

        return DKData.释放Unleash.GetSpell();
    }

    public void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}