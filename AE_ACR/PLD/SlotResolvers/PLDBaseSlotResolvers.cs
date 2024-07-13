using AE_ACR.utils;

namespace AE_ACR.PLD.SlotResolvers;

public abstract class PLDBaseSlotResolvers : BaseIslotResolver
{
    public const uint
        先锋剑FastBlade = 9,
        暴乱剑RiotBlade = 15,
        ShieldBash = 16,
        战女神之怒RageOfHalone = 21,
        厄运流转CircleOfScorn = 23,
        投盾ShieldLob = 24,
        深奥之灵SpiritsWithin = 29,
        沥血剑GoringBlade = 3538,
        王权剑RoyalAuthority = 3539,
        全蚀斩TotalEclipse = 7381,
        安魂祈祷Requiescat = 7383,
        安魂祈祷Requiescatv2 = 36921,
        安魂祈祷徐剑 = 36922,
        圣灵HolySpirit = 7384,
        日珥斩Prominence = 16457,
        圣环HolyCircle = 16458,
        大保健连击Confiteor = 16459,
        偿赎剑Expiacion = 25747,
        BladeOfFaith = 25748,
        BladeOfTruth = 25749,
        BladeOfValor = 25750,
        战逃反应FightOrFlight = 20,
        赎罪剑Atonement3 = 36919,
        赎罪剑Atonement2 = 36918,
        赎罪剑Atonement = 16460,
        调停Intervene = 16461,
        盾阵Sheltron = 3542,
        预警 = 17,
        钢铁信念 = 28,
        测试 = 31,
        留空 = 0;

    public static class Buffs
    {
        public const ushort
            Requiescat = 1368,
            FightOrFlight = 76,
            悔罪预备 = 3019,
            DivineMight = 2673,
            HolySheltron = 2674,
            沥血剑BUFFGoringBladeReady = 3847,
            赎罪剑Atonement1BUFF = 1902,
            赎罪剑Atonement2BUFF = 3827,
            赎罪剑Atonement3BUFF = 3828,
            预警 = 74,
            留空 = 0;
    }
}