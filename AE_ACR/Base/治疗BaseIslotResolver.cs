using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.ClientState.Objects.Types;

namespace AE_ACR.Base;

public abstract class 治疗BaseIslotResolver :BaseIslotResolver
{
    public static class TankBuffs
    {
        public const ushort
            铁壁 = 1191,
            亲疏自行 = 1209,
            雪仇 = 1193,
           
            
            留空 = 0;
    }
    
    public const uint
        铁壁 = 7531,
        亲疏自行 = 7548,
        挑衅 = 7533,
        雪仇 = 7535,
        醒梦 = 7562,
        即刻咏唱 =7561 ,
        留空 = 0;

    public static IBattleChara? getTankHpOrderByPercent()
    {
        var battleChara = PartyHelper.CastableAlliesWithin30.Where(r => r.CurrentHp > 0 && r.IsTank()).OrderBy(r => r.CurrentHpPercent()).FirstOrDefault();
        if (battleChara != null && battleChara.IsValid())
        {
            return battleChara;
        }
        
        return null;
    }

    public static int 周围敌人雪仇数量()
    {
        return EnemysIn12DebuffByStatusId(TankBuffs.雪仇);
    }
}