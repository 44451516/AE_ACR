#region

using AE_ACR.PLD.Setting;
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
            if (和目标的距离() > PLDSettings.Instance.近战最大攻击距离)
            {
                return Flag_超出攻击距离;
            }
            
            return 0;
        }

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(沥血剑GoringBlade.OriginalHook());
    }
}