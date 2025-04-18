﻿#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.PLD.SlotResolvers.减伤;

public class Ability_神圣领域 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (!是否减伤())
        {
            return Flag_减伤;
        }

        if (PLDSettings.Instance.自动无敌 == false)
        {
            return Flag_减伤;
        }


        if (CanWeave())
        {
            //判断多少人打自己？ 再判断铁壁的id
            if (TankBuffs.亲疏自行.GetBuffRemainingTime() > 500)
                return -1;

            if (TankBuffs.铁壁.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.预警.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.预警v2.GetBuffRemainingTime() > 500)
                return -1;

            if (Buffs.壁垒.GetBuffRemainingTime() > 500)
                return -1;

            if (周围敌人雪仇数量() >= 5)
                return -1;


            if (神圣领域.ActionReady() && attackMeCount() >= 4 && Core.Me.CurrentHpPercent() < 0.65f)
                return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(神圣领域.OriginalHook());
    }
}