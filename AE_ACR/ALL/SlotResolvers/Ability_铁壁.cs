using AE_ACR.GLA.Data;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace AE_ACR.ALL.SlotResolvers;

public class Ability_铁壁 : ISlotResolver
{
    public int Check()
    {
        if (BaseIslotResolver.CanWeave())
        {
            //判断多少人打自己？ 再判断铁壁的id
            if (ALLData.Buffs.亲疏自行.GetBuffRemainingTime() > 500) return -1;
            if (Data.Buffs.预警.GetBuffRemainingTime() > 0.5f) return -1;


            if (ALLData.铁壁.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) >= 6) return 0;
        }

        return -1;
    }


    public void Build(Slot slot)
    {
        slot.Add(ALLData.铁壁.OriginalHook());
    }
}