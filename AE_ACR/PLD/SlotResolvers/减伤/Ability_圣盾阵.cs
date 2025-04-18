﻿#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers.减伤;

public class Ability_圣盾阵 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }


        if (CanWeave())
        {
            if (圣盾阵.ActionReady() == false)
            {
                return Flag_CD;
            }
            
            if (神圣领域.ActionReady())
                return -1;
            

            if (Buffs.神圣领域.GetBuffRemainingTime() > 500)
            {
                return -1;
            }

            if (Buffs.预警.GetBuffRemainingTime() > 500)
            {
                return -2;
            }

            if (Buffs.预警v2.GetBuffRemainingTime() > 500)
            {
                return -3;
            }

            if (Buffs.壁垒.GetBuffRemainingTime() > 500)
            {
                return -4;
            }


            if (圣盾阵.OriginalHookActionReady() && Core.Resolve<JobApi_Paladin>().Oath >= 50)
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (Core.Me.CurrentHpPercent() <= 0.65f)
                    {
                        if (attackMeCount() >= 3)
                        {
                            return 0;
                        }

                        if (TargetHelper.IsBoss(target))
                        {
                            return 0;
                        }

                        if (TargetHelper.targetCastingIsDeathSentenceWithTime(target, 3) && target.TargetObjectId== Core.Me.GameObjectId)
                        {
                            return 0;
                        }
                    }
                }


            }

        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(圣盾阵.OriginalHook());
    }
}