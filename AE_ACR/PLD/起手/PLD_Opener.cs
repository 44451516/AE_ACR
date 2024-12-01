using AE_ACR.Base;
using AE_ACR.PLD.Setting;
using AE_ACR.PLD.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.Extension;
using AEAssist.Helper;

namespace AE_ACR.PLD.起手;

public class PLD_Opener : IOpener
{
    public int StartCheck()
    {
        if (!PLDRotationEntry.QT.GetQt(PLDQTKey.起手序列))
        {
            return -100;
        }

        if (PartyHelper.CastableParty.Count < 8)
            return -99;

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
        Step0, //先锋剑
        Step1 //爆发药
        // Step2//花

    };


    public void InitCountDown(CountDownHandler countDownHandler)
    {
        if (PLDSettings.Instance.起手幕帘 && PLDBaseSlotResolvers.圣光幕帘.IsUnlock())
        {
            countDownHandler.AddAction(PLDSettings.Instance.起手幕帘阈值, PLDBaseSlotResolvers.圣光幕帘, SpellTargetType.Self);
        }
        countDownHandler.AddAction(PLDSettings.Instance.起手圣灵阈值, PLDBaseSlotResolvers.圣灵HolySpirit, SpellTargetType.Target);
        if (PLDSettings.Instance.起手突进 && PLDBaseSlotResolvers.调停Intervene.IsUnlock())
        {
            countDownHandler.AddAction(500, PLDBaseSlotResolvers.调停Intervene, SpellTargetType.Target);
        }

    }

    private static void Step0(Slot slot)
    {
        if (PLDSettings.Instance.起手突进 && PLDBaseSlotResolvers.调停Intervene.IsUnlock())
        {
            if (PLDBaseSlotResolvers.调停Intervene.Charges() >= 2)
            {
                slot.Add(new Spell(PLDBaseSlotResolvers.调停Intervene, SpellTargetType.Target));
            }
        }
        
        slot.Add(new Spell(PLDBaseSlotResolvers.先锋剑FastBlade, SpellTargetType.Target));
    }

    private static void Step1(Slot slot)
    {
        if (PLDRotationEntry.QT.GetQt(BaseQTKey.爆发药))
        {
            slot.Add(Spell.CreatePotion());
        }
    }
}