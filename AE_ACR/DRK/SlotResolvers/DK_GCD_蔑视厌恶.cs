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

public class DK_GCD_蔑视厌恶 : ISlotResolver
{
    public int Check()
    {
        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0) return -1;

        if (DKData.蔑视厌恶Disesteem.IsReady() == false) return -1;

        if (DKSettings.Instance.GCD爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds) return -1;


        if (Core.Me.HasAura(DKData.Buffs.Scorn)) return 0;


        return -1;
    }


    // 将指定技能加入技能队列中
    public void Build(Slot slot)
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.蔑视厌恶Disesteem).GetSpell();
        slot.Add(spell);
    }
}