#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_Ability_精雕怒斩 : DRKBaseSlotResolvers
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


        if (Core.Resolve<MemApiSpell>().CheckActionChange(精雕怒斩CarveAndSpit).IsReady() == false) 
            return -3;

        if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds) 
            return -1;


        if (Core.Resolve<MemApiSpell>().CheckActionChange(精雕怒斩CarveAndSpit).IsReady()) 
            return 0;


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(精雕怒斩CarveAndSpit.OriginalHook());
    }
}