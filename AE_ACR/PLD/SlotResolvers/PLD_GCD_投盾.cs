#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
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


        if (getQTValue(PLDQTKey.远程投盾) == false)
        {
            return Flag_远程技能QT;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (投盾ShieldLob.MyIsUnlock())
            {
                var 阈值 = PLDSettings.Instance.投盾阈值;
                if (和目标的距离() >= 阈值 && 和目标的距离() <= 20f)
                {

                    if (HasEffect(Buffs.DivineMight) == false && IsMoving())
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