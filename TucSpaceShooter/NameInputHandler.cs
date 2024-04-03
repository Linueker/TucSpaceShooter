using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class NameInputHandler
{
    private string playerName = "";

    public string PlayerName { get => playerName; set => playerName = value; }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }


    public void HandleTextInput(TextInputEventArgs e)
    {
        // Kontrollera om inmatningen är en vanlig tecken eller ett mellanslag
        if (char.IsLetterOrDigit(e.Character) || char.IsWhiteSpace(e.Character))
        {
            // Lägg till tecknet till spelarens namn
            playerName += e.Character;
        }
        // Kontrollera om inmatningen är en backspace
        else if (e.Character == '\b' && playerName.Length > 0) // '\b' representerar backspace-tecknet
        {
            // Ta bort det sista tecknet från spelarens namn
            playerName = playerName.Remove(playerName.Length - 1);
        }
    }
}
