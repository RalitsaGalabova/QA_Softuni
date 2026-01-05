int[] numbers = Console.ReadLine()  //"3 42 61 7 8 9 10 23"
                .Split()            //["3", "42", "61", "7", "8", "9", "10", "23"]
                .Select(int.Parse)  //[3, 42, 61, 7, 8, 9, 10, 23]
                .ToArray();

int n = int.Parse(Console.ReadLine());

int max = int.MinValue; //най-голямото число
int min = int.MaxValue; //най-малкото число

//обходя само първите n на брой елемента в масива
for (int position = 0; position < n; position++)
{
    int number = numbers[position]; //елемента от масива да текущата позиция
    //проверка дали е максимум
    if (number > max)
    {
        max = number;
    }

    //проверка дали е минимум
    if (number < min)
    {
        min = number;
    }
}

//знаем кое е максималното число
Console.WriteLine(max);
//знаем кое е минималното число
Console.WriteLine(min);


