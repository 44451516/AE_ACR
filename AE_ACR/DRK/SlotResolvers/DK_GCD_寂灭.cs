#region

using AE_ACR.DRK.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;

#endregion

namespace AE_ACR_DRK.SlotResolvers;

public class DK_GCD_寂灭 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }
        
        if (寂灭Quietus.IsUnlock())
        {
            return -1;
        }


        var Blood = Core.Resolve<JobApi_DarkKnight>().Blood;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;
        if (darksideTimeRemaining == 0)
        {
            return -1;
        }


        if (Blood > 50 || Core.Me.HasAura(Buffs.血乱Delirium))
        {
            if (TargetHelper.GetNearbyEnemyCount(5) > 2)
            {
                return 0;
            }

        }


        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(寂灭Quietus.OriginalHook());
    }
}