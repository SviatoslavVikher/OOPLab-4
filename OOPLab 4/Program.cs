using System;

struct Money
{
    public int WholePart { get; private set; }
    public short FractionalPart { get; private set; }

    
    public Money(int whole = 0, short fractional = 0)
    {
        try
        {
            if (fractional < 0 || fractional >= 100 || whole < 0)
            {
                throw new ArgumentOutOfRangeException("Значення не повинні бути від’ємними або некоректними.");
            }

            WholePart = whole;
            FractionalPart = fractional;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Помилка ініціалізації: {ex.Message}");
            WholePart = 0;
            FractionalPart = 0;
        }
        finally
        {
            Console.WriteLine("Конструктор викликано.");
        }
    }

    /
    public override string ToString()
    {
        return $"{WholePart},{FractionalPart:D2} грн";
    }

    
    public static Money operator +(Money a, Money b)
    {
        int totalFraction = a.FractionalPart + b.FractionalPart;
        int totalWhole = a.WholePart + b.WholePart + totalFraction / 100;
        return new Money(totalWhole, (short)(totalFraction % 100));
    }

    
    public static Money operator -(Money a, Money b)
    {
        int totalA = a.WholePart * 100 + a.FractionalPart;
        int totalB = b.WholePart * 100 + b.FractionalPart;

        if (totalA < totalB)
            throw new InvalidOperationException("Результат від’ємний!");

        int result = totalA - totalB;
        return new Money(result / 100, (short)(result % 100));
    }

    
    public static Money operator /(Money a, int divisor)
    {
        try
        {
            if (divisor == 0)
                throw new DivideByZeroException("Ділення на нуль неможливе.");

            int total = a.WholePart * 100 + a.FractionalPart;
            int result = total / divisor;
            return new Money(result / 100, (short)(result % 100));
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return new Money();
        }
        finally
        {
            Console.WriteLine("Операція ділення завершена.");
        }
    }
}

class Program
{
    static void Main()
    {
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        
        Money money1 = new Money(10, 50);
        Money money2 = new Money(5, 25);

        
        Console.WriteLine("Мінімальна сума: " + money1.ToString());
        Console.WriteLine("Друга сума: " + money2.ToString());

        
        Money sum = money1 + money2;
        Console.WriteLine($"Сума: {sum}");

        
        Money difference = money1 - money2;
        Console.WriteLine($"Різниця: {difference}");

        
        Money divisionResult = money1 / 2;
        Console.WriteLine($"Результат ділення: {divisionResult}");
    }
}
