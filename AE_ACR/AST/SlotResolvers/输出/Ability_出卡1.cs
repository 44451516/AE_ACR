﻿#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_出卡1 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (Play1.OriginalHook().Id == Play1)
        {
            return -1;
        }

        if (CanWeave())
        {
            if (Play1.OriginalHook().Id is 近战卡 or 远程卡)
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        IBattleChara? RealbattleChara = null;
        if (Play1.OriginalHook().Id == 近战卡)
        {
            var battleChara = PartyHelper.CastableAlliesWithin30 //周围30米
                .Where(r => r.CurrentHp > 0 && r.IsMelee()).FirstOrDefault();

            if (battleChara != null && battleChara.IsValid())
            {
                RealbattleChara = battleChara;
            }

            if (battleChara == null)
            {
                battleChara = PartyHelper.CastableAlliesWithin30 //周围30米
                    .Where(r => r.CurrentHp > 0 && r.IsTank()).FirstOrDefault();

                if (battleChara != null && battleChara.IsValid())
                {
                    RealbattleChara = battleChara;
                }
            }

        }
        else if (Play1.OriginalHook().Id == 远程卡)
        {
            var battleChara = PartyHelper.CastableAlliesWithin30 //周围30米
                .Where(r => r.CurrentHp > 0 && r.IsRanged()).FirstOrDefault();

            if (battleChara != null && battleChara.IsValid())
            {
                RealbattleChara = battleChara;
            }
        }
        else
        {
            RealbattleChara = Core.Me;
        }

        if (RealbattleChara == null)
        {
            RealbattleChara = Core.Me;
        }


        slot.Add(new Spell(Play1.OriginalHook().Id, RealbattleChara));
    }
}