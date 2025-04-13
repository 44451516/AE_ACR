#region

using AE_ACR.PLD.Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.PLD.SlotResolvers;

public class PLD_GCD_Base : PLDBaseSlotResolvers
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

        if (GetBaseGCDSpell() == null)
        {
            return Flag_GCD_Base_NULL;
        }


        return 0;
    }

    private Spell? GetBaseGCDSpell()
    {
        bool inAttackDistance = 和目标的距离() <= PLDSettings.Instance.近战最大攻击距离;
        bool inAttackDistance25 = 和目标的距离() <= 25;

        if (HasEffect(Buffs.FightOrFlight) && 王权剑RoyalAuthority.MyIsUnlock())
        {
            if (GetBuffRemainingTime(Buffs.DivineMight) >= 27
                && GetBuffRemainingTime(Buffs.FightOrFlight) >= 0.1f
                && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp
                && inAttackDistance25)
            {
                return 圣灵HolySpirit.OriginalHook();
            }
            
            
            if (GetBuffRemainingTime(Buffs.DivineMight) >0 && GetBuffRemainingTime(Buffs.DivineMight) <=3 
                && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp
                && inAttackDistance25)
            {
                return 圣灵HolySpirit.OriginalHook();
            }


            if (lastComboActionID == 王权剑RoyalAuthority && (HasEffect(Buffs.赎罪剑Atonement1BUFF) || HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF)) && inAttackDistance)
            {
                return 赎罪剑Atonement.OriginalHook();
            }

            if ((HasEffect(Buffs.赎罪剑Atonement2BUFF) || HasEffect(Buffs.赎罪剑Atonement3BUFF)) && inAttackDistance)
            {
                return 赎罪剑Atonement.OriginalHook();
            }

            if (HasEffect(Buffs.DivineMight) && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp && inAttackDistance25)
            {
                return 圣灵HolySpirit.OriginalHook();
            }

            if (lastComboActionID is 暴乱剑RiotBlade && inAttackDistance)
            {
                return 战女神之怒RageOfHalone.OriginalHook();
            }

            if (大保健连击Confiteor.IsUnlock() == false && HasEffect(Buffs.Requiescat) && 圣灵HolySpirit.MyIsUnlock() && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp && inAttackDistance25)
            {
                return 圣灵HolySpirit.OriginalHook();
            }
        }

        if (GetCooldownRemainingTime(战逃反应FightOrFlight) >= 15 && RaidBuff.爆发期_120())
        {
            if (HasEffect(Buffs.赎罪剑Atonement3BUFF) && inAttackDistance)
                return 赎罪剑Atonement.OriginalHook();

            if (HasEffect(Buffs.DivineMight) && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp && inAttackDistance25)
            {
                return 圣灵HolySpirit.OriginalHook();
            }


            if (HasEffect(Buffs.赎罪剑Atonement2BUFF) && inAttackDistance)
            {
                return 赎罪剑Atonement.OriginalHook();
            }


            if (HasEffect(Buffs.赎罪剑Atonement1BUFF) && inAttackDistance)
            {
                return 赎罪剑Atonement.OriginalHook();
            }
        }

        if (PLDSettings.Instance.日常模式)
        {
            if (Core.Me.TargetObject is IBattleChara battleChara)
            {
                if (投盾ShieldLob.MyIsUnlock())
                {
                    if (和目标的距离() > PLDSettings.Instance.近战最大攻击距离 && 和目标的距离() <= 15)
                    {
                        return 投盾ShieldLob.GetSpell();
                    }
                }
            }
        }

        if (HasEffect(Buffs.赎罪剑Atonement1BUFF) && inAttackDistance)
        {
            return 赎罪剑Atonement.OriginalHook();
        }

        if (lastComboActionID == 先锋剑FastBlade && 暴乱剑RiotBlade.MyIsUnlock() && inAttackDistance)
        {
            return 暴乱剑RiotBlade.OriginalHook();
        }

        if (lastComboActionID is 暴乱剑RiotBlade && 战女神之怒RageOfHalone.MyIsUnlock() && inAttackDistance)
        {
            if (HasEffect(Buffs.赎罪剑Atonement2BUFF) && inAttackDistance)
            {
                return 赎罪剑Atonement.OriginalHook();
            }

            if (HasEffect(Buffs.DivineMight) && GetResourceCost(圣灵HolySpirit) <= Core.Me.CurrentMp && inAttackDistance25)
            {
                return 圣灵HolySpirit.OriginalHook();
            }

            if (HasEffect(Buffs.赎罪剑Atonement3BUFF) && inAttackDistance)
            {
                return 赎罪剑Atonement.OriginalHook();
            }

            return 战女神之怒RageOfHalone.OriginalHook();
        }

        if (inAttackDistance)
        {
            return 先锋剑FastBlade.OriginalHook();
        }
        return null;

    }

    public override void Build(Slot slot)
    {
        var spell = GetBaseGCDSpell();
        if (spell != null)
        {
            if (spell.Id == 圣灵HolySpirit)
            {
                if (PLDSettings.Instance.M6S设置 && CombatTime.Instance.CombatEngageDuration().TotalSeconds > 281)
                {
                    IBattleChara? 鱼 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 18346 && x.IsValid() && x is { IsDead: false, IsTargetable: true } && x.CurrentHpPercent() <= 0.95f);
                    if (鱼 != null)
                    {
                        spell = new Spell(圣灵HolySpirit, 鱼);
                    }

                    IBattleChara? 猫 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 18347 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
                    if (猫 != null)
                    {
                        spell = new Spell(圣灵HolySpirit, 猫);
                    }

                    IBattleChara? 人马 = TargetMgr.Instance.EnemysIn25.Values.FirstOrDefault(x => x.DataId == 18345 && x.IsValid() && x is { IsDead: false, IsTargetable: true });
                    if (人马 != null)
                    {
                        spell = new Spell(圣灵HolySpirit, 人马);
                    } 
                }
              
            }
            slot.Add(spell);
        }
    }
}