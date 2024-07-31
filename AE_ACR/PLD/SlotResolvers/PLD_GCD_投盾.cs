#region

using AE_ACR_DRK_Setting;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.PLD.Setting;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLD_GCD_投盾 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }


        if (getQTValue(PLDQTKeyc.远程投盾) == false)
        {
            return Flag_远程技能QT;
        }


        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (投盾ShieldLob.IsUnlock())
            {
                var 阈值 = PLDSettings.Instance.投盾阈值;
                if (TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me) >= 阈值
                    && TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me) <= 20f)
                {
                    
                    if (HasEffect(Buffs.DivineMight) == false && IsMoving() == true)
                    {
                        return 0;
                    }

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
        return 投盾ShieldLob.GetSpell();
    }
}