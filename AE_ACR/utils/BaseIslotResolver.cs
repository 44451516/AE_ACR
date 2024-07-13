using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.utils;

public abstract class BaseIslotResolver : ISlotResolver
{
    public static uint lastComboActionID => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    public static double comboTime => Core.Resolve<MemApiSpell>().GetComboTimeLeft().TotalSeconds;

    public static bool IsMoving()
    {
        return MoveHelper.IsMoving();
    }


    public static bool CanWeave(int weaveTime = 660)
    {
        if (GCDHelper.GetGCDCooldown() < weaveTime)
        {
            return true;
        }

        return false;
    }

    public static bool HasEffect(ushort value)
    {
        return value.HasEffect();
    }

    public static int GetBuffRemainingTime(ushort value)
    {
        return value.GetBuffRemainingTime();
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

    public abstract int Check();
    public abstract void Build(Slot slot);
}