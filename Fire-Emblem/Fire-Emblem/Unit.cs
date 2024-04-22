namespace Fire_Emblem;


// Character que puede tener Abilities
public class Unit
{
    public string Name { get; set; }
    public List<string> Abilities { get; set; } = new List<string>();
    public Character Character { get; set; }
    
}