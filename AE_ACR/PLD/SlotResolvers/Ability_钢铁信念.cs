﻿using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.MemoryApi;

namespace AE_ACR.PLD.SlotResolvers;

public class Ability_钢铁信念 : PLDBaseSlotResolvers
{
    public static uint LastBaseGcd => Core.Resolve<MemApiSpell>().GetLastComboSpellId();
    public static uint LastSpell => Core.Resolve<MemApiSpellCastSuccess>().LastSpell;

    public override int Check()
    {
        if (是否停手()) return -1;

        if (钢铁信念.ActionReady()) return 0;

        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(钢铁信念.OriginalHook());
    }
}