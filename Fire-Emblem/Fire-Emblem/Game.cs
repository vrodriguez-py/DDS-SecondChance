using Fire_Emblem_View;
using Fire_Emblem.Skills;


namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    


    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        //Crear funcion para cargar los personajes, hacer lo mismo para habilidades (proxima entrega)
        string charactersFilePath = "characters.json";
        var characters = Character.ReadCharacters(charactersFilePath);

        _view.WriteLine("Elige un archivo para cargar los equipos");

        // Cambiar y crear funcion
        List<string> files = FileHelper.PrintFiles(_teamsFolder, _view);
        
        // Crear funcion
        string input = _view.ReadLine();
        string fileName = files[int.Parse(input)];
        string filePath = $"{_teamsFolder}/{fileName}";
        
        var skillManager = new SkillManager(); //necesita ser clase?
        var skills = skillManager.LoadSkills("skills.json");
        
        //Antes de esto, leer las habilidades
        var teams = Team.ReadTeams(filePath, skills);
        if (!teams.All(team => team.IsTeamValid()))
        {
            _view.WriteLine("Archivo de equipos no válido");
            return;
        }
        
        // Crear funcion en teams
        foreach (var team in teams)
        {
            foreach (var unit in team.Units)
            {
                Character character = characters.FirstOrDefault(c => c.Name == unit.Name);
                unit.Character = character.Clone();
            }
        }
        
        
        // Funciona ok, ahora hacer el combate
        Battle battle = new Battle(teams[0], teams[1]);
        battle.Start(_view);
    }
    

}