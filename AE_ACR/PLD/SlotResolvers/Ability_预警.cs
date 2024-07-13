using AE_ACR.ALL;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;
using AEAssist.MemoryApi;

namespace AE_ACR.PLD.SlotResolvers
{
    public class Ability_预警 : PLDBaseSlotResolvers
    {
        public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
        public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;

        public override int Check()
        {
            if (BaseIslotResolver.CanWeave())
            {
                //判断多少人打自己？ 再判断铁壁的id
                if (ALLData.Buffs.亲疏自行.GetBuffRemainingTime() > 500)
                {
                    return -1;
                }
                
                if (ALLData.Buffs.铁壁.GetBuffRemainingTime() > 500)
                {
                    return -1;
                }

                if (Data.Data.预警.ActionReady() && TargetHelper.GetNearbyEnemyCount(5) >= 6)
                {
                    return 0;
                }
            }

            return -1;
        }


        public override void Build(Slot slot)
        {
            slot.Add(Data.Data.预警.OriginalHook());
        }
    }
}