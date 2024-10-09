#region

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

public class DK_GCD_蔑视厌恶 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0)
            return -1;

        if (蔑视厌恶Disesteem.IsReady() == false)
            return -1;

        if (DKSettings.Instance.GCD爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
            return -1;

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (Core.Me.HasAura(Buffs.Scorn))
            return 0;


        return -1;
    }

    public override void Build(Slot slot)
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(蔑视厌恶Disesteem).GetSpell();
        slot.Add(spell);
    }
}