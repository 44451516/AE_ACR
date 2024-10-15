#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public class Ability_预警 : GLABaseSlotResolvers
{
    public override int Check()
    {
        if (CanWeave())
        {
            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 0.5f)
                return -1;

            
            if (预警.ActionReady())
            {
                if (TargetHelper.GetNearbyEnemyCount(5) >= 4)
                {
                    return 0; 
                }
                
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.TargercastingIsDeathSentence(target, 3))
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
        slot.Add(预警.OriginalHook());
    }
}