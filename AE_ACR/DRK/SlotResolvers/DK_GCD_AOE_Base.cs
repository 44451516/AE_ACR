#region

using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_GCD_AOE_Base : DRKBaseSlotResolvers
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

        

        if (lastComboActionID == 单体2SyphonStrike && 单体3Souleater.MyIsUnlock())
        {
            return -1;
        }

        if (lastComboActionID == 释放Unleash && 刚魂StalwartSoul.MyIsUnlock())
        {
            return 0;
        }

        if (TargetHelper.GetNearbyEnemyCount(5) >= 2)
        {
            if (释放Unleash.MyIsUnlock())
            {
                return 0;
            }

        }

        return -1;
    }

    private Spell GetAOEGCDSpell()
    {
        if (lastComboActionID == 释放Unleash && getQTValue(BaseQTKey.攒资源) == false)
        {
            if (Core.Resolve<JobApi_DarkKnight>().Blood >= 80 && 寂灭Quietus.MyIsUnlock())
            {
                var spell = Core.Resolve<MemApiSpell>().CheckActionChange(寂灭Quietus).GetSpell();
                return spell;
            }
        }
       

        if (lastComboActionID == 释放Unleash && 刚魂StalwartSoul.MyIsUnlock())
        {
            return 刚魂StalwartSoul.GetSpell();
        }

        return 释放Unleash.GetSpell();
    }
    
    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}