using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace AE_ACR.ALL.SlotResolvers
{
    public class Ability_亲疏自行 : BaseIslotResolver
    {
        public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;

        public override int Check()
        {
            if (CanWeave())
            {
                if (ALLData.Buffs.铁壁.GetBuffRemainingTime() > 0.5f)
                {
                    return -1;
                }
                
                //判断多少人打自己？
                if (ALLData.亲疏自行.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) >= 6)
                {
                    return 0;
                }
            }

            return -1;
        }


        public override void Build(Slot slot)
        {
            slot.Add(ALLData.亲疏自行.OriginalHook());
        }
    }
}