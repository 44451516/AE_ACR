namespace AE_ACR.Base;

public abstract class TankBaseIslotResolver : BaseIslotResolver
{
    public const uint
        铁壁 = 7531,
        亲疏自行 = 7548,
        挑衅 = 7533,
        雪仇 = 7535,
        退避 = 7537,
        留空 = 0;

    public static int 周围敌人雪仇数量()
    {
        return EnemysIn12DebuffByStatusId(TankBuffs.雪仇);
    }

    public static class TankBuffs
    {
        public const ushort
            铁壁 = 1191,
            亲疏自行 = 1209,
            雪仇 = 1193,
            留空 = 0;
    }
}