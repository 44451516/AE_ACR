using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.GLA.SlotResolvers
{
    public class Ability_先锋剑 : ISlotResolver
    {
        public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;

        public int Check()
        {
            if (UIntExtensions.CanWeave())
            {
                if (Data.Data.厄运流转CircleOfScorn.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) > 0)
                {
                    return 0;
                }
            }

            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(Data.Data.厄运流转CircleOfScorn.OriginalHook());
        }
    }
}