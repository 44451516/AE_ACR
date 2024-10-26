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

public class GCD_倾泻资源 : PLDBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!getQTValue(PLDQTKey.倾泻资源))
        {
            return Flag_QT;
        }

        if (isHasCanAttackBattleChara() == false)
        {
            return Flag_无效目标;
        }


        if (GetBaseGCDSpell() == null)
        {
            return Flag_GCD_Base_NULL;
        }

        if (赎罪剑Atonement.MyIsUnlock())
        {
            if (HasEffect(Buffs.赎罪剑Atonement1BUFF) || HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF))
            {
                if (PLDSettings.Instance.近战最大攻击距离 >= 和目标的距离())
                {
                    return 0;
                }
            }

            if (HasEffect(Buffs.DivineMight))
            {
                if (和目标的距离() < 25)
                {
                    return 0;
                }
            }
        }


        return -1;
    }

    private Spell? GetBaseGCDSpell()
    {
        bool inAttackDistance = 和目标的距离() <= PLDSettings.Instance.近战最大攻击距离;
        bool inAttackDistance25 = 和目标的距离() <= 25;

        if (HasEffect(Buffs.赎罪剑Atonement3BUFF) && inAttackDistance)
        {
            return 赎罪剑Atonement.OriginalHook();
        }

        if (HasEffect(Buffs.赎罪剑Atonement2BUFF) && inAttackDistance)
        {
            return 赎罪剑Atonement.OriginalHook();
        }

        if (HasEffect(Buffs.DivineMight) && inAttackDistance25)
        {
            return 圣灵HolySpirit.OriginalHook();
        }

        if (HasEffect(Buffs.赎罪剑Atonement1BUFF) && inAttackDistance)
        {
            return 赎罪剑Atonement.OriginalHook();
        }


        return null;

    }


    public override void Build(Slot slot)
    {
        var baseGcdSpell = GetBaseGCDSpell();
        slot.Add(baseGcdSpell);
    }
}