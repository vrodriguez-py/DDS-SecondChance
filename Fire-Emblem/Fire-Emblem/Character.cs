namespace Fire_Emblem;
using Newtonsoft.Json;
using System.IO;

public class Character
{   
    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Spd { get; set; }
    public int Def { get; set; }
    public int Res { get; set; }
    
    
    public static List<Character> ReadCharacters(string file)
    {
        string json = File.ReadAllText(file);
        var characters = JsonConvert.DeserializeObject<List<Character>>(json);
        return characters;
    }
    
    public void ModifyStat(string stat, int modifier)
    {
        switch (stat)
        {
            case "Hp":
                Hp += modifier;
                break;
            case "Atk":
                Atk += modifier;
                break;
            case "Spd":
                Spd += modifier;
                break;
            case "Def":
                Def += modifier;
                break;
            case "Res":
                Res += modifier;
                break;
            default:
                throw new ArgumentException($"Stat {stat} not found.");
        }
    }
    
    public Character Clone()
    {
        return (Character)this.MemberwiseClone();
    }

    
}