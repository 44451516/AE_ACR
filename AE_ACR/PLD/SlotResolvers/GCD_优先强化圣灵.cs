#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_优先强化圣灵 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }

        if (!getQTValue(PLDQTKey.优先圣灵))
        {
            return Flag_QT;
        }

        if (和目标的距离() > 25f)
        {
            return Flag_超出攻击距离;
        }

        if (GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
        {

            if (圣灵HolySpirit.MyIsUnlock())
            {
                if (HasEffect(Buffs.DivineMight))
                {
                    return 0;

                }
            }

        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(圣灵HolySpirit.OriginalHook());
    }
}