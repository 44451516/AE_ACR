#region

using AE_ACR.PLD;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Helper;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public class DKUsePotion : DRKBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (!getQTValue(PlDQTKey.爆发药))
        {
            return Flag_爆发药;
        }


        if (CanWeave())
        {
            if (爆发药冷却时间() == 0)
            {
                if (LivingShadow.ActionReady())
                {
                    return 0;
                }

                if (LivingShadow.IsUnlock() && GetCooldownRemainingTime(LivingShadow) < 5F)
                {
                    return 0;
                }
            }
        }


        return -1;
    }

    public override void Build(Slot slot)
    {
        slot.Add(Spell.CreatePotion());
    }
}