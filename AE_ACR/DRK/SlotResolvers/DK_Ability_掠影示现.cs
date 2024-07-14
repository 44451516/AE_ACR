#region

using AE_ACR_DRK;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_掠影示现 : ISlotResolver
{
    public int Check()
    {
        if (GCDHelper.GetGCDCooldown() < 600) return -1;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0) return -2;


        if (DKData.LivingShadow.IsReady()) return 0;


        return -1;
    }


    public void Build(Slot slot)
    {
        slot.Add(DKData.LivingShadow.GetSpell());
    }
}