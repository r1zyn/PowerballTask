int[,] lottoNumbers = new int[10, 8];
int[] divisions = new int[] {
    0,
    25111111,
    35771,
    1270,
    110,
    55,
    41,
    15
};
char[] alphabets = new char[10] {
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'
};
int[] winningNumbers = new int[8];
int bonusball = 0;
int powerball = 0;
int totalWinnings = 0;
int tries = 0;

while (totalWinnings == 0)
{
    WriteTicket();
    CreateDivisions();
    DisplayWins();

    tries++;
}

Console.WriteLine("It took {0} tries to win", tries);

void CreateDivisions()
{
    int correctNumbers = 0;
    bool hasBonusball = false;
    bool hasPowerball = false;

    GenerateWinningNumber();
    Console.WriteLine("The winning numbers are: " + string.Join(" ", winningNumbers));

    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            if (j < 6)
            {
                if (lottoNumbers[i, j] == winningNumbers[j]) correctNumbers++;
            }
            else if (j == 6)
            {
                if (lottoNumbers[i, j] == bonusball) hasBonusball = true;
            }
            else if (j == 7)
            {
                if (lottoNumbers[i, j] == powerball) hasPowerball = true;
            }
        }

        switch (correctNumbers)
        {
            case 6:
                if (hasPowerball) totalWinnings += divisions[1]; break;
            case 5:
                if (hasPowerball)
                {
                    if (hasBonusball) totalWinnings += divisions[2];
                    else totalWinnings += divisions[3];
                }

                break;
            case 4:
                if (hasPowerball)
                {
                    if (hasBonusball) totalWinnings += divisions[4];
                    else totalWinnings += divisions[5];
                }

                break;
            case 3:
                if (hasPowerball)
                {
                    if (hasBonusball) totalWinnings += divisions[6];
                    else totalWinnings += divisions[7];
                }

                break;
            default:
                totalWinnings += divisions[0]; break;
        }

        correctNumbers = 0;
        hasBonusball = false;
        hasPowerball = false;
    }
}

void DisplayWins()
{
    Console.WriteLine("Total winnings: $" + totalWinnings);
}

void GenerateWinningNumber()
{
    for (int i = 0; i < 8; i++)
    {
        if (i < 7) winningNumbers[i] = new Random().Next(1, 41);
        else if (i == 7) winningNumbers[i] = new Random().Next(1, 10);
    }

    
    // Uncomment this and comment part above to test that the program works when there is a winning line
    // for (int i = 0; i < 10; i++)
    // {
    //     for (int j = 0; j < 8; j++)
    //     {
    //         winningNumbers[j] = lottoNumbers[i, j];
    //     }
    // }

    bonusball = winningNumbers[6];
    powerball = winningNumbers[7];
}

void WriteTicket()
{
    WriteEmptyLine();
    WriteLineEffect(ApplyEffect(new string(' ', (41 - "POWERBALL".Length) / 2 + 1) + "POWERBALL" + new string(' ', (41 - "POWERBALL".Length) / 2 - 1), Effects.Bold), Effects.BackgroundYellow);
    WriteLineEffect(CenterText("TRIPLE DIP ", 41), Effects.BackgroundYellow);
    WriteEmptyLine();
    WriteLineEffect("POWER".PadLeft(40) + " ", Effects.BackgroundYellow);
    WriteLineEffect("BALL".PadLeft(39) + "  ", Effects.BackgroundYellow);
    WriteLineEffect(new string('=', 41), Effects.BackgroundYellow);

    for (int i = 0; i < 10; i++)
    {
        WriteEffect("  " + $"{alphabets[i]}." + "  ", Effects.BackgroundYellow);

        for (int j = 0; j < 8; j++)
        {
            if (j < 7)
            {
                lottoNumbers[i, j] = new Random().Next(1, 41);
                WriteEffect(lottoNumbers[i, j].ToString().PadLeft(2, '0') + "  ", Effects.BackgroundYellow);
            }
            else if (j == 7)
            {
                lottoNumbers[i, j] = new Random().Next(1, 10);
                WriteEffect("|  " + lottoNumbers[i, j].ToString().PadLeft(2, '0') + "  ", Effects.BackgroundYellow);
            }
        }

        Console.WriteLine();
    }

    WriteLineEffect(new string('=', 41), Effects.BackgroundYellow);
    WriteLineEffect("  PRICE" + "$15.00".PadLeft(40 - "  PRICE".Length) + " ", Effects.BackgroundYellow);
    WriteLineEffect("  DRAW" + FillNumber(new Random().Next(10000), 4).PadLeft(40 - "  DRAW".Length) + " ", Effects.BackgroundYellow);
    WriteLineEffect(DateTime.Now.DayOfWeek.ToString().ToUpper().Substring(0, 3).PadLeft(40) + " ", Effects.BackgroundYellow);
    WriteLineEffect($"{DateTime.Now.Day} {GetMonth(DateTime.Now.Month)} {DateTime.Now.Year.ToString().Substring(2, 2)}".PadLeft(40) + " ", Effects.BackgroundYellow);
    WriteEmptyLine();
    WriteLineEffect($"  RET {FillNumber(new Random().Next(999999), 6)}" + $"{FillNumber(new Random().Next(3), 3)}-{FillNumber(new Random().Next(Convert.ToInt32(1000000000)), 9)}-{FillNumber(new Random().Next(10000), 4)}".PadLeft(28) + " ", Effects.BackgroundYellow);
    WriteEmptyLine();
    WriteLineEffect($"  {FillNumber(new Random().Next(999999), 6)}" + ApplyEffect("======".PadLeft(32) + " ", Effects.Invisible), Effects.BackgroundYellow);
    WriteLineEffect(CenterText("EACH LOTTO TICKET YOU BUY", 41), Effects.BackgroundYellow);
    WriteLineEffect(CenterText("FUNDS AMAZING COMMUNITY", 41), Effects.BackgroundYellow);
    WriteLineEffect(CenterText(" SERVICES AND PROJECTS.", 41), Effects.BackgroundYellow);
    WriteLineEffect(CenterText("GOOD ON YOU LOTTO PLAYERS", 41), Effects.BackgroundYellow);
    WriteEmptyLine();
    WriteEmptyLine();
    WriteLineEffect(new string(' ', 6) + GenerateBarcode(30) + new string(' ', 5), Effects.BackgroundYellow); // To fix - yellow background not working for this line
    WriteEmptyLine();
    Console.WriteLine();
}

string ApplyEffect(string input, string effect)
{
    return effect + input + Effects.Reset;
}

string FillNumber(int number, int width)
{
    return number.ToString().PadLeft(width, '0');
}

string GenerateBarcode(int width)
{
    string barcode = string.Empty;
    for (int i = 0; i < width; i++)
    {
        int bold = new Random().Next(0, 2);

        switch (bold)
        {
            case 0: barcode += "|"; break;
            case 1: barcode += ApplyEffect("|", Effects.Bold); break;
        }
    }

    return barcode;
}

string GetMonth(int month)
{
    switch (month)
    {
        case 1: return "JAN";
        case 2: return "FEB";
        case 3: return "MAR";
        case 4: return "APR";
        case 5: return "MAY";
        case 6: return "JUN";
        case 7: return "JUL";
        case 8: return "AUG";
        case 9: return "SEP";
        case 10: return "OCT";
        case 11: return "NOV";
        case 12: return "DEC";
        default: return "";
    }
}

void WriteEffect(string input, string effect)
{
    Console.Write(ApplyEffect(effect + input + Effects.Reset, Effects.Black));
}

void WriteEmptyLine()
{
    WriteLineEffect(ApplyEffect(new string('=', 41), Effects.Invisible), Effects.BackgroundYellow);
}

void WriteLineEffect(string input, string effect)
{
    Console.WriteLine(ApplyEffect(effect + input + Effects.Reset, Effects.Black));
}

string CenterText(string text, int width)
{
    if (width % 2 == 0)
    {
        return new string(' ', (width - text.Length) / 2) + text + new string(' ', (width - text.Length) / 2);
    }
    else
    {
        return new string(' ', ((width - text.Length) / 2) + 1) + text + new string(' ', ((width - text.Length) / 2) - 1);
    }
}

public class Effects
{
    public static string BackgroundYellow = "\u001b[43m";
    public static string Black = "\u001b[30m";
    public static string Bold = "\u001b[1m";
    public static string BrightYellow = "\u001b[33;1m";
    public static string Invisible = "\u001b[8m";
    public static string Reset = "\u001b[0m";
    public static string Yellow = "\u001b[33m";
}