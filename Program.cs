global using Text_msj;

class Program
{
    static void Main(string[] args)
    {        
        Console.WriteLine("Учебное консольное приложение на C# - LineDriver. 28.03.2023");
        Console.WriteLine("------------------------------------------------------------\n");
        Console.WriteLine("\nПриветствую! Данная программа позволит сделать расчёт потенциального дохода для владельцев маршрутных транспортных средств.\n");
        byte step = 0,
             sumstep = 3;
        bool endstep;
        bool test = false; // true для отладки/проверки расчётов и принятия значений классов по умолчанию, чтоб закомментить циклы while

        Console.WriteLine("Для получения результатов расчёта можно:");
        Console.WriteLine(" (1) ввести свои данные для расчёта;\n" + 
                          " (2) посмотреть работу программы на значениях по умолчанию.");
        Console.Write("\nВведите номер подходящего пункта с последующим нажатием на Enter: ");

        // Будем вводить данные или используем данные по умолчанию:
        bool defolt, a, b, c, d, e, f, g, h;
        defolt = a = b = c = d = e = f = g = h = false;
              
        int tempInt = 0; 
        string res = "";
#nullable disable
        while (res != "1" && res != "2")
        {
            tempInt++;
            res = Console.ReadLine().Trim();

            if (res == "1") defolt = false;            
            else if (res == "2") defolt = true;
            else Console.WriteLine("Введите корректный ответ (\"1\" или \"2\"): ");
        }
        if (tempInt > 2) Console.Write($"Поздравляем: у вас получилось выбрать корректный ответ всего лишь с {tempInt}-й попытки! Что ж, продолжим...\n");
#nullable enable


        // Заполнение данных, если оно активировано. Если нет - сразу получаем отчёты
        Console.Write("\n█ ШАГ {0}/{1}. ДАННЫЕ АВТОМОБИЛЯ\n", ++step, sumstep);
        Car bus = new();
        
        endstep = defolt;

        while (!endstep) 
        {
            if (!a) a = bus.SetName();
            if (!b) b = bus.SetGasWaste();
            if (!c) c = bus.SetNumOfPass();

            if (!a || !b || !c) Console.WriteLine("\n >> Ошибка: имеются недопустимые значения, измените ответы...");
            else
            {
                Console.WriteLine($"\nОТЧЁТ ({step})");
                bus.displayInfo();
                endstep = finishstep();
                if (!endstep) a = b = c = false;
            }                 
         }

        endstep = defolt;
        if(defolt) bus.displayInfo(); // если мы не попадали в цикл while и смотрим значения по умолчанию

        //Вытаскиваем деконструктором переменные из класса в цикл Main
        string name; decimal dGasWaste; int numOfPass;
        bus.Deconstruct(out name, out dGasWaste, out numOfPass); // или так - (carName, GasWaste, carNumOfPass) = bus;

        a = b = c = false;
        
        Console.Write("\n█ ШАГ {0}/{1}. ДАННЫЕ МАРШРУТА\n", ++step, sumstep);
        CityWay BusLine = new();

        while (!endstep)
        {
            if (!a) a = BusLine.SetNameWay();
            if (a && !b) b = BusLine.SetWayLength();
            if (b && !c) c = BusLine.SetNumDistrict();

            if (!a || !b || !c) Console.WriteLine("\n >> Ошибка: имеются недопустимые значения, измените ответы...");
            else
            {
                if (!d) d = BusLine.SetNumBusStop();
                if (d && !e) e = BusLine.SetMinMaxFirst(numOfPass);
                
                if (BusLine.GetSumBusStop() < 3) f = true; // при 2 остановках у нас никто не садится на второй остановке
                if (e && !f) f = BusLine.SetMinMaxSecond(numOfPass);
                if (!d || !e || !f) Console.WriteLine("\n >> Ошибка: имеются недопустимые значения, измените ответы...");
            }

            if (d & e & f)
            {
                Console.WriteLine($"\nОТЧЁТ ({step})");
                BusLine.displayInfo();
                endstep = finishstep();
                if (!endstep) a = b = c = false;
            }
        }

        endstep = defolt;
        if (defolt) BusLine.displayInfo(); // если мы не попадали в цикл while и смотрим значения по умолчанию

        BusLine.Deconstruct(out string sNameWay, out decimal dWayLength, out int iNumDistrict,
                out int iSumBusStop, out int iMinPasFirst, out int iMaxPasFirst, out int iMinPasSecond, out int iMaxPasSecond, out int[] iNumBusStop);
        a = b = c = d = e = f = false;

        Console.Write("\n█ ШАГ {0}/{1}. ЗАТРАТЫ ДЕНЕГ И ВРЕМЕНИ\n", ++step, sumstep);
        TimeMoney TMexpenses = new();
        TMexpenses.Deconstruct(out string sCurrency, out string sTypeOil, out decimal dOilPrice, out decimal dServicePrice, out decimal dPriceOneDist
                             , out decimal dPriceTwoDist, out int iNumExitWay, out int iDaysMonth);


        while (!endstep)
        {
            if (!a) a = TMexpenses.SetCurrency();
            if (a & !b) b = TMexpenses.SetTypeOil();
            if (b & !c) c = TMexpenses.SetOilPrice();
            if (c & !d) d = TMexpenses.SetServicePrice();
            if (d & !e) e = TMexpenses.SetPriceOneDist();

            if (e & !f && iNumDistrict > 1) f = TMexpenses.SetPriceTwoDist();
            TMexpenses.Deconstruct(out sCurrency, out sTypeOil, out dOilPrice, out dServicePrice, out dPriceOneDist
                                 , out dPriceTwoDist, out iNumExitWay, out iDaysMonth);
            if (dPriceOneDist > dPriceTwoDist) Console.WriteLine("\n>> Примечание: цена по району указана выше, чем между районами. Возможно, это ошибка.\n");

            if (e & !g) g = TMexpenses.SetNumExitWay();
            if (g & !h) h = TMexpenses.SetDaysMonth();

            if (h)
            {
                Console.WriteLine($"\nОТЧЁТ ({step})");
                TMexpenses.displayInfo(iNumDistrict);
                endstep = finishstep();
                if (!endstep) a = b = c = false;
            }
        }

        endstep = defolt;
        if (defolt) TMexpenses.displayInfo(iNumDistrict); // если мы не попадали в цикл while и смотрим значения по умолчанию

        TMexpenses.Deconstruct(out sCurrency, out sTypeOil, out dOilPrice, out dServicePrice, out dPriceOneDist
                             , out dPriceTwoDist, out iNumExitWay, out iDaysMonth);
        a = b = c = d = e = f = g = h = false;


        //РАСЧЁТЫ
        Console.WriteLine("\n█ ДАННЫЕ ПРИНЯТЫ");
        int inDistrict = 0; // кол-во пассажиров по району
        int outDistrict = 0; // кол-во пассажиров между районами

        int n = 3; // кол-во экспериментов
        Console.WriteLine("Используя рандомайзер, выполним моделирование маршрута. Количество моделей - " + n + ". Количество остановок - " + iSumBusStop);

        string QuaPas;
        string sInOutDistrict;

        for (int i = 1; i <= n; i++)
        {
            Console.Write($"\nМодель {i}. Данные по остановкам:");

            // получим в виде строки заполненность остановок потенциальными пассажирами:
            QuaPas = Calculation.QuantityPas(iSumBusStop, iMinPasFirst, iMaxPasFirst, BusLine.minPasSecond, BusLine.maxPasSecond);

            // Число пассажиров за маршрут по району и между ними в формате строки "число_ПО число_МЕЖДУ"
            sInOutDistrict = Calculation.Passangers(numOfPass, iNumDistrict, iSumBusStop, QuaPas, iNumBusStop);

            // Получим отсюда int значения пассажиров:
            int[] InOutDistrict = Calculation.TextInIntArray(sInOutDistrict);// это c Java всё это осталось...
            inDistrict += InOutDistrict[0]; // проехавшие по району
            outDistrict += InOutDistrict[1]; // проехавшие между районами

            Console.Write("\nПромежуточный итог: получено пассажиров по району: {0}, между районами: {1}\n", InOutDistrict[0], InOutDistrict[1]);
        }

        inDistrict = (int)(Math.Round((float)inDistrict / n));     // усреднённое кол-во пассажиров по району
        outDistrict = (int)(Math.Round((float)outDistrict / n));  //  усреднённое кол-во пассажиров между районами
        Console.Write("\nМОДЕЛИРОВАНИЕ ЗАВЕРШЕНО. В качестве окончательного результата моделирования будут приняты усреднённые значения расчётов каждой модели.\n");

        decimal dDistanceDay = Math.Round(dWayLength * iNumExitWay * 1000); //расстояние, пройденное за 1 рабочий день/смену (МЕТРЫ)
        decimal dDistanceMonth = iDaysMonth * dDistanceDay; // расстояние, пройденное за 1 месяц (МЕТРЫ)
        decimal dOilExpenses = (dDistanceMonth / 1000) * dGasWaste / 100; // расход топлива за месяц в литрах
        decimal dOilExpensMonth = dOilExpenses * dOilPrice * 100; // затраты на бензин, КОПЕЙКИ/мес.
        decimal dMoneyExpensMonth = dOilExpensMonth + dServicePrice * 100; // общие затраты (сервис+бензин), КОПЕЙКИ/мес.

        decimal dProfitOne = Math.Round((dPriceOneDist * inDistrict + dPriceTwoDist * outDistrict) * 100); // выручка за 1 выезд, КОПЕЙКИ
        decimal dProfitMonth = iNumExitWay * iDaysMonth * dProfitOne; // выручка за 1 месяц, КОПЕЙКИ
        int nds = 13; // ставка НДС
        decimal dClearProfitMonth = (dProfitMonth - dMoneyExpensMonth) * (1 - nds / 100M); // чистая прибыль в месяц с вычетом НДС, КОПЕЙКИ

        Console.WriteLine("\n█ РЕЗУЛЬТАТЫ РАСЧЁТА:");
        Console.Write("Средние значения по пассажирам: по району едут {0} чел., между районами - {1} чел.\n", inDistrict, outDistrict);
        //Console.Write("Количество пассажиров при выполнении маршрута по району: {0}, между районами: {1}\n", inDistrict, outDistrict);
        Console.Write("Выручка за выполнение одного такого маршрута: {0:.##} {1}. \n", dProfitOne / 100, sCurrency);
        Console.Write("При заданном количестве выездов за день ({0}) и количестве смен в месяц ({1}) выручка за месяц составит {2:.##} {3} \n", iNumExitWay, iDaysMonth, dProfitMonth / 100, sCurrency);
        Console.Write("Пробег за месяц составит {0:.##} км. Затраты топлива: {1:.##} литров стоимостью {2:.##} {3}\n", dDistanceMonth / 1000, dOilExpenses, dOilExpensMonth / 100, sCurrency);
        Console.Write("С учётом сервисных затрат ({0:.##} {1}), общие ежемесячные затраты составили {2:.##} {1}\n", dServicePrice, sCurrency, dMoneyExpensMonth / 100);

        string sPercent = nds + "%";

        Console.Write("ЧИСТАЯ ПРИБЫЛЬ, после вычета НДС ({0}) и затрат на топливо/сервис ТС, составила {1:.##} {2}/месяц\n", sPercent, dClearProfitMonth / 100, sCurrency);
        Console.Write("Налог НДС ({0}) составил {1:.##} {2}\n", sPercent, (dProfitMonth - dMoneyExpensMonth) * (nds / 100M) / 100, sCurrency);

    }

    static bool finishstep()
        {
            Console.WriteLine("\nДля редактирования данных Отчёта нажмите \"2\", для перехода к следующему шагу нажмите сразу Enter: ");
#nullable disable
        bool res = true;
            string a = Console.ReadLine();

            if (a == "2")
            {
                res = false;
                Console.WriteLine("Редактирование:");
            }
#nullable enable
        return res;
        }
    }










