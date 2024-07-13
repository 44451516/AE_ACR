using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

namespace AE_ACR.PLD.SlotResolvers
{
    public class Ability_深奥之灵 : PLDBaseSlotResolvers
    {
        public override int Check()
        {
            if (CanWeave())
            {
                if (深奥之灵SpiritsWithin.OriginalHookActionReady())
                {
                    if (HasEffect(Buffs.FightOrFlight))
                    {
                        if (深奥之灵SpiritsWithin.IsUnlock())
                        {
                            if (GetCooldownRemainingTime(安魂祈祷Requiescat) > 40)
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }

                    if (GetCooldownRemainingTime(战逃反应FightOrFlight) > 20 &&
                        GetCooldownRemainingTime(战逃反应FightOrFlight) < 40)
                    {
                        return 0;
                    }
                }
            }

            return -1;
        }

        public override void Build(Slot slot)
        {
            slot.Add(深奥之灵SpiritsWithin.OriginalHook());
        }
    }
}