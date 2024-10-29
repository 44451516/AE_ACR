#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

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
            
            if (雪仇.IsReady() == false)
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


            if (雪仇.ActionReady() && attackMeCount() >= 3 && Core.Me.CurrentHpPercent() < 0.99f)
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(雪仇.OriginalHook());
    }
}