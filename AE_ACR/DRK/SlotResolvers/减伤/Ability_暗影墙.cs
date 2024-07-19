#region

using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_暗影墙 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }
        
        if (CanWeave())
        {
            if (铁壁.GetCooldownRemainingTime() > 85)
            {
                return -1;
            }

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 0.5f)
            {
                return -1;
            }

            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 0.5f)
            {
                return -1;
            }

            if (暗影墙.ActionReady() && attackMeCount() >= 3 && Core.Me.CurrentHpPercent() < 0.89f)
            {
                return 0;
            }
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(暗影墙.OriginalHook());
    }
}