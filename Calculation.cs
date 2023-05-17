//namespace Calc;

class Calculation
    {
        // Расчёт пассажиров: сколько всего пассажиров, сколько из них едут по району, а сколько между районами. Получение таблицы с данными по каждой остановке.
        public static string Passangers(int numOfPass, int iNumDistrict, int iSumBusStop, string QuaPas, int[] NumBusStop)
        {
            int inDistrict = 0; int outDistrict = 0; // количество пассажиров по/между районами

            // ТЕПЕРЬ ДЕЛАЮ 3 МАССИВА, ХАРАКТЕРИЗУЮЩИХ КАЖДУЮ ОСТАНОВКУ (или можно было бы сделать 3-мерный один массив, но так мне удобнее):

            int[] BusStopPlus = TextInIntArray(QuaPas); // здесь мы видим, сколько каждая остановка даёт пассажиров
            int[] BusStopMinus = new int[iSumBusStop]; //  здесь мы будем видеть, сколько пассажиров выйдет на каждой остановке
            int[] NumberDisrtrict = new int[iSumBusStop]; // здесь мы видим, к какому району относится каждая остановка

            for (int i = 0; i < BusStopMinus.Length; i++) BusStopMinus[i] = 0; // пока что никто нигде не выходит

            //Определим NumberDisrtrict[] 
            int k = 0; // добавочное число к номерам остановок в цикле ниже
            for (int i = 0; i < iNumDistrict; i++)
            {
                if (i > 0) k = k + NumBusStop[i - 1];
                for (int j = 0; j < NumBusStop[i]; j++)
                {
                    NumberDisrtrict[j+k] = i + 1;
                }
            }

            int capacity = 0; // пассажиров в ТС в настоящий момент
            int oldCapacity; // предыдущее значение пассажиров
            int x = 1;

            // int[] BusStopMinus определяется по ходу "движения" нашего ТС. "Поехали!" :
            for (int bstop = 0; bstop < iSumBusStop; bstop++)
            {
                capacity -= BusStopMinus[bstop]; // выходят
                oldCapacity = capacity;
                capacity += BusStopPlus[bstop]; // заходят
                if (capacity > numOfPass) capacity = numOfPass; // нельзя больше пассажироёмкости ТС
                for (int i = 0; i < capacity - oldCapacity; i++)
                {
                    x = getRandomInt(bstop + 1, iSumBusStop - 1, 0, false);
                    BusStopMinus[x] += 1; // Для каждого входящего пассажира определяется рандомный номер остановки выхода. Эти номера сформируют int[] BusStopMinus
                    if (NumberDisrtrict[bstop] == NumberDisrtrict[x]) inDistrict += 1;
                    else outDistrict += 1;
                }
            }

            // Напечатаем таблицы:
            Console.Write("\nНомер района:        ");
            foreach (int item in NumberDisrtrict) Console.Write(item + " ");
            Console.Write("\nВходящие пассажиры:  ");
            foreach (int item in BusStopPlus) Console.Write(item + " ");
            Console.Write("\nВыходящие пассажиры: ");
            foreach (int item in BusStopMinus) Console.Write(item + " ");

            return inDistrict + " " + outDistrict; // передаём в формате строки "по_между"
        }

        public static int[] TextInIntArray(string TextArray)
        {
            string[] words = TextArray.Split(" ");
            int[] intArray = new int[words.Length];
            for (int i = 0; i < words.Length; i++) intArray[i] = Convert.ToInt32(words[i]);
            return intArray;
        }

        // Получаем в строках массивы чисел для того, чтобы передать их в методы...
        // Делаем массив потенциальных пассажиров для каждой остановки:
        public static string QuantityPas(int iSumBusStop, int iMinPasFirst, int iMaxPasFirst, int iMinPasSecond, int iMaxPasSecond)
        {
            int[] peopleBusStop = new int[iSumBusStop];
            string res = "";
            for (int i = 0; i < iSumBusStop - 1; i++)
            {
                if (i > 0) peopleBusStop[i] = getRandomInt(iMinPasSecond, iMaxPasSecond, peopleBusStop[i - 1], true);
                else peopleBusStop[i] = getRandomInt(iMinPasFirst, iMaxPasFirst, 0, false);
                res += Convert.ToString(peopleBusStop[i]) + " ";
            }
            res += "0";
            return res.Trim();
        }
        // Делаем строку из массива количества остановок в каждом районе:
        public static string NumBusInDistricts(int iNumDistrict, int[] NumBusStop)
        {
            string res = "";
            for (int i = 0; i < iNumDistrict; i++)
            {
                res += NumBusStop[i] + " ";
            }
            return res.Trim();
        }

        // Получение случайного int числа из диапазона [min; max] или, если точнее, [min; max+1). Рандомайзер.
        static int getRandomInt(int min, int max, int past, bool dif)
        {            
            Random rnd = new Random();
            int result = rnd.Next(min, max);
            // При переводе в int Рандомайзер часто может выдать одно и то же число в одном диапазоне. Если точно нужны разные, то передаём в dif
            // значение true и входим сюда:
            while (dif && result == past)
            {
                result = rnd.Next(min, max);
            }
            return result;
        }
 }
