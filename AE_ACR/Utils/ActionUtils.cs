#region

using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

#endregion

namespace AE_ACR.utils;

internal static class ActionUtils
{
    internal static double GetCooldownRemainingTime(this uint value)
    {
        return Core.Resolve<MemApiSpell>().GetCooldown(value).TotalSeconds;
    }

    internal static Spell OriginalHook(this uint value)
    {
        return Core.Resolve<MemApiSpell>().CheckActionChange(value).GetSpell();
    }

    internal static bool ActionReady(this uint value)
    {
        return value.IsUnlock() && value.IsLevelEnough() && value.IsReady();
    } 
    internal static float Charges(this uint value)
    {
        return value.GetSpell().Charges;
    }

    internal static bool OriginalHookActionReady(this uint value)
    {
        var id = value.OriginalHook().Id;
        return id.IsUnlock() && id.IsLevelEnough() && id.IsReady();
    }

    internal static bool WasLastAction(this uint value)
    {
        return Core.Resolve<MemApiSpellCastSuccess>().LastSpell == value;
    }


    internal static bool HasEffect(this ushort value)
    {
        return Core.Me.HasAura(value);
    }

    internal static int GetBuffRemainingTime(this ushort value)
    {
        return Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, value, true);
    }

    /// <summary>
    /// </summary>
    /// <param name="actionID"></param>
    /// <param name="weaveTime"> 毫秒</param>
    /// <returns></returns>
    public static bool CanWeave(int weaveTime = 660)
    {
        if (GCDHelper.GetGCDCooldown() < weaveTime) return true;

        return false;
    }
}