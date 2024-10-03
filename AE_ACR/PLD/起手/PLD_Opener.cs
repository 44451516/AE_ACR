using AE_ACR.PLD.SlotResolvers;
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
            Step0,//先锋剑
            Step1,//爆发药
            // Step2//花

    };
    
    private static void Step0(Slot slot)
    {
        slot.Add(new Spell(PLDBaseSlotResolvers.先锋剑FastBlade, SpellTargetType.Target));
    }
    private static void Step1(Slot slot)
    {
        if (PLDRotationEntry.QT.GetQt(PLDQTKey.爆发药))
        {
            slot.Add(Spell.CreatePotion());
        }
    }

    
    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(2000, PLDBaseSlotResolvers.圣灵HolySpirit, SpellTargetType.Target);
    }
}