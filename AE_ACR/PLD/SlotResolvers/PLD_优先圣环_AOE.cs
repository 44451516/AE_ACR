#region

using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.utils;
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

        if (lastComboActionID == 全蚀斩TotalEclipse && HasEffect(Buffs.DivineMight) && 圣环HolyCircle.MyIsUnlock())
        {
            if (aoeCount >= PLDSettings.Instance.USE_AOE)
            {
                return 圣环HolyCircle.OriginalHook();
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