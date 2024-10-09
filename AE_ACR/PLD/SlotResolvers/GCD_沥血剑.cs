#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class GCD_沥血剑 : PLDBaseSlotResolvers
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

        if (HasEffect(Buffs.沥血剑BUFFGoringBladeReady))
        {
            if (Core.Me.TargetObject.DistanceToPlayer() < 3)
            {
                return 0;
            }
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(沥血剑GoringBlade.OriginalHook());
    }
}