#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.GLA.SlotResolvers.减伤;

public class Ability_铁壁 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            //判断多少人打自己？ 再判断铁壁的id
            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 0.5F)
                return -1;

            if (Buffs.预警.GetBuffRemainingTime() > 0.5f) 
                return -1;

            if (铁壁.ActionReady())
            {
                if (TargetHelper.GetNearbyEnemyCount(5) >= 4)
                {
                    return 0; 
                }
                
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.targetCastingIsDeathSentenceWithTime(target, 3))
                    {
                        return 0;
                    }
                }
            }
                
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(铁壁.OriginalHook());
    }
}