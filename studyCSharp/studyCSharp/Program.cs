using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace studyCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("helloWord!");
            Console.WriteLine();
            User u = new User("张三",45);
            Console.Write(u);

            u.getAge();

            User u1 = new Muser("赵小二", 7);
            u1.getAge();
        }
    }


    public class User
    {
        protected string name;
        protected int age;
        public User(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public override string ToString()
        {
            return this.name + this.age;
        }

        public virtual void getAge()
        {
            Console.Write(this.age);
        }
    }

    public class Muser : User {

        public Muser(string name, int age) : base(name, age)
        {

        }

        public override void getAge()
        {
            Console.Write("多态的实现在c#中");
        }
    }

}
