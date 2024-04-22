namespace Fire_Emblem.Skills;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class SkillManager
{
    public Dictionary<string, BasicSkill> LoadSkills(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        var skills = JsonConvert.DeserializeObject<List<BasicSkill>>(jsonData);

        var skillDictionary = new Dictionary<string, BasicSkill>();
        foreach (var skill in skills)
        {
            InitializeSkills(skill);
            skillDictionary.Add(skill.Name, skill);
        }
        return skillDictionary;
    }

    private void InitializeSkills(Skill skill)
    {
        switch (skill.Name)
        {
            case "Attack +6":
                skill.PlayerEffects["Atk"] = 6;
                break;
            case "Speed +5":
                skill.PlayerEffects["Spd"] = 5;
                break;
            case "Defense +5":
                skill.PlayerEffects["Def"] = 5;
                break;
            case "Resistance +5":
                skill.PlayerEffects["Res"] = 5;
                break;
            case "HP +15":
                skill.PlayerEffects["HP"] = 15;
                skill.IsPassive = true;
                break;
            case "Atk/Def +5":
                skill.PlayerEffects["Atk"] = 5;
                skill.PlayerEffects["Def"] = 5;
                break;
            case "Atk/Res +5":
                skill.PlayerEffects["Atk"] = 5;
                skill.PlayerEffects["Res"] = 5;
                break;
            case "Spd/Res +5":
                skill.PlayerEffects["Spd"] = 5;
                skill.PlayerEffects["Res"] = 5;
                break;
            case "Spd/Def +5":
                skill.PlayerEffects["Spd"] = 5;
                skill.PlayerEffects["Def"] = 5;
                break;
            case "Atk/Spd +5":
                skill.PlayerEffects["Atk"] = 5;
                skill.PlayerEffects["Spd"] = 5;
                break;
            default:
                Console.WriteLine("No direct modifications or condition found for: " + skill.Name);
                break;
        }
    }
    
    public static List<BasicSkill> stringToSkill(List<string> skillNames, Dictionary<string, BasicSkill> skills)
    {
        List<BasicSkill> skillList = new List<BasicSkill>();
        foreach (var skillName in skillNames)
        {
            if (skills.ContainsKey(skillName))
            {
                var skill = skills[skillName];
                skillList.Add(skill);
            }
        }
        return skillList;
    }
    

}

