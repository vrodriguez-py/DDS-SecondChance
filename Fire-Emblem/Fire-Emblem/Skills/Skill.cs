namespace Fire_Emblem.Skills;

public abstract class Skill
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string SkillType { get; set; } = "Basic";
    
    public bool IsPassive { get; set; } = false;
    
    public Dictionary<string, int> PlayerEffects { get; set; } = new Dictionary<string, int>();
    public Dictionary<string, int> EnemyEffects { get; set; } = new Dictionary<string, int>();

    public Skill(string name, string description)
    {
        Name = name;
        Description = description;
        InitializeDictionaries();
    }

    private void InitializeDictionaries()
    {
        var stats = new List<string> { "Hp", "Atk", "Spd", "Def", "Res" };
        foreach (var stat in stats)
        {
            PlayerEffects[stat] = 0;
            EnemyEffects[stat] = 0;
        }
    }

    public abstract void Activate(Unit unit);
}
