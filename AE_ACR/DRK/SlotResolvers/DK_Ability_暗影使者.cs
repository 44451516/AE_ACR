#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_暗影使者 : DRKBaseSlotResolvers
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

        if (!CanWeave())
        {
            return -1;
        }

        if (LastSpell == Shadowbringer暗影使者)
        {
            return -1;
        }


        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0)
            return -2;

        if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
            return -1;


        if (Shadowbringer暗影使者.ActionReady())
        {
            if (RaidBuff.爆发期())
            {
                return 0;
            }

            if (Shadowbringer暗影使者.GetCooldownRemainingTime() == 0)
            {
                return 0;
            }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(Shadowbringer暗影使者.OriginalHook());
    }
}