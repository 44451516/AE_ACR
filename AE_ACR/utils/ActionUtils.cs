using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace AE_ACR.utils
{
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
            return value.IsUnlock() && value.IsLevelEnough() && value.ActionReady();
        }

        internal static bool WasLastAction(this uint value)
        {
            return Core.Resolve<MemApiSpellCastSuccess>().LastSpell == value;
        }
        
        
        internal static bool HasEffect(this uint value)
        {
            return GameObjectExtension.HasAura(Core.Me, value);
        }

        internal static int GetBuffRemainingTime(this uint value)
        {
            return Core.Resolve<MemApiBuff>().GetAuraTimeleft(Core.Me, value, true);
        }
        
    }
}