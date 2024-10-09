#region

using AE_ACR_DRK_Setting;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR_DRK.SlotResolvers;

public class DK_GCD_伤残 : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (getQTValue(DRKQTKey.伤残) == false)
        {
            return Flag_远程技能QT;
        }


        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (伤残.MyIsUnlock())
            {
                var 伤残阈值 = DKSettings.Instance.伤残阈值;
                if (和目标的距离() >= 伤残阈值 && 和目标的距离() <= 20f)
                {
                    return 0;
                }

            }
        }

        return -1;
    }

    public override void Build(Slot slot)
    {
        var spell = GetGCDSpell();
        slot.Add(spell);
    }

    private Spell GetGCDSpell()
    {
        return 伤残.GetSpell();
    }
}