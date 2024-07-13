using AE_ACR_DRK_Setting;
using AE_ACR.GLA.Data;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace AE_ACR.GLA.SlotResolvers;

public class GCD_Base : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    public int Check()
    {

        return 0;
    }

    private Spell GetAOEGCDSpell()
    {
        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        if (aoeCount >= 2 && Data.Data.全蚀斩TotalEclipse.IsUnlock())
        {
            return Data.Data.全蚀斩TotalEclipse.OriginalHook();
        }

        if (LastBaseGcd == Data.Data.先锋剑FastBlade && Data.Data.暴乱剑RiotBlade.IsUnlock())
        {
            return Data.Data.暴乱剑RiotBlade.GetSpell();
        }

        if (LastBaseGcd == Data.Data.暴乱剑RiotBlade && Data.Data.战女神之怒RageOfHalone.IsUnlock())
        {
            return Data.Data.战女神之怒RageOfHalone.GetSpell();
        }


        return Data.Data.先锋剑FastBlade.OriginalHook();
    }

    public void Build(Slot slot)
    {
        Spell spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}