﻿#region

using AE_ACR.DRK.SlotResolvers;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers.减伤;

public class Ability_圣光幕帘 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }
        
        if (PLDSettings.Instance.AOE幕帘 == false)
        {
            return Flag_QT;
        }
        
        if (CanWeave())
        {
            if (圣光幕帘.ActionReady() )
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (TargetHelper.targetCastingIsBossAOE(target,15_000))
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
        slot.Add(圣光幕帘.OriginalHook());
    }
}