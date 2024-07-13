using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Target;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.JobApi;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.PLD.SlotResolvers
{
    public class Ability_深奥之灵 : PLDBaseSlotResolvers
    {

        public override int Check()
        {
            if (CanWeave())
            {
                if (厄运流转CircleOfScorn.OriginalHookActionReady() && TargetHelper.GetNearbyEnemyCount(5) > 0)
                {
                    if (HasEffect(Buffs.FightOrFlight))
                    {
                        if (安魂祈祷Requiescat.IsUnlock())
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
            slot.Add(厄运流转CircleOfScorn.OriginalHook());
        }
    }
}