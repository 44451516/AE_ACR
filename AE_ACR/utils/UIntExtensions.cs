using AEAssist;
using AEAssist.MemoryApi;

namespace AE_ACR.utils
{
    internal static class UIntExtensions
    {
        internal static double GetCooldownRemainingTime(this uint value)
        {

            return Core.Resolve<MemApiSpell>().GetCooldown(value).TotalSeconds;

            // var recastTimeElapsed = Core.Resolve<MemApiSpell>().GetRecastTimeElapsed(value);
            // if (recastTimeElapsed == 0)
            // {
            //     return 0;
            // }
            // else
            // {
            //     return Core.Resolve<MemApiSpell>().GetRecastTimeElapsed(value) - recastTimeElapsed;
            // }
        }
    }
}