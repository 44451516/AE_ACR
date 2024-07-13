using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace AE_ACR.utils
{
    internal static class UIntExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="weaveTime"> 毫秒</param>
        /// <returns></returns>
        public static bool CanWeave(int weaveTime = 660)
        {
            if (GCDHelper.GetGCDCooldown() < weaveTime)
            {
                return true;
            }

            return false;
        }
    }
}