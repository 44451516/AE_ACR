﻿using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.GLA.SlotResolvers
{
    public class Ability_厄运流转 : GLABaseSlotResolvers
    {
        public override int Check()
        {
            if (CanWeave())
            {
                if (厄运流转CircleOfScorn.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) > 0)
                {
                    return 0;
                }
            }

            return -1;
        }


        public override void Build(Slot slot)
        {
            slot.Add(厄运流转CircleOfScorn.OriginalHook());
        }
    }
}