#region

using AE_ACR_DRK_Setting;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.PLD.Setting;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLD_GCD_远程圣灵 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }
        
        
        if (getQTValue(PLDQTKey.远程圣灵) == false)
        {
            return Flag_远程技能QT;
        }
        
        
        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (圣灵HolySpirit.IsUnlock() && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
            {
                var 阈值 = PLDSettings.Instance.远程圣灵阈值;
                if (和目标的距离() >= 阈值 && 和目标的距离() <= 25f)
                {
                    if (HasEffect(Buffs.DivineMight))
                    {
                        return 0;
                    }

                    if (HasEffect(Buffs.DivineMight) == false && IsMoving() == false)
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
        return 圣灵HolySpirit.GetSpell();
    }
}