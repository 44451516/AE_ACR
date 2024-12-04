#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;
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

        if (getQTValue(BaseQTKey.攒资源))
        {
            return Flag_攒资源;
        }

        if (!getQTValue(DRKQTKey.蔑视厌恶))
        {
            return Flag_QT;
        }

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0)
            return -1;

        if (蔑视厌恶Disesteem.ActionReady() == false)
            return -1;

        if (DKSettings.Instance.GCD爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
            return -1;

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        // if (Buffs.Scorn.GetBuffRemainingTime() > 0 && Buffs.Scorn.GetBuffRemainingTime() < 4000)
        // {
        //     return 1;
        // }

        if (费雷时间 is > 0 and <= 10_000)
        {
            return 1;
        }

        if (RaidBuff.爆发期_120())
        {
            if (Buffs.Scorn.GetBuffRemainingTime() > 0 && Buffs.Scorn.GetBuffRemainingTime() < 20_000)
            {
                return 5;
            }
        }


        return -1;
    }

    public override void Build(Slot slot)
    {
        var spell = Core.Resolve<MemApiSpell>().CheckActionChange(蔑视厌恶Disesteem).GetSpell();
        slot.Add(spell);
    }
}