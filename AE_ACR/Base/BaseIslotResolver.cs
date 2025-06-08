#region

using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.Base;

public abstract class BaseIslotResolver : ISlotResolver
{
    //Flag
    public const int
        Flag_停手 = -100,
        Flag_减伤 = -101,
        Flag_攒资源 = -102,
        Flag_QT = -103,
        Flag_爆发药 = -104,
        Flag_远程技能QT = -106,
        Flag_无效目标 = -107,
        Flag_CD = -108,
        Flag_小队人数不够 = -109,
        Flag_残血不打爆发 = -110,
        Flag_不在副本里面 = -111,
        Flag_不是日常本 = -112,
        Flag_没有解锁 = -113,
        Flag_超出攻击距离 = -114,
        Flag_GCD_Base_NULL = -115,
        Flag_伤害太低了 = -116,
        Flag_反转关闭 = -117,
        Flag_没有设置爆发药 = -118,
        Flag_没有爆发药数量为0 = -119,
        留空 = 3624;

    public static uint lastComboActionID => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    public static uint lastGCDActionID => Core.Resolve<MemApiSpellCastSuccess>().LastGcd;
    public static double comboTime => Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalSeconds;
    
    

    public static uint LastAction => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;
    public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;


    public abstract int Check();
    public abstract void Build(Slot slot);

    public static bool IsMoving()
    {
        return MoveHelper.IsMoving();
    }


    public static double 爆发药冷却时间()
    {
        return GetCooldownRemainingTime(846);
    }


    public static bool CanWeave(int weaveTime = 660)
    {
        if (GCDHelper.GetGCDCooldown() > weaveTime) 
            return true;

        return false;
    }

    public static bool HasEffect(ushort value)
    {
        return value.HasEffect();
    }

    public static double GetBuffRemainingTime(ushort value)
    {
        return value.GetBuffRemainingTime() / 1000d;
    }

    public static double GetCooldownRemainingTime(uint value)
    {
        return value.GetCooldownRemainingTime();
    }

    public static bool WasLastAction(uint value)
    {
        return value.WasLastAction();
    }

    public static uint GetResourceCost(uint actionID)
    {
        return Core.Resolve<MemApiSpell>().MPNeed(actionID);
    }

    public static float getTargetObjectDistance()
    {
        if (Core.Me.TargetObject == null)
        {
            return -1;
        }

        if (Core.Me.TargetObject is IBattleChara targetObject)
        {
            return Core.Me.Distance(targetObject);
        }
        return -1;
    }

    public static int EnemysIn12DebuffByStatusId(uint StatusId)
    {
        var count = 0;
        var instanceEnemysIn12 = TargetMgr.Instance.EnemysIn12;
        foreach (var keyValuePair in instanceEnemysIn12)
        {
            var battleChara = keyValuePair.Value;
            if (battleChara.CanAttack())
            {
                foreach (var statuse in battleChara.StatusList)
                {
                    if (statuse.StatusId == StatusId)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public static int attackMeCount()
    {
        var count = 0;
        foreach (var keyValuePair in TargetMgr.Instance.EnemysIn25)
        {
            var battleChara = keyValuePair.Value;
            if (battleChara.CanAttack())
            {
                if (battleChara.TargetObjectId == Core.Me.GameObjectId)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static bool isHasCanAttackBattleChara()
    {
        if (Core.Me.TargetObject == null)
        {
            return false;
        }

        if (Core.Me.TargetObject is IBattleChara targetObject)
        {
            if (targetObject.CanAttack())
            {
                return true;
            }
        }
        return false;
    }


    public static float 和目标的距离()
    {
        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            return TargetHelper.GetTargetDistanceFromMeTest2D(battleChara, Core.Me);
        }
        //-1000无目标
        return -1000;
    }

    public static bool 战斗爽()
    {
        var currTarget = Core.Me.GetCurrTarget();
        if (currTarget == null)
        {
            return false;
        }
        
        //不在副本里面直接返回了
        if (Core.Resolve<MemApiDuty>().IsBoundByDuty() == false)
        {
            return true;
        }
        
        
        if (Core.Resolve<MemApiDuty>().InBossBattle == true)
        {
            var dutySchedule = Core.Resolve<MemApiDuty>().GetSchedule();
            if (dutySchedule?.CountPoint == dutySchedule?.NowPoint)
            {
                return true;
            }
        }

        
        bool 使用爆发 = true;

        GeneralSettings generalSettings = SettingMgr.GetSetting<GeneralSettings>();
        if (currTarget.IsBoss())
        {
            if (isLastBoss(currTarget))
            {
                return true;
            }

            if (isUseAeCheck)
            {
                if (generalSettings.TimeToKillCheckTime >= 12000)
                {
                    //如果当前目标在12秒内会被击杀（TTK），并且当前目标是Boss，返回不爽
                    if (TTKHelper.IsTargetTTK(currTarget, 12000, true))
                    {
                        return false;
                    }
                }
            }
            else
            {
                // 当前目标的生命值百分比小于0.05f（即5%）
                if (currTarget.CurrentHp <= 0.05f)
                {
                    return false;
                }
            }

        }
        else
        {
            foreach (var keyValuePair in TargetMgr.Instance.EnemysIn25)
            {
                var battleChara = keyValuePair.Value;
                if (battleChara.CanAttack())
                {
                    if (isUseAeCheck)
                    {
                        if (generalSettings.TimeToKillCheckTime >= 12000)
                        {
                            if (TTKHelper.IsTargetTTK(battleChara, 12000, true))
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (battleChara.CurrentHpPercent() >= 0.5f)
                        {
                            使用爆发 = true;
                        }
                    }

                }
            }
        }
        //默认开爆发
        return 使用爆发;
    }

    /// <summary>
    ///     空方法等待别人实现
    /// </summary>
    /// <param name="currTarget"></param>
    /// <returns></returns>
    private static bool isLastBoss(IBattleChara currTarget)
    {
        if (LastBossId.list.Contains(currTarget.DataId))
        {
            return true;
        }
        return false;
    }

    public static class DeBuffs
    {
        public const ushort
            加速器炸弹 = 3802,
            留空 = 0;
    }
    
    public static class Buffs
    {
        public const ushort
            生还 = 418,
            留空 = 0;
    }

    public static bool 是否全局停手()
    {
        if (HasEffect(Buffs.生还))
        {
            return true;
        }
        
        return false;
    }

    private static bool isUseAeCheck = true;
}