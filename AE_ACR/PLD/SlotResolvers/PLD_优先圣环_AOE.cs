#region

using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLD_优先圣环_AOE : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (getQTValue(PLDQTKey.优先圣环) == false)
        {
            return Flag_QT;
        }

        if (GetBaseGCDSpell() != null)
        {
            return 0;
        }

        return -1;
    }

    private Spell? GetBaseGCDSpell()
    {

        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        
        if (圣环HolyCircle.MyIsUnlock() && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp && aoeCount >= PLDSettings.Instance.USE_AOE)
        {
            //
            // if (GetBuffRemainingTime(Buffs.DivineMight) > 0 && GetBuffRemainingTime(Buffs.DivineMight) <= 3)
            // {
            //     return 圣环HolyCircle.OriginalHook();
            // }
            //
            // if (lastComboActionID == 全蚀斩TotalEclipse && HasEffect(Buffs.DivineMight))
            // {
            //     if (aoeCount >= PLDSettings.Instance.USE_AOE)
            //     {
            //         return 圣环HolyCircle.OriginalHook();
            //     }
            // }
            if (HasEffect(Buffs.DivineMight))
            {
                if (aoeCount >= PLDSettings.Instance.USE_AOE)
                {
                    return 圣环HolyCircle.OriginalHook();
                }
            }
        }

        return null;
    }

    public override void Build(Slot slot)
    {
        var spell = GetBaseGCDSpell();
        if (spell != null)
        {
            slot.Add(spell);
        }
    }
}