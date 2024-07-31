#region

using AE_ACR.utils;
using AEAssist;
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
        留空 = 3624;

    public static class DeBuffs
    {
        public const ushort
            加速器炸弹 = 3802,
            留空 = 0;
    }

    public static uint lastComboActionID => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
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
        if (GCDHelper.GetGCDCooldown() > weaveTime) return true;

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
}