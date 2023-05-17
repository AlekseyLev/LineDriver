using System;
using System.Xml.Linq;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
public class CityWay
{
        string nameWay; // номер маршрута
        public decimal wayLength; // расстояние от начальной до конечной точки маршрута в км
        public int numDistrict; // количество районов
        public int[] numBusStop; // массив с количеством остановок в каждом районе
        public int sumBusStop; // исходя из кол-ва районов и остановок в них, посчитаем Итого остановок в городе

        // Данные о пассажирах
        public int minPasFirst; // min кол-во пассажиров при выезде из точки А
        public int maxPasFirst; // max кол-во пассажиров при выезде из точки А
        public int minPasSecond; // кол-во пассажиров на других остановках
        public int maxPasSecond;


        private int idCityWay; // Идентификатор записи со счётчиком записей ниже
        static int counter = 0;


        public void displayInfo()
        {
            Console.WriteLine("Маршрут \"{0}\" (ID записи = {1}): длина маршрута - {2} км, количество районов - {3}. Количество остановок - {4}, из них:",
                    nameWay, idCityWay, wayLength, numDistrict, sumBusStop);
            string str = ";";
            for (int i = 0; i < numBusStop.Length; i++)
            {
                if (i == numBusStop.Length - 1) str = "";
                Console.Write($"в районе {i + 1} - {numBusStop[i]} шт.{str}\n");
            }
            Console.WriteLine($"Количество пассажиров на стартовой остановке - от {minPasFirst} до {maxPasFirst}.");
        if (sumBusStop > 2) Console.WriteLine($"Количество пассажиров на остальных остановках - от {minPasSecond} до {maxPasSecond}.");
        }

    public int GetSumBusStop() { return sumBusStop; }

    //Деконструктор
    public void Deconstruct(out string sNameWay, out decimal dWayLength, out int iNumDistrict,
                out int iSumBusStop, out int iMinPasFirst, out int iMaxPasFirst, out int iMinPasSecond, out int iMaxPasSecond, out int[] iNumBusStop)
    {
        sNameWay = nameWay;
        dWayLength = wayLength;
        iNumDistrict = numDistrict;
        iNumBusStop = new int[numBusStop.Length];
        for (int i = 0; i < numBusStop.Length; i++) iNumBusStop[i] = numBusStop[i];
        
        iSumBusStop = sumBusStop;
              
        iMinPasFirst  = minPasFirst;
        iMaxPasFirst  = maxPasFirst;
        iMinPasSecond = minPasSecond;
        iMaxPasSecond = maxPasSecond;
    }


//Конструкторы
    public CityWay()
        {
            this.nameWay = "Маршрутное такси №127"; 
            this.wayLength = 33;             
            this.numBusStop = new int[] { 34, 21, 16 };
            this.numDistrict = numBusStop.Length;

        for (int i = 0; i < numBusStop.Length; i++) this.sumBusStop += numBusStop[i]; //сумма всех остановок

        // Данные о пассажирах
            this.minPasFirst = 1;
            this.maxPasFirst = 6;
            this.minPasSecond = 1;
            this.maxPasSecond = 3;

            counter++;
            this.idCityWay = counter;
        }

    public bool SetNameWay()
    {
        bool result = false;
        string answer;
        Console.Write("Номер/имя маршрута - ");
#nullable disable
        answer = Console.ReadLine();

        if (answer != "" && answer.Length < 30)
        {
            nameWay = answer;
            result = true;
        }
        return result;
#nullable enable
    }

    public bool SetWayLength()
    {
        bool result = false;
        decimal answer;
#nullable disable
        try
        {
            Console.Write("Длина маршрута в км - ");
            answer = Convert.ToDecimal(Console.ReadLine().Trim().Replace(".", ","));

            if (answer > 0)
            {
            wayLength = answer;
            result = true;
            }
        }
        catch { Console.WriteLine("Ошибка принятия значения."); }
        return result;
    }
#nullable enable

    public bool SetNumDistrict()
    {
        bool result = false;
        int answer;
#nullable disable
        try
        {
            Console.Write("\nЦены за проезд могут отличаться между районами города. Нам нужно это учесть.\n" +
                          "Сколько районов в городе в плане ценового разделения стоимости проезда - ");
            answer = Convert.ToInt32(Console.ReadLine().Trim());

            if (answer > 0 && answer < 127)
            {
                numDistrict = answer;
                result = true;
            }
        }
        catch { Console.WriteLine("Ошибка принятия значения."); }
        return result;
#nullable enable
    }

    public bool SetNumBusStop()
    {
        bool result = false;
        int[] answer = new int[numDistrict];
        numBusStop = new int[numDistrict];
        int start = 2, end = 127;
        sumBusStop = 0;

        Console.WriteLine("\nВведите количество остановок (включая конечные пункты А и Б):");
        for (int i = 0; i < numDistrict; i++)
        {
            try
            {
                result = false;
                Console.Write($"для {i + 1}-го района - ");
                answer[i] = Convert.ToInt32(Console.ReadLine());
                if (answer[i] < start && answer[i] > end)
                {
                    Console.WriteLine($"Ошибка: введённое значение выходит за диапазон значений [{start};{end}]. Повторите ввод:");
                    i--;                    
                }
                else result = true;

            }
            catch (Exception) { 
                Console.WriteLine("Ошибка принятия значения. Введите числовое значение:");
                result = false;
                i--;
            }
            
        }
        if (!result) Console.WriteLine("Операция ввода остановок прервана. Необходим повторный ввод.\n");
        else
        {
            for (int i = 0; i < answer.Length; i++)
            {
                numBusStop[i] = answer[i];
                sumBusStop += answer[i];
            } 
        } 
        return result;
    }

    public bool SetMinMaxFirst(int numOfPass)
    {
        Console.WriteLine(); // сделаем пустую строку на выходе
        string str = "для первой остановки (пункт А)";
        GetMinMax(str, 0, numOfPass, out minPasFirst, out maxPasFirst, out bool result);
        return result;
    }

    public bool SetMinMaxSecond(int numOfPass)
    {        
        string str = "для остальных остановок";
        GetMinMax(str, 0, numOfPass, out minPasSecond, out maxPasSecond, out bool result);
        return result;
    }


    private void GetMinMax(string str, int start, int inumOfPass, out int min, out int max, out bool result)
    {
        result = false;

        Console.WriteLine($"Введём диапазон Min и Max пассажиров (в данном случае наша пассажироёмкость от {start} до {inumOfPass} чел.) {str}:");
        min = max = -1;
        bool a, b;
        a = b = result = false;
        while (!result)
        { 
            try
            {
                if (!a)
                {
                    Console.Write("Min (чел) - ");
                    min = Convert.ToInt32(Console.ReadLine());
                    if (min >= 0 && min <= inumOfPass) a = true;
                }
                if(a)
                {                    
                    Console.Write("Max (чел) - ");
                    max = Convert.ToInt32(Console.ReadLine());
                    if (max >= min && max <= inumOfPass) result = true;
                }
                

            }
            catch (Exception) { Console.Write("Ошибка принятия значения. "); }
            if (!result) Console.WriteLine($"Значение Min вводится в диапазоне от {start} до {inumOfPass}, значение Max - от \"Min\" до {inumOfPass}. " +
                $"Повторите ввод:");
        }
    }

    /*
        //        Console.WriteLine(@"
        //Учебное консольное приложение на C# - LineDriver. 28.03.2023
        //                      ██████████████████████████████████▄
        //                     █     █     █      █     █     █████
        //                     █▀▀▀▀▀█▀▀▀▀▀█▀▀▀▀▀▀█▀▀▀▀▀█▀▀▀▀▀█████
        //                     █     █     █      █     █     █████
        //                     █     █     █      █     █     █████
        //                  ▄███▄▄▄▄▄█▄▄▄▄▄█▄▄▄▄▄▄█▄▄▄▄▄█▄▄▄▄▄█████
        //                 ████████████████████████████████████████
        //                 ██▀▀▄▄▀▀████████████████████▀▀▄▄▀▀██████
        //                 █ ██████ ██████████████████ ██████ ████▀
        //                   ▀████▀                    ▀████▀
        //▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀
        //");     
    
     
     
  
        public CityWay(int numOfPass)
        { // передаем кол-во загрузки пассажиров транспорта для проверок числа пассажиров на остановках в этом методе

            bool a = false, b = false, c = false, errCheck = false;
            string sNameWay = "", WayLength = "", numDistrict = "";

            // ниже 3 строки присвоения для работы конструктора со словом this на ~205 строке
            decimal fWayLength = 1;
            int numDistrict = 1; int minPasFirst = 1; int maxPasFirst = 1; int minPasSecond = 1; int maxPasSecond = 1;
            int[] numBusStop = { 1 };


            while (!a || !b || !c)
            {
                errCheck = false;

                if (a == false)
                {

                    Console.Write("Номер/имя маршрута - ");
                    sNameWay = Console.ReadLine().Trim();
                    if (sNameWay.Length > 0 && sNameWay.Length < 128) a = true;
                }
                if (b == false)
                {
                    Console.Write("Длина маршрута в км - ");
                    WayLength = Console.ReadLine().Trim().Replace(",", ".");
                }
                if (!c)
                { // более короткая запись false
                    Console.Write("Цены за проезд могут отличаться между районами города. Нам нужно это учесть.\n" +
                                     "Сколько районов в городе в плане ценового разделения стоимости проезда - ");
                    numDistrict = Console.ReadLine().Trim();
                }
                try
                {
                    fWayLength = (decimal)Convert.ToDecimal(WayLength);
                    if (fWayLength > 0) b = true;
                    else Console.WriteLine("Ошибка: некорректное значение длины маршрута");
                }
                catch (Exception ex) { errCheck = true; }
                try
                {
                    numDistrict = Convert.ToInt32(numDistrict);
                    if (numDistrict > 0 && numDistrict < 32) c = true;
                    else Console.WriteLine("Ошибка: некорректное значение количества районов");
                }
                catch (Exception ex) { errCheck = true; }

                if (errCheck == true) { Console.WriteLine("Ошибка принятия значения. Необходим повторный ввод."); }
            }

            numBusStop = new int[numDistrict];
            Console.WriteLine("Введите количество остановок (включая конечные пункты А и Б):");
            for (int i = 0; i < numDistrict; i++)
            {
                a = false;
                while (!a)
                {
                    try
                    {
                        Console.Write($"для {i + 1}-го района - ");
                        numBusStop[i] = Convert.ToInt32(Console.ReadLine());
                        if (numBusStop[i] > 0) a = true;

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ошибка: введите корректное значение!");
                    }
                    if (numBusStop[i] == 0) Console.WriteLine("В каждом районе, если его наличие было указано ранее, должно быть не менее одной остановки. Повторите ввод:");
                }
            }

            Console.WriteLine("Отлично, ввод остановок в каждом районе выполнен!\n\n" +
                    "Теперь давайте прикинем наличие на остановках потенциальных клиентов-пассажиров.\n" +
                    "Число потенциальных пассажиров будет выбрано случайным образом, но из конкретного диапазона, который мы укажем.\n" +
                    "Определимся с загрузкой выбранного ТС пассажирами при выходе на маршрут со стартовой станции/остановки.\n");
            Console.Write($"Введите диапазон min и Max пассажиров (в данном случае наша пассажироёмкость от 0 до {numOfPass} чел.):\n");

            a = false; b = false; c = false; errCheck = false;


            while (!a || !b)
            {
                try
                {
                    if (a == false)
                    {
                        Console.Write("min (чел) -");
                        int Min = Convert.ToInt32(Console.ReadLine());
                        if (numOfPass >= Min && Min >= 0)
                        {
                            minPasFirst = Min;
                            a = true;
                            c = true;
                        }
                    }
                    if (a && !b)
                    {
                        Console.Write("Max (чел) -");
                        int Max = Convert.ToInt32(Console.ReadLine());
                        if (numOfPass >= Max && Max > minPasFirst)
                        {
                            maxPasFirst = Max;
                            b = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    // System.out.println("Ошибка: некорректное значение. Повторите ввод")
                    // errCheck = true;
                }
                if (!a || !b) Console.WriteLine("Ошибка: некорректное значение. Повторите ввод");

            }

            a = false; b = false; c = false; errCheck = false;
            Console.WriteLine("Готово. ");
            // И теперь, если у нас больше 1 района или больше 1 остановки в одном районе, то определимся с другими остановками
            if (numBusStop[0] > 1 || numDistrict > 1)
            {
                Console.Write("\nТеперь определимся с min и Max числом пассажиров на других остановках.\n");
                while (a == false || b == false)
                {
                    try
                    {
                        if (a == false)
                        {
                            Console.Write("min (чел) -");
                            int Min = Convert.ToInt32(Console.ReadLine());
                            if (numOfPass >= Min && Min >= 0)
                            {
                                minPasSecond = Min;
                                a = true;
                            }
                        }
                        if (a == true && b == false)
                        {
                            Console.Write("Max (чел) -");
                            int Max = Convert.ToInt32(Console.ReadLine());
                            if (numOfPass >= Max && Max > minPasSecond)
                            {
                                maxPasSecond = Max;
                                b = true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        // System.out.println("Ошибка: некорректное значение. Повторите ввод");
                        errCheck = true;
                    }
                    if (!a || !b) Console.WriteLine("Ошибка: некорректное значение. Повторите ввод");

                }
            }
            //new CityWay() : this(numBusStop, nameWay, numDistrict, wayLength, minPasFirst,
            //            maxPasFirst, minPasSecond, maxPasSecond);
            //CityWay line1 = new(numBusStop, nameWay, numDistrict, wayLength, minPasFirst,
            //            maxPasFirst, minPasSecond, maxPasSecond);
            this.nameWay = sNameWay;
            this.wayLength = fWayLength;
            this.numDistrict = numDistrict;
            this.numBusStop = new int[numDistrict];
            int SumBusStop = 0;
            for (int i = 0; i < numDistrict; i++)
            {  // массив с количеством остановок в каждом районе
                this.numBusStop[i] = numBusStop[i];
                SumBusStop += numBusStop[i];
            }

            this.SumBusStop = SumBusStop;     // Всего остановок на маршруте во всех районах
            this.minPasFirst = minPasFirst;   // min кол-во пассажиров при выезде из точки А
            this.maxPasFirst = maxPasFirst;   // max кол-во пассажиров при выезде из точки А
            this.minPasSecond = minPasSecond; // кол-во пассажиров на других остановках
            this.maxPasSecond = maxPasSecond; // кол-во пассажиров на других остановках

            counter++;
            this.idCityWay = counter; // почему так с лишней переменной, а не ++id? ответ в классе Car

            Console.WriteLine("\nВвод данных по параметрам маршрута выполнен!");
        }

        // Получение случайного int числа из диапазона [min; max] или, если точнее, [min; max+1).
        static int getRandomInt(int min, int max)
        {
            Random rnd = new Random();
            int result = rnd.Next(min, max);
            return result;
    }
    */
}

