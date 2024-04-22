using Fire_Emblem_View;

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
        for (int i = 0; i < 500; i++)
        {
            string file = i.ToString();
            if (file.Length == 1)
                file = $"00{file}";
            else if (file.Length == 2)
                file = $"0{file}";

            _view.WriteLine($"{i}: {file}.txt");
        }
        
        // Crear funcion
        string input = _view.ReadLine();
        input = input.PadLeft(3, '0');
        string filePath = Path.Combine(_teamsFolder, $"{input}.txt");


        var teams = Team.ReadTeams(filePath);
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