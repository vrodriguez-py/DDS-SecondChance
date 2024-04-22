using Fire_Emblem_View;

namespace Fire_Emblem;

public class Battle
{
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }
    
    public int round = 1;
    
    public Battle(Team team1, Team team2)
    {
        Team1 = team1;
        Team2 = team2;
    }
    
    public void Start(View _view)
    {
        
        // Check winner and while loop

        while (!IsGameOver())
        {
            List<Unit> units = SelectUnits(_view, round);
            StartRound(units[0], units[1], round, _view); 
            round++;
        }
        EndGame(_view);
    }

    public List<Unit> SelectUnits(View _view, int round)
    {
        if (round % 2 == 0)
        {
            _view.WriteLine("Player 2 selecciona una opción");
            List<Unit> filteredUnits2 = Team2.Units.Where(unit => unit.Character.Hp > 0).ToList(); // Crear una lista filtrada

            int i = 0;
            foreach (Unit unit in filteredUnits2) // Iterar sobre la lista filtrada
            {
                _view.WriteLine($"{i}: {unit.Name}");
                i++;
            }

            int option = int.Parse(_view.ReadLine());
            Unit unit2 = filteredUnits2[option]; // Usar la lista filtrada para obtener la unidad correcta

            
            _view.WriteLine("Player 1 selecciona una opción");
            List<Unit> filteredUnits1 = Team1.Units.Where(unit => unit.Character.Hp > 0).ToList(); // Crear una lista filtrada
            i = 0;
            foreach (Unit unit in filteredUnits1)
            {
                _view.WriteLine($"{i}: {unit.Name}");
                i++;
            }
            option = int.Parse(_view.ReadLine());
            Unit unit1 = filteredUnits1[option];
            return [unit1, unit2];
        }
        else
        {
            _view.WriteLine("Player 1 selecciona una opción");
            List<Unit> filteredUnits1 = Team1.Units.Where(unit => unit.Character.Hp > 0).ToList(); // Crear una lista filtrada
            int i = 0;
            foreach (Unit unit in filteredUnits1)
            {
                _view.WriteLine($"{i}: {unit.Name}");
                i++;
            }
            int option = int.Parse(_view.ReadLine());
            Unit unit1 = filteredUnits1[option];
            
            
            _view.WriteLine("Player 2 selecciona una opción");
            List<Unit> filteredUnits2 = Team2.Units.Where(unit => unit.Character.Hp > 0).ToList(); // Crear una lista filtrada

            i = 0;
            foreach (Unit unit in filteredUnits2) // Iterar sobre la lista filtrada
            {
                _view.WriteLine($"{i}: {unit.Name}");
                i++;
            }

            option = int.Parse(_view.ReadLine());
            Unit unit2 = filteredUnits2[option]; // Usar la lista filtrada para obtener la unidad correcta
            
            return [unit1, unit2];
        }
    }
    public void StartRound(Unit unit1, Unit unit2, int round, View _view)
    {
        // calcular triangulo de armas
        
        if (round % 2 == 0)
        {
            // Player 2 attacks;
            _view.WriteLine($"Round {round}: {unit2.Name} (Player 2) comienza");
            Combat(unit2, unit1, _view);
        }
        else
        {
            _view.WriteLine($"Round {round}: {unit1.Name} (Player 1) comienza");
            Combat(unit1, unit2, _view);
            // Player 1 attacks
        }
    }
    
    public void Combat(Unit attacker, Unit defender, View _view)
    {
        // Calculate damage Luego crear función aparte para imprimir
        double wtb = WeaponTriangle.CalculateWTB(attacker.Character.Weapon, defender.Character.Weapon);
        if (wtb > 1.0)
        {
            _view.WriteLine($"{attacker.Name} ({attacker.Character.Weapon}) tiene ventaja con respecto a {defender.Name} ({defender.Character.Weapon})");
        }
        else if (wtb < 1.0)
        {
            _view.WriteLine($"{defender.Name} ({defender.Character.Weapon}) tiene ventaja con respecto a {attacker.Name} ({attacker.Character.Weapon})");
        }
        else
        {
            _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        }
        // Calculate damage, hay que verificar si el arma es fisica o magica
        int damage = CalculateDamage(attacker, defender);
        
        _view.WriteLine($"{attacker.Name} ataca a {defender.Name} con {damage} de daño");
        defender.Character.Hp -= damage;
        if (CheckCombatWinner(attacker, defender))
        {
            //VER QUE HACER CON GANADOR
            //_view.WriteLine($"{defender.Name} ha sido derrotado"); //por ahora
            _view.WriteLine($"{attacker.Name} ({attacker.Character.Hp}) : {defender.Name} (0)");
            return;
        }
        else
        {
            //counter attack
            damage = CalculateDamage(defender, attacker);
            _view.WriteLine($"{defender.Name} ataca a {attacker.Name} con {damage} de daño");
            attacker.Character.Hp -= damage;
        }
        if (CheckCombatWinner(defender, attacker))
        {
            //VER QUE HACER CON GANADOR
            //_view.WriteLine($"{attacker.Name} ha sido derrotado"); //por ahora
            _view.WriteLine($"{attacker.Name} (0) : {defender.Name} ({defender.Character.Hp})");
            return;
        }
        //follow up
        int followUp = FollowUpAttack(attacker.Character, defender.Character);
        if (followUp == 2)
        {
            damage = CalculateDamage(attacker, defender);
            _view.WriteLine($"{attacker.Name} ataca a {defender.Name} con {damage} de daño");
            defender.Character.Hp -= damage;
            if (CheckCombatWinner(attacker, defender))
            {
                //VER QUE HACER CON GANADOR
                //_view.WriteLine($"{defender.Name} ha sido derrotado"); //por ahora
                _view.WriteLine($"{attacker.Name} ({attacker.Character.Hp}) : {defender.Name} (0)");
                return;
            }
        }
        else if (followUp == 1)
        {
            damage = CalculateDamage(defender, attacker);
            _view.WriteLine($"{defender.Name} ataca a {attacker.Name} con {damage} de daño");
            attacker.Character.Hp -= damage;
            if (CheckCombatWinner(defender, attacker))
            {
                //VER QUE HACER CON GANADOR
                //_view.WriteLine($"{attacker.Name} ha sido derrotado"); //por ahora
                // muestra el estado de las unidades, el que ataca primero
                _view.WriteLine($"{attacker.Name} (0) : {defender.Name} ({defender.Character.Hp})");
                return;
            }
        }
        else
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
        
        
        // Finaliza una ronda de ataque, se muesra el estado de las unidades Dimitri (39) : Claude (19)
        _view.WriteLine($"{attacker.Name} ({attacker.Character.Hp}) : {defender.Name} ({defender.Character.Hp})");
    }
    public bool IsGameOver()
    {
        return Team1.Units.All(unit => unit.Character.Hp <= 0) || Team2.Units.All(unit => unit.Character.Hp <= 0);
    }
    
    public int CalculateDamage(Unit attacker, Unit defender)
    {
        double wtb = WeaponTriangle.CalculateWTB(attacker.Character.Weapon, defender.Character.Weapon);
        int damage = 0;
        if (attacker.Character.Weapon != "Magic") //Cambiar bool por funcion aparte
        {
            damage =(int)Math.Floor(attacker.Character.Atk * wtb) - defender.Character.Def;
            if (damage < 0)
            {
                damage = 0;
            }
        }
        else
        {
            damage = (int)Math.Floor(attacker.Character.Atk * wtb) - defender.Character.Res;
            if (damage < 0)
            {
                damage = 0;
            }
        }
        return damage;
    }
    
    public void EndGame(View _view)
    {
        if (Team1.Units.All(unit => unit.Character.Hp <= 0))
        {
            _view.WriteLine("Player 2 ganó");
        }
        else
        {
            _view.WriteLine("Player 1 ganó");
        }
    }
    
    public bool CheckCombatWinner(Unit attacker, Unit defender)
    {
        return defender.Character.Hp <= 0;
    }
    
    public int FollowUpAttack(Character attack, Character defense)
    {
        if (attack.Spd - defense.Spd >= 5)
        {
            return 2; // Double attack
        }
        else if (defense.Spd - attack.Spd >= 5)
        {
            return 1; // Double Counter attack
        }
        else
        {
            return 0; // No follow up attack
        }
    }
}