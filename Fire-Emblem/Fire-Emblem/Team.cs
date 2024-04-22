namespace Fire_Emblem;
using Fire_Emblem.Skills;

public class Team
{
    public string Name { get; set; }
    public List<Unit> Units { get; set; } = new List<Unit>();
    
    public static List<Team> ReadTeams(string file, Dictionary<string, BasicSkill> skills)
    {
        var teams = new List<Team>();
        Team currentTeam = null;

        var lines = File.ReadAllLines(file);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("Player"))
            {
                currentTeam = new Team { Name = line };
                teams.Add(currentTeam);
            }
            else
            {
                var parts = line.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                var unitName = parts[0].Trim();
                List<string> abilities = parts.Length > 1 
                    ? parts[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(ability => ability.Trim())  // Trimming each ability to clean up whitespace
                        .ToList()
                    : new List<string>();
                
                List<BasicSkill> realSkills = SkillManager.stringToSkill(abilities, skills);
                var unit = new Unit
                {
                    Name = unitName,
                    Abilities = realSkills
                };
                //Console.WriteLine($"Unidad {unitName} con habilidades {string.Join(", ", abilities)}");
                
                //Console.WriteLine($"Unidad {unitName} con habilidades {string.Join(", ", abilities)}"); ACA necesito inicializar las habilidades
                currentTeam.Units.Add(unit);
            }
        }

        return teams;
    }
    
    public bool IsTeamValid()
    {
        if (!Units.Any() || Units.Count > 3) // Check for empty teams or teams with more than three units
        {
            return false;
        }

        var unitNames = new HashSet<string>();
        foreach (var unit in Units)
        {
            // Check for duplicate units
            if (!unitNames.Add(unit.Name))
            {
                return false;
            }

            // Check for units with more than two abilities or duplicate abilities
            if (unit.Abilities.Count > 2 || unit.Abilities.Distinct().Count() != unit.Abilities.Count)
            {
                return false;
            }
        }

        return true;
    }
    
    
    
}