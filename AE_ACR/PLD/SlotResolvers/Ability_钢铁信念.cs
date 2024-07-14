#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_钢铁信念 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (PLDSettings.Instance.日常模式 && !HasEffect(Buffs.钢铁信念) && 钢铁信念.ActionReady())
        {
            return 0;
        }


        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(钢铁信念.OriginalHook());
    }
}