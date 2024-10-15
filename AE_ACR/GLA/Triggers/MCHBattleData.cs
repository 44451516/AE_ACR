using AEAssist.CombatRoutine;

namespace AE_ACR.GLA.Triggers;
public class PLDBattleData
{
    public static PLDBattleData Instance = new();
    
    public void Reset()
    {
        Instance = new PLDBattleData();
        SpellQueueGCD.Clear();
        SpellQueueoGCD.Clear();
        
    }
    
    public Queue<Spell> SpellQueueGCD = new();
    public Queue<Spell> SpellQueueoGCD = new();
}