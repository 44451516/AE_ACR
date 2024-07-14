using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

namespace AE_ACR.GLA.SlotResolvers
{
    public class Ability_战逃反应 : GLABaseSlotResolvers
    {
        public override int Check()
        {
            if (CanWeave())
            {
                if (战逃反应FightOrFlight.ActionReady() && Core.Me.CurrentMp >= 3000)
                {
                    return 0;
                }
            }

            return -1;
        }


        public override void Build(Slot slot)
        {
            slot.Add(战逃反应FightOrFlight.GetSpell());
        }
    }
}