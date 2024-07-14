#region

using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public class Ability_预警 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            //判断多少人打自己？ 再判断铁壁的id
            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 0.5f) 
                return -1;

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 0.5f) 
                return -1;

            if (预警.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) >= 4) 
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(预警.OriginalHook());
    }
}