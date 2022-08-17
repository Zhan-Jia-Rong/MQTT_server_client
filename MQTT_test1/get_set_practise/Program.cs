using System;

namespace get_set_practise
{
    class subclass
    {
        public string name;
        //subclass class被實例化時，會立即執行建構子內容，並且可以傳入參數       
        public string nameGet
        {
            get
            {
                if (name != "")
                    return name;
                else
                    return "default";
            }
            set { Console.WriteLine("NameGet : {0}", value); }
        }
        /*public override string ToString() // 覆寫 subclass.ToString()
        {
            return "override string ToString = " + name;
        }*/
    }
    class Progam
    {
        static void Main()
        {
            subclass e = new subclass();            
            e.name = "J8";
            Console.WriteLine("覆寫 e.Tostring : {0}", e);
            e.nameGet = "Brayn";
            Console.WriteLine("NameGet ToString => {0}", e.nameGet);
            Console.ReadKey();
        }
    }
}
