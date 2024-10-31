#region

using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLD_GCD_Base_AOE : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (getQTValue(BaseQTKey.AOE) == false)
        {
            return Flag_QT;
        }


        if (lastComboActionID == 暴乱剑RiotBlade && 战女神之怒RageOfHalone.MyIsUnlock())
        {
            return -1;
        }

        if (GetBaseGCDSpell() == null)
        {
            return -5;
        }

        if (lastComboActionID == 全蚀斩TotalEclipse && 日珥斩Prominence.MyIsUnlock())
        {
            return 0;
        }

        if (TargetHelper.GetNearbyEnemyCount(5) >= 2)
        {
            if (全蚀斩TotalEclipse.MyIsUnlock())
            {
                return 0;
            }

        }


        return -1;
    }

    private Spell? GetBaseGCDSpell()
    {

        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        
        if (lastComboActionID == 全蚀斩TotalEclipse && HasEffect(Buffs.DivineMight) && 圣环HolyCircle.MyIsUnlock())
        {
            if (aoeCount >= 3)
            {
                return 圣环HolyCircle.OriginalHook();
            }
            else
            {
                return 圣灵HolySpirit.OriginalHook();
            }

        }

        
        if (lastComboActionID == 全蚀斩TotalEclipse && 日珥斩Prominence.MyIsUnlock())
        {
            return 日珥斩Prominence.OriginalHook();
        }
        
        if (全蚀斩TotalEclipse.MyIsUnlock())
        {
            return 全蚀斩TotalEclipse.OriginalHook();
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