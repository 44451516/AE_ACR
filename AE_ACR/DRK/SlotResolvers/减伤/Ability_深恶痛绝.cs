#region

using AE_ACR_DRK_Setting;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.JobApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_深恶痛绝 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (DKSettings.Instance.日常模式 && 深恶痛绝.ActionReady() && !HasEffect(Buffs.深恶痛绝))
        {
            return 0;
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(深恶痛绝.OriginalHook());
    }
}