#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_铁壁 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否减伤()) return -1;

        if (CanWeave())
        {
            if (神圣领域.ActionReady()) 
                return -1;

            if (预警.ActionReady()) 
                return -1;

            if (Buffs.神圣领域.GetBuffRemainingTime() > 0.5f) 
                return -1;

            if (Buffs.预警.GetBuffRemainingTime() > 0.5f) 
                return -1;

            if (Buffs.预警v2.GetBuffRemainingTime() > 0.5f) 
                return -1;

            if (Buffs.壁垒.GetBuffRemainingTime() > 0.5f) 
                return -1;


            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 0.5f) 
                return -1;


            if (铁壁.ActionReady() && attackMeCount() >= 5 && Core.Me.CurrentHpPercent() < 0.89f) 
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(铁壁.OriginalHook());
    }
}