﻿using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;
using AE_ACR.DRK.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;

namespace AE_ACR.DRK.起手;

public class DRK_Opener100 : IOpener
{
    public int StartCheck()
    {
        if (!DRKRotationEntry.QT.GetQt(DRKQTKey.起手序列))
        {
            return -100;
        }

        // if (PartyHelper.CastableParty.Count < 8)
        //     return -99;

        if (DKSettings.Instance.只在高难模式使用起手序列)
        {
            if (Data.IsInHighEndDuty == false)
            {
                return -10;
            }
        }

        if (!TargetHelper.IsBoss(Core.Me.GetCurrTarget()))
            return -1;

        if (AI.Instance.BattleData.CurrBattleTimeInMs > 2000)
        {
            return -5;
        }

        return 0;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step0,
        爆发药,
        Step2,
        Step3
    };



    public void InitCountDown(CountDownHandler countDownHandler)
    {
        if (DKSettings.Instance.起手突进)
        {
            countDownHandler.AddAction(500, DRKBaseSlotResolvers.暗影步, SpellTargetType.Target);
        } 
    
        if (DKSettings.Instance.起手关盾 && DRKBaseSlotResolvers.关盾.IsUnlock())
        {
            if (BaseIslotResolver.HasEffect(DRKBaseSlotResolvers.Buffs.深恶痛绝))
            {
                countDownHandler.AddAction(DKSettings.Instance.起手关盾阈值, DRKBaseSlotResolvers.关盾, SpellTargetType.Self);
            }
        }
    }

    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(DRKBaseSlotResolvers.重斩, SpellTargetType.Target));
    }



    private static void 爆发药(Slot slot)
    {
        if (DRKRotationEntry.QT.GetQt(BaseQTKey.爆发药))
        {
            slot.Add(Spell.CreatePotion());
        }
        slot.AddDelaySpell(700, new Spell(DRKBaseSlotResolvers.暗影峰, SpellTargetType.Target));
    }

    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(DRKBaseSlotResolvers.吸收斩, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.暗影峰, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.掠影示现, SpellTargetType.Target));

        slot.Add(new Spell(DRKBaseSlotResolvers.噬魂斩, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.血乱Delirium, SpellTargetType.Self));
        slot.Add(new Spell(DRKBaseSlotResolvers.精雕怒斩CarveAndSpit, SpellTargetType.Target));

        slot.Add(new Spell(DRKBaseSlotResolvers.血溅1, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.Shadowbringer暗影使者, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.腐秽大地SaltedEarth, SpellTargetType.Self));

    }

    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(DRKBaseSlotResolvers.血溅2, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.暗影峰, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.腐秽黑暗, SpellTargetType.Target));

        slot.Add(new Spell(DRKBaseSlotResolvers.血溅3, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.Shadowbringer暗影使者, SpellTargetType.Target));
        slot.Add(new Spell(DRKBaseSlotResolvers.暗影峰, SpellTargetType.Target));

        // slot.Add(new Spell(DRKBaseSlotResolvers.血溅Bloodspiller, SpellTargetType.Target));
        // slot.Add(new Spell(DRKBaseSlotResolvers.蔑视厌恶Disesteem, SpellTargetType.Target));
    }
}