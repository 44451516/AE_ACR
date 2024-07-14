#region

using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR_DRK.SlotResolvers;

public class DK_GCD_寂灭 : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    // 返回>=0表示检测通过 即将调用Build方法
    public int Check()
    {
        if (DKData.寂灭Quietus.IsUnlock()) return -1;


        var Blood = Core.Resolve<JobApi_DarkKnight>().Blood;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;
        if (darksideTimeRemaining == 0) return -1;


        if (Blood > 50 || Core.Me.HasAura(DKData.Buffs.血乱Delirium))
            if (TargetHelper.GetNearbyEnemyCount(5) > 2)
                // if (TargetHelper.CheckNeedUseAOE(5, 5, 3))
                return 0;


        return -1;
    }


    // 将指定技能加入技能队列中
    public void Build(Slot slot)

    {
        var spell = GetBaseGCD();
        slot.Add(spell);
    }

    public static Spell GetBaseGCD()
    {
        return DKData.寂灭Quietus.GetSpell();
    }
}