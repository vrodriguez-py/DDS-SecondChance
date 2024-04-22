namespace Fire_Emblem.Skills;

public class StatModifierSkill : Skill
{
    // Diccionario para almacenar los modificadores de stats
    public Dictionary<string, int> StatModifiers { get; set; }

    public StatModifierSkill(string name, string description, Dictionary<string, int> statModifiers)
        : base(name, description)
    {
        StatModifiers = statModifiers;
    }

    public override void Activate(Unit unit)
    {
        foreach (var modifier in StatModifiers)
        {
            unit.Character.ModifyStat(modifier.Key, modifier.Value);
            Console.WriteLine($"{Name} aplicado: {modifier.Key} modificado por {modifier.Value}.");
        }
    }
}
