int n = int.Parse(Console.ReadLine());

bool isFoundMagicNumber = false; //имаме ли намерено поне едно магическо число
//isFoundMagicNumber = true -> имам поне едно магическо число, което е отпечатано
//isFoundMagicNumber = false -> нямаме нито едно магическо число

//всички числа от 1 до n
for (int number = 1; number <= n; number++)
{
    //number -> числото, което трябва да го проверим и да го отпечатаме
    int initialNumber = number; //числото, върху, което ще правя проверките
    //за всяко едно число -> проверявам го дали е магическо

    //проверка:
    //1. дали всяка една от цифрите му е просто число
    bool areAllDigitsPrime = true;
    //areAllDigitsPrime = true -> всички цифри на числото са прости
    //areAllDigitsPrime = false -> не всички цифри на числото са прости

    //2. сумата от цифрите е четно число / се дели на 2
    int sumDigits = 0; //сума от цифрите на числото

    //трябва да взимаме всяка цифра на числото -> проверяваме дали е просто число и трябва да я сумираме
    //initialNumber > 0 -> имаме цифри в числото
    //initialNumber <= 0 -> нямаме повече цифри в числото
    while (initialNumber > 0)
    {
        //number = 3428
        int lastDigit = initialNumber % 10; // 8
        //всички цифри са между 0 и 9
        //прости: 2, 3, 5, 7
        //съставни: 0, 1, 4, 6, 8, 9
        //1. проверка дали цифра е просто число
        if (lastDigit == 0 || lastDigit == 1
            || lastDigit == 4 || lastDigit == 6
            || lastDigit == 8 || lastDigit == 9)
        {
            //имам цифра в числото, която не е просто число
            areAllDigitsPrime = false;
            break;
            //спираме да обхождаме и другите цифри, защото имаме една, която не е просто число
        }

        //2. добавя към общата сума на цифрите
        sumDigits += lastDigit;

        //премахваме проверената цифра от числото
        initialNumber = initialNumber / 10; //342
    }

    //знаем дали всички цифри са прости числа -> areAllDigitsPrime
    //знаем сумата от цифрите на числото
    if (areAllDigitsPrime && sumDigits % 2 == 0)
    {
        //магическо число
        Console.Write(number + " ");
        isFoundMagicNumber = true;
    }
}

//обходили сме всички числа от 1 до n
if (isFoundMagicNumber == false)
{
    //нямаме нито едно магическо число, което да е намерено и отпечатано
    Console.WriteLine("no");
}


//метод, който ми проверява дали дадена цифра е просто число
//true -> ако цифрата е просто число (бр. делителите == 2)
//false -> ако цифрата е съставно число (бр.делителите > 2)
static bool IsPrime(int digit)
{
    //digit = 3
    int count = 0; //броя на делителите на числото

    for (int number = 1; number <= digit; number++)
    {
        if (digit % number == 0)
        {
            //number е делител на моята цифра
            count++;
        }
    }

    //знам колко е броят на делителите
    return count == 2;
}

