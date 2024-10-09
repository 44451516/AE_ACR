#region

using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public class GCD_Base : GLABaseSlotResolvers
{
    public override int Check()
    {
        return 0;
    }

    private Spell GetAOEGCDSpell()
    {
        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        if (aoeCount >= 2 && 全蚀斩TotalEclipse.MyIsUnlock())
        {
            return 全蚀斩TotalEclipse.OriginalHook();
        }

        if (投盾ShieldLob.MyIsUnlock() && 和目标的距离() > 3.5)
        {
            return 投盾ShieldLob.GetSpell();
        }

        if (lastComboActionID == 先锋剑FastBlade && 暴乱剑RiotBlade.MyIsUnlock())
        {
            return 暴乱剑RiotBlade.GetSpell();
        }

        if (lastComboActionID == 暴乱剑RiotBlade && 战女神之怒RageOfHalone.MyIsUnlock())
        {
            return 战女神之怒RageOfHalone.GetSpell();
        }


        return 先锋剑FastBlade.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}