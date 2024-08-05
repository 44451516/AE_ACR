#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_优先圣灵 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }


        if (getQTValue(PLDQTKey.优先圣灵))
        {
            if (圣灵HolySpirit.IsUnlock() && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
            {
                return 0;
            }
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(圣灵HolySpirit.OriginalHook());
    }
}