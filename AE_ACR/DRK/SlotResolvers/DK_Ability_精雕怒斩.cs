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

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_精雕怒斩 : ISlotResolver
{
    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 600) return -1;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0) return -2;


        if (Core.Resolve<MemApiSpell>().CheckActionChange(DKData.精雕怒斩CarveAndSpit).IsReady() == false) return -3;

        if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds) return -1;


        if (Core.Resolve<MemApiSpell>().CheckActionChange(DKData.精雕怒斩CarveAndSpit).IsReady() == true) return 0;


        return -1;
    }


    public void Build(Slot slot)
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.精雕怒斩CarveAndSpit).GetSpell();

        slot.Add(spell);
    }
}