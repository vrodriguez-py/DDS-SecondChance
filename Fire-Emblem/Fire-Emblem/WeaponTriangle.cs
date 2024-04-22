namespace Fire_Emblem;

public class WeaponTriangle
{

    
    public static double CalculateWTB(string attacker, string defender)
    {

        // Casos de ventaja
        if ((attacker == "Sword" && defender == "Axe") ||
            (attacker == "Axe" && defender == "Lance") ||
            (attacker == "Lance" && defender == "Sword"))
        {
            return 1.2;
        }

        // Casos de desventaja
        if ((attacker == "Sword" && defender == "Lance") ||
            (attacker == "Axe" && defender == "Sword") ||
            (attacker == "Lance" && defender == "Axe"))
        {
            return 0.8;
        }

        // Por defecto, asumimos que no hay ventaja ni desventaja
        return 1.0;
    }

    
}