#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers.减伤;

public class Ability_雪仇 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }

        if (CanWeave())
        {
            if (雪仇.ActionReady() == false)
            {
                return Flag_CD;
            }

            
            if (神圣领域.ActionReady())
                return -1;


            if (铁壁.ActionReady())
                return -1;

            if (预警.ActionReady())
                return -1;

        

            if (Buffs.神圣领域.GetBuffRemainingTime() > 500)
                return -1;

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 500)
                return -1;


            if (Buffs.壁垒.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.预警.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.预警v2.GetBuffRemainingTime() > 500)
                return -1;


            if (雪仇.ActionReady())
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (PLDSettings.Instance.AOE雪仇 && TargetHelper.TargercastingIsbossaoe(target,14_000))
                    {
                        return 0;
                    }
                }

                if (attackMeCount() >= 3 && Core.Me.CurrentHpPercent() < 0.99f)
                {
                    return 0;
                }
            }
        }
               

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(雪仇.OriginalHook());
    }
}