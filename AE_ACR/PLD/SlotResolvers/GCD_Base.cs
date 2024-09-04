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

public class GCD_Base : PLDBaseSlotResolvers
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

        

        return 0;
    }

    private Spell GetAOEGCDSpell()
    {
        var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
        if (aoeCount >= 2)
        {
            if (aoeCount >= 3)
            {
                if (HasEffect(Buffs.DivineMight) && 圣环HolyCircle.IsUnlock())
                {
                    return 圣环HolyCircle.OriginalHook();
                }
            }


            if (lastComboActionID == 暴乱剑RiotBlade && 战女神之怒RageOfHalone.IsUnlock())
            {
                return 战女神之怒RageOfHalone.OriginalHook();
            }
            if (lastComboActionID == 全蚀斩TotalEclipse && HasEffect(Buffs.DivineMight) && 圣环HolyCircle.IsUnlock())
            {
                return 圣环HolyCircle.OriginalHook();
            }

            if (lastComboActionID == 全蚀斩TotalEclipse && 日珥斩Prominence.IsUnlock())
            {
                return 日珥斩Prominence.OriginalHook();
            }

            if (全蚀斩TotalEclipse.IsUnlock())
            {
                return 全蚀斩TotalEclipse.OriginalHook();
            }


        }

        if (HasEffect(Buffs.FightOrFlight) && 王权剑RoyalAuthority.IsUnlock())
        {
            if (GetBuffRemainingTime(Buffs.DivineMight) >= 27
                && GetBuffRemainingTime(Buffs.FightOrFlight) >= 0.1f
                && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
            {
                // AELoggerUtils.Log("圣灵1");
                return 圣灵HolySpirit.OriginalHook();
            }


            if (lastComboActionID == 王权剑RoyalAuthority && (HasEffect(Buffs.赎罪剑Atonement1BUFF) || HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF)))
            {
                return 赎罪剑Atonement.OriginalHook();
            }

            if (HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF))
            {
                return 赎罪剑Atonement.OriginalHook();
            }

            if (HasEffect(Buffs.DivineMight) && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
            {
                return 圣灵HolySpirit.OriginalHook();
            }

            if (lastComboActionID is 暴乱剑RiotBlade)
            {
                return 战女神之怒RageOfHalone.OriginalHook();
            }

            if (HasEffect(Buffs.Requiescat) && 圣灵HolySpirit.IsUnlock() && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
            {
                return 圣灵HolySpirit.OriginalHook();
            }
        }

        if (GetCooldownRemainingTime(战逃反应FightOrFlight) >= 15 && RaidBuff.爆发期_120())
        {
            if (HasEffect(Buffs.赎罪剑Atonement3BUFF)) return 赎罪剑Atonement.OriginalHook();

            if (HasEffect(Buffs.DivineMight) && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp)
            {
                return 圣灵HolySpirit.OriginalHook();
            }


            if (HasEffect(Buffs.赎罪剑Atonement2BUFF))
            {
                return 赎罪剑Atonement.OriginalHook();
            }


            if (HasEffect(Buffs.赎罪剑Atonement1BUFF))
            {
                return 赎罪剑Atonement.OriginalHook();
            }
        }

        if (PLDSettings.Instance.日常模式)
        {
            if (Core.Me.TargetObject is IBattleChara battleChara)
            {
                if (投盾ShieldLob.IsUnlock())
                {
                    if (TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me) is > 5 and <= 15)
                    {
                        return 投盾ShieldLob.GetSpell();
                    }
                }
            }
        }


        if (lastComboActionID == 先锋剑FastBlade && 暴乱剑RiotBlade.IsUnlock())
        {
            return 暴乱剑RiotBlade.OriginalHook();
        }

        if (HasEffect(Buffs.赎罪剑Atonement1BUFF) || HasEffect(Buffs.赎罪剑Atonement2BUFF))
        {
            return 赎罪剑Atonement.OriginalHook();
        }


        if (HasEffect(Buffs.DivineMight))
        {
            return 圣灵HolySpirit.OriginalHook();
        }

        if (HasEffect(Buffs.赎罪剑Atonement1BUFF) || HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF))
        {
            return 赎罪剑Atonement.OriginalHook();
        }


        if (lastComboActionID == 暴乱剑RiotBlade && 战女神之怒RageOfHalone.IsUnlock())
        {
            return 战女神之怒RageOfHalone.OriginalHook();
        }


        return 先锋剑FastBlade.OriginalHook();
    }

    public override void Build(Slot slot)
    {
        var spell = GetAOEGCDSpell();
        slot.Add(spell);
    }
}