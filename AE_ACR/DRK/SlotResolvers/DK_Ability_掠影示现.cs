#region

using AE_ACR_DRK;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_掠影示现 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }
        
        if (CanWeave()) 
            return -1;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0) 
            return -2;


        if (LivingShadow.IsReady()) 
            return 0;


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(LivingShadow.GetSpell());
    }
}