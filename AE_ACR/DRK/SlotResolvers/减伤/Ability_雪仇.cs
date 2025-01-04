#region

using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.DRK.SlotResolvers.减伤;

public class Ability_雪仇 : DRKBaseSlotResolvers
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
            
            if (铁壁.ActionReady())
                return -1;

            if (暗影墙.ActionReady())
                return -1;
            
            if (铁壁.RecentlyUsed())
            {
                return -1;
            }
            
            if (暗影墙.RecentlyUsed())
            {
                return -1;
            }

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.暗影墙.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.暗影墙v2.GetBuffRemainingTime() > 500)
                return -1;
            
            if (雪仇.ActionReady())
            {
                if (Core.Me.TargetObject is IBattleChara target)
                {
                    if (DKSettings.Instance.AOE雪仇 && TargetHelper.TargercastingIsbossaoe(target,14_000))
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