#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DK_GCD_Base : DRKBaseSlotResolvers
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
        
        if (GetBaseGCD() == null)
        {
            return Flag_GCD_Base_NULL;
        }
        
        return 0;
    }

    public override void Build(Slot slot)

    {
        var spell = GetBaseGCD();
        if (spell != null)
        {
            slot.Add(spell);
        }
    }

    public static Spell? GetBaseGCD()
    {
        
        bool inAttackDistance = 和目标的距离() <= DKSettings.Instance.近战最大攻击距离;
        
        if (DKSettings.Instance.日常模式)
        {
            // if (Core.Me.TargetObject is IBattleChara battleChara)
            // {
            //     if (伤残.MyIsUnlock())
            //     {
            //         var 伤残阈值 = DKSettings.Instance.伤残阈值;
            //         if (和目标的距离() >= 伤残阈值 && 和目标的距离() <= 20f)
            //         {
            //             return 伤残.GetSpell();
            //         }
            //     }
            // }
        }

        if (lastComboActionID == 单体1HardSlash && 单体2SyphonStrike.MyIsUnlock() && inAttackDistance)
        {
            return 单体2SyphonStrike.GetSpell();
        }
        

        if (lastComboActionID == 单体2SyphonStrike && 单体3Souleater.MyIsUnlock() && inAttackDistance)
        {
            return 单体3Souleater.GetSpell();
        }

        if (inAttackDistance)
        {
            return 单体1HardSlash.GetSpell();
        }

        return null;
    }
}