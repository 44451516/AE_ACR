#region

using AE_ACR_DRK;
using AE_ACR.Base;
using AE_ACR.utils;
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
        
        if (getQTValue(BaseQTKey.攒资源))
        {
            return Flag_攒资源;
        }
        
        if (!CanWeave())
        {
            return -1;
        }

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

        if (darksideTimeRemaining == 0) 
            return -2;


        if (LivingShadow.ActionReady()) 
            return 0;


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(LivingShadow.GetSpell());
    }
}