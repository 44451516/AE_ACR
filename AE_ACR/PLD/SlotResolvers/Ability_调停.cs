﻿#region

using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_调停 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!是否使用突进())
        {
            return Flag_QT;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }
        
        if (PLDRotationEntry.QT.GetQt(PLDQTKey.移动不打调停) && IsMoving())
        {
            return Flag_QT ;
        }


        if (CanWeave())
        {
            if (调停Intervene.ActionReady() && !WasLastAction(调停Intervene))
            {
                if (PLDRotationEntry.QT.GetQt(PLDQTKey.战逃打完调停))
                {
                    if (HasEffect(Buffs.FightOrFlight))
                    {
                        bool inAttackDistance = 和目标的距离() <= PLDSettings.Instance.最大突进距离;
                        if (inAttackDistance)
                        {
                            return 3;
                        }
                    }
                }

                if (调停Intervene.Charges() > PLDSettings.Instance.调停保留层数)
                {
                    bool inAttackDistance = 和目标的距离() <= PLDSettings.Instance.最大突进距离;
                    if (inAttackDistance)
                    {
                        if (HasEffect(Buffs.FightOrFlight))
                        {
                            return 1;
                        }

                    }
                }
            }
        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(调停Intervene.OriginalHook());
    }
}