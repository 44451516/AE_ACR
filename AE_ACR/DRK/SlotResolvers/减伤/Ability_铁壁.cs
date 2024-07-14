#region

using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_铁壁 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {

            if (暗影墙.ActionReady())
            {
                return -1;
            }

         
            if (Buffs.暗影墙.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (Buffs.暗影墙v2.GetBuffRemainingTime() > 0.5f)
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