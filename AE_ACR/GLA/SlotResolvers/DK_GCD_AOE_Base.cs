using AE_ACR_DRK_Setting;
using AE_ACR.GLA.Data;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace AE_ACR.GLA.SlotResolvers;

public class DK_GCD_AOE_Base : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == Data.Data.单体2SyphonStrike)
        {
            return -1;
        }
        
        if (Core.Resolve<MemApiSpell>().GetLastComboSpellId() == Data.Data.释放Unleash)
        {
            return 0;
        }


        
        if (TargetHelper.GetNearbyEnemyCount(5) >= 2)
        {
            return 0;
        }
        
        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        if (LastBaseGcd == Data.Data.释放Unleash && DKSettings.Instance.留资源 == false)
        {
            if (Core.Resolve<JobApi_DarkKnight>().Blood >= 80 && Data.Data.寂灭Quietus.IsUnlock())
            {
                Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(Data.Data.寂灭Quietus).GetSpell();

                return spell;
            }
        }

        if (LastBaseGcd == Data.Data.释放Unleash && Data.Data.刚魂StalwartSoul.IsUnlock())
            return Data.Data.刚魂StalwartSoul.GetSpell();

        return Data.Data.释放Unleash.GetSpell();
    }

    public void Build(Slot slot)
    {
        Spell spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}