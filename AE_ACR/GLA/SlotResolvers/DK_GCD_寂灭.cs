using AE_ACR.GLA.Data;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace AE_ACR.GLA.SlotResolvers;

public class DK_GCD_寂灭 : ISlotResolver
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();

    // 返回>=0表示检测通过 即将调用Build方法
    public int Check()
    {
        if (Data.Data.寂灭Quietus.IsUnlock())
        {
            return -1;
        }

        
        var Blood = Core.Resolve<JobApi_DarkKnight>().Blood;

        var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;
        if (darksideTimeRemaining == 0)
        {
            return -1;
        }



        if (Blood > 50 || GameObjectExtension.HasAura(Core.Me, Data.Data.Buffs.血乱Delirium, 0))
        {
            if (TargetHelper.GetNearbyEnemyCount(5) > 2)
            // if (TargetHelper.CheckNeedUseAOE(5, 5, 3))
            {
                return 0;
            }
            
        }


        return -1;
    }

    public static Spell GetBaseGCD()
    {
        return SpellHelper.GetSpell(Data.Data.寂灭Quietus);
    }


    // 将指定技能加入技能队列中
    public void Build(Slot slot)

    {
        Spell spell = GetBaseGCD();
        slot.Add(spell);
    }
}