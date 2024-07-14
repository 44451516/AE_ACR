using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_大宝剑连击 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return -1;
        }

        if (HasEffect(Buffs.Requiescat) && GetResourceCost(大保健连击Confiteor) < Core.Me.CurrentMp)
        {
            return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        var spell = 圣灵HolySpirit.OriginalHook();
        if (BladeOfFaith.IsUnlock())
        {
            spell = 大保健连击Confiteor.OriginalHook();
        }

        slot.Add(spell);
    }
}