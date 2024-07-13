namespace AE_ACR.utils;

// 存放战斗中的缓存数据 战斗重置后也会跟着清除
// 举例： 诗人需要记录上一次的双dot什么时候上的/吃了多少强化资源，来决定是否在恰当的时候立即刷新双dot
public class CombatTime
{
    public TimeSpan combatDuration = new();
    public DateTime combatStart = DateTime.MinValue;
    public DateTime combatEnd;

    public TimeSpan CombatEngageDuration() => combatDuration;

    public void UpdateCombatTimer()
    {
        combatEnd = DateTime.Now;
        combatDuration = combatEnd - combatStart;
    }

    public static CombatTime Instance = new();
}