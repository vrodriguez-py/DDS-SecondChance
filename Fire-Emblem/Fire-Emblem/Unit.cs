using Fire_Emblem.Skills;

namespace Fire_Emblem;
using Fire_Emblem.Skills;

public class Unit
{
    public string Name { get; set; }
    public List< BasicSkill > Abilities { get; set; } = new List<BasicSkill>();
    public Character Character { get; set; }
    public Dictionary<string, int> Bonuses { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> FirstAttackBonuses { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> FollowUpBonuses { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> Penalties { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> FirstAttackPenalties { get; private set; } = new Dictionary<string, int>();
    public Dictionary<string, int> FollowUpPenalties { get; private set; } = new Dictionary<string, int>();

    public Unit()
    {
        InitializeDictionaries();
    }
    private void InitializeDictionaries()
    {
        var stats = new List<string> { "Hp", "Atk", "Spd", "Def", "Res" };

        foreach (var stat in stats)
        {
            Bonuses[stat] = 0;
            FirstAttackBonuses[stat] = 0;
            FollowUpBonuses[stat] = 0;
            Penalties[stat] = 0;
            FirstAttackPenalties[stat] = 0;
            FollowUpPenalties[stat] = 0;
        }
    }
    
    public void ClearModifiers()
    {
        InitializeDictionaries();
    }
}