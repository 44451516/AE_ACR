#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

/// <summary>
/// 没有学习大保健的使用用的
/// </summary>
public class GCD_大宝剑连击_圣灵_圣环 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!getQTValue(PLDQTKey.大宝剑连击))
        {
            return Flag_QT;
        }


        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (HasEffect(Buffs.Requiescat) && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
        {
            if (和目标的距离() > 25f)
            {
                return Flag_超出攻击距离;
            }
            
            
            if (圣灵HolySpirit.MyIsUnlock())
            {
                return 0;
            }

        }


        return -1;
    }


    public override void Build(Slot slot)
    {

        var spell = 圣灵HolySpirit.OriginalHook();

        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        
        if (圣环HolyCircle.MyIsUnlock())
        {
            if (aoeCount > 2)
            {
                spell = 圣环HolyCircle.GetSpell();
            }
        }
        
        slot.Add(spell);
    }
}