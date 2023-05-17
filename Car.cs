using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


   
    public class Car
    {          

        string name; //Газель, МАЗ, Богдан - потом опробовать формировать текстовый файл с отчётом..
        decimal gasWaste; //Расход безнзина на 100 км, затем стоимость еще нужно вводить будет
        int numOfPass; // Максимальное кол-во пассажиров в авто

        private int idCar; // Идентификатор записи со счётчиком записей ниже для пробы с SQL...
        private static int counter = 0;



        public void displayInfo()
        {
            Console.WriteLine("Автомобиль - {0}, расход - {1} л/100 км, кол-во пассажиров - {2} чел. (ID записи - {3}) \n", name, gasWaste, numOfPass, idCar);
        }

    // Деконструктор вытаскивает значения переменных объекта класса https://metanit.com/sharp/tutorial/3.35.php
        public void Deconstruct(out string carName, out decimal carGasWaste, out int carNumOfPass)
        {
            carName = name;
            carGasWaste = gasWaste;
            carNumOfPass = numOfPass;
        }

        public int GetNumOfPas() { return numOfPass; } // в С# есть деконструкторы, использую их, поэтому я этим методом пользоваться не буду


    //Конструкторы
        public Car(/*string name = "Undefined", decimal gasWaste = 18, int numOfPass = 15*/)
        {
            //this.name = name; // закомментировал, чтобы была возможность только через сеттеры задавать другие значения
            //this.gasWaste = gasWaste;
            //this.numOfPass = numOfPass;

            this.name = "GAZelle";
            this.gasWaste = 18;
            this.numOfPass = 15;
            counter++;
            idCar = counter; // почему так с лишней переменной, а не ++id? ответ ниже 
        
            /*потому что статическая переменная - общая переменная для всех объектов классов. Это состояние класса в целом, а идентификатор
            объкта - это часть состояния отдельного объекта класса. Состояние класса и состояние объекта - это разые вещи. И если
            к примеру, сделать id статической, тогда это будет одно и тоже значение для всего класса*/
        }
        
        
        public Car(bool defolt) : this()//так делаем вызов конструктора по умолчанию в случае TRUE 
    {
        if (!defolt) {

            {   //область видимости, в которой можно сделать недоступные снаружи вещи
                //https://metanit.com/sharp/tutorial/2.18.php
            }

            // какие-то условия, проверки... И после этого присваиваем, например: 
            this.name = "noname";
            this.gasWaste = 8;
            this.numOfPass = 4;          
        }
    }// А этот конструктор оставляю, так сказать, для примера пройденного материала... Использовать не буду

    // Сеттеры
        public bool SetCar() 
    {
        

        {   //область видимости, в которой можно сделать недоступные снаружи вещи
            //https://metanit.com/sharp/tutorial/2.18.php
        }

        bool a = false, b = false, c = false;

        string fieldGas, fieldPass;
        fieldGas = fieldPass = name = "0";
        numOfPass = 0;
        gasWaste = 0;
        bool errCheck = false;

        while (a == false || b == false || c == false)
        {
            errCheck = false;

            if (a == false)
            {
                Console.Write("Тип авто (марка)- ");
                name = Console.ReadLine();
            }


            if (b == false)
            {
                Console.Write("Расход топлива (л/100 км) - ");
                fieldGas = Console.ReadLine().Trim().Replace(".", ",");
            }

            if (c == false)
            {
                Console.Write("Вместимость по пассажирам (чел.) - ");
                fieldPass = Console.ReadLine();
            }

            try
            {
                if (name != "" && name.Length < 80) a = true;
                else Console.WriteLine("Ошибка: измените тип автомобиля");

                gasWaste = Convert.ToDecimal(fieldGas);
                if (gasWaste > 0.1M && gasWaste < 1000) b = true;
                else Console.WriteLine("Ошибка: некорректное значение расхода");
            }
            catch (Exception) { errCheck = true; }

            try
            {
                numOfPass = Convert.ToInt32(fieldPass);
                if (numOfPass >= 1 && numOfPass < 10_000) c = true;
                else Console.WriteLine("Ошибка: некорректное значение кол-ва пассажиров");
            }

            catch (Exception) { errCheck = true; }

            if (errCheck == true) { Console.WriteLine("Ошибка принятия значения. Необходим повторный ввод."); }
        }
        Console.WriteLine("\nПараметры приняты.");

        this.name = name;
        this.gasWaste = gasWaste;
        this.numOfPass = numOfPass;

        return true;


    } // Сеттер для всего класса. Также не нужно использовать!
        public bool SetName() 
        {
            bool result = false;
            string answer;
            Console.Write("Тип авто (марка)- ");
            answer = Console.ReadLine();

            if (answer != "" && answer.Length < 30)
            {
                name = answer;
                result = true;
            } 
            return result; 
        }
        public bool SetGasWaste() 
        {
            bool result = false;
            decimal answer;

            Console.Write("Расход топлива (л/100 км) - ");
            try
            {
                answer = Convert.ToDecimal(Console.ReadLine().Trim().Replace(".", ","));
                if (answer > 0 && answer < 10000)
                {
                    gasWaste = answer;
                    result = true;
                }
            }
            catch (Exception) { }          
                    
            return result;
        }
        public bool SetNumOfPass()
        {
            bool result = false;
            int answer;

            Console.Write("Вместимость по пассажирам (чел.) - ");
            try
            {
                answer = Convert.ToInt32(Console.ReadLine().Trim());
                if (answer > 0 && answer < 10_000)
                {
                    numOfPass = answer;
                    result = true;
                }
            }
            catch (Exception) { }

            return result;
        }

}
