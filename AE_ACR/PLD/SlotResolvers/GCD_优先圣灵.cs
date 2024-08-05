#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

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
            return 0;
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(圣灵HolySpirit.OriginalHook());
    }
}