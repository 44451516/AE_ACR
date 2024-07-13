using AE_ACR_DRK;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.DRK.SlotResolvers
{
    public class DK_Ability_吸血深渊 : ISlotResolver
    {
        public int Check()
        {
            if (GCDHelper.GetGCDCooldown() < 600)
            {
                return -1;
            }

            var darksideTimeRemaining = Core.Resolve<JobApi_DarkKnight>().DarksideTimeRemaining;

            if (darksideTimeRemaining == 0)
            {
                return -2;
            }


            IBattleChara? battleChara = Core.Me.GetCurrTarget();


            if (TargetHelper.GetNearbyEnemyCount(battleChara, 5, 5) < 2)
            {
                return -4;
            }


            if (
                Core.Resolve<MemApiSpell>().CheckActionChange(DKData.AbyssalDrain).IsReady() == true 
                && TargetHelper.GetNearbyEnemyCount(battleChara, 5, 5) >= 3
                )
            {
                return 0;
            }

            return -5;
        }


        public void Build(Slot slot)
        {
            Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.AbyssalDrain).GetSpell();

            slot.Add(spell);
        }
    }
}