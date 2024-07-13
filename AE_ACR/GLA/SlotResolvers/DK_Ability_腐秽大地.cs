using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;

namespace AE_ACR.GLA.SlotResolvers
{
	public class DK_Ability_腐秽大地 : ISlotResolver
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
			
			if (DKSettings.Instance.能力技爆发延时 > CombatTime.Instance.CombatEngageDuration().TotalSeconds)
			{
				return -1;
			}
			
			
			if (Core.Resolve<MemApiSpell>().CheckActionChange(DKData.腐秽大地SaltedEarth).IsReady() == true)
			{
				return 0;
			}

			
			return -3;
		}


		public void Build(Slot slot)
		{
			Spell spell = Core.Resolve<MemApiSpell>().CheckActionChange(DKData.腐秽大地SaltedEarth).GetSpell();

			slot.Add(spell);
			
		}
	}
}
