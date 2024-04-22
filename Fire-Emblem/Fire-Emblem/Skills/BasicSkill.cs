namespace Fire_Emblem.Skills
{
    public class BasicSkill : Skill
    {
        public BasicSkill(string name, string description) : base(name, description) { }

        public override void Activate(Unit unit)
        {
            // Implementation for activating the skill
            Console.WriteLine($"Activating {Name} for {unit.Name}");
        }
    }
}