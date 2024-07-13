using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace AE_ACR.ALL.SlotResolvers
{
    public class Ability_血仇 : ISlotResolver
    {
        public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;

        public int Check()
        {
            if (BaseIslotResolver.CanWeave())
            {
                
                if (ALLData.雪仇.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) >= 6)
                {
                    return 0;
                }
            }

            return -1;
        }


        public void Build(Slot slot)
        {
            slot.Add(ALLData.雪仇.OriginalHook());
        }
    }
}