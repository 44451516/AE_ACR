﻿#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers.减伤;

public class Ability_预警 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }


        if (CanWeave())
        {
            if (神圣领域.ActionReady())
                return -1;

            if (神圣领域.RecentlyUsed())
            {
                return -1;
            }


            if (Buffs.神圣领域.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (Buffs.壁垒.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 0.5f)
                return -1;

            if (预警.ActionReady())
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.TargercastingIsDeathSentence(target, 5) && 圣盾阵.MyIsUnlock())
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
        slot.Add(预警.OriginalHook());
    }
}