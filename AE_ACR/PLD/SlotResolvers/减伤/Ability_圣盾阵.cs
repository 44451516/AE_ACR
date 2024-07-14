#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

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
            // if (神圣领域.ActionReady())
            // {
            //     return -1;
            // }


            if (Buffs.神圣领域.GetBuffRemainingTime() > 0.5f)
            {
                return -1;
            }

            if (Buffs.预警.GetBuffRemainingTime() > 0.5f)
            {
                return -2;
            }

            if (Buffs.预警v2.GetBuffRemainingTime() > 0.5f)
            {
                return -3;
            }

            if (Buffs.壁垒.GetBuffRemainingTime() > 0.5f)
            {
                return -4;
            }


            if (圣盾阵.OriginalHookActionReady() && Core.Resolve<JobApi_Paladin>().Oath >= 50)
            {
                if (Core.Me.CurrentHpPercent() <= 0.65f)
                {
                    if (attackMeCount() >= 3)
                    {
                        return 0;
                    }
                
                    if (Core.Me.TargetObject is IBattleChara target)
                    {
                        if (TargetHelper.IsBoss(target))
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