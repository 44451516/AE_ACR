#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

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

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 500)
            {
                return -1;
            }

            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 500)
            {
                return -1;
            }
            
            if (暗影墙.ActionReady())
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.targetCastingIsDeathSentenceWithTime(target, 5) && target.TargetObjectId== Core.Me.GameObjectId)
                    {
                        return 0;
                    }
                    
                    if (attackMeCount() >= 3 && Core.Me.CurrentHpPercent() < 0.89f)
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
        slot.Add(暗影墙.OriginalHook());
    }
}