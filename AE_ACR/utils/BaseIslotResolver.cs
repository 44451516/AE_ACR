#region

using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.utils;

public abstract class BaseIslotResolver : ISlotResolver
{
    public static uint lastComboActionID => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    public static double comboTime => Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalSeconds;


    // public static bool getsads()
    // {
    //     return BaseQTKey.QT.GetQt("百花");
    // }

    public abstract int Check();
    public abstract void Build(Slot slot);

    public static bool IsMoving()
    {
        return MoveHelper.IsMoving();
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
        if (Core.Me.TargetObject == null) return -1;

        if (Core.Me.TargetObject is IBattleChara targetObject) return Core.Me.Distance(targetObject);

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
                foreach (var statuse in battleChara.StatusList)
                    if (statuse.StatusId == StatusId)
                        count++;
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
                if (battleChara.TargetObjectId == Core.Me.GameObjectId)
                    count++;
        }

        return count;
    }
}