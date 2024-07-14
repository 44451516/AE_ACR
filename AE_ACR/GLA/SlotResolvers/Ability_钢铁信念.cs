#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public class Ability_钢铁信念 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (!HasEffect(Buffs.钢铁信念) && 钢铁信念.ActionReady()) 
            return 0;

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(钢铁信念.OriginalHook());
    }
}