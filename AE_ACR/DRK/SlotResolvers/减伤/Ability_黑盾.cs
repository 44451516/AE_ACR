#region

using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.JobApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_黑盾 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {

            if (Buffs.暗影墙.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (Buffs.暗影墙v2.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (至黑之夜.ActionReady() && Core.Me.CurrentMp >= 3000 && attackMeCount() >= 5 && Core.Me.CurrentHpPercent() < 0.88f)
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(至黑之夜.OriginalHook());
    }
}