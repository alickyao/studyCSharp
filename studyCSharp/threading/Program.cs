using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// 线程学习
/// </summary>
namespace threading
{
    class Program
    {
        static internal Thread[] threads = new Thread[10];

        static void Main(string[] args)
        {
            //线程启动
            //ThreadingText t = new ThreadingText();
            //Thread myThread = new Thread(new ThreadStart(t.beta));
            //myThread.Start();
            //if (myThread.IsAlive) {
            //    Thread.Sleep(10);
            //    Console.WriteLine(myThread.ThreadState.ToString());
            //    myThread.Abort();
            //    Console.WriteLine(myThread.ThreadState.ToString());
            //    myThread.Join();
            //    Console.WriteLine(myThread.ThreadState.ToString());
            //    Console.WriteLine();
            //    Console.WriteLine("Alpha.Beta has finished");
            //    float i = 54564543543543545341.1f;
            //    int s = (int)i;
            //    Console.WriteLine(s);
            //    Console.ReadLine();

            //}

            //余额
            //Balance b = new Balance();
            //for (int i = 0; i < 10; i++) {
            //    Thread s = new Thread(new ThreadStart(b.doS));
            //    threads[i] = s;
            //    threads[i].Name = i.ToString()+"号线程";
            //    threads[i].Start();
            //}
            //Console.ReadLine();

            //生产者与消费者
            //Cell c = new Cell();
            //GetCell getc = new GetCell(c, 20);
            //SetCell setc = new SetCell(c, 20);
            //Thread g = new Thread(new ThreadStart(getc.doGetCell));
            //Thread s = new Thread(new ThreadStart(setc.doSetCell));
            //s.Start();
            //g.Start();
            //Console.ReadLine();

            //定时器
            //TimerCallback callback = new TimerCallback(checkStatus);
            //TimerTest s = new TimerTest();
            //Timer tim = new Timer(checkStatus, s, 0, 1000);
            //s.tim = tim;
            //while (s.tim != null) { }
            //Console.WriteLine("Timer example done.");
            //Console.ReadLine();

            //互斥对象
            MutexTest.test();
        }

        /// <summary>
        /// 定时器执行
        /// </summary>
        /// <param name="state"></param>
        public static void checkStatus(object state) {
            TimerTest s = (TimerTest)state;
            s.counter++;
            if (s.counter < 5)
            {
                Console.WriteLine("定时器执行中" + s.counter);
            }
            else {
                s.tim.Dispose();
                Console.WriteLine("执行完毕");
            }
        }
    }

    /// <summary>
    /// 线程的启动与停止
    /// </summary>
    public class ThreadingText {
        public void beta()
        {
            while (true)
            {
                Console.WriteLine("ThreadingText.beta 运行中");
            }
        }
    }

    /// <summary>
    /// 余额与扣除余额
    /// </summary>
    public class Balance {

        /// <summary>
        /// 当前余额
        /// </summary>
        public static decimal b  = 5000;
        public static Random r = new Random();

        static Balance() {

        }

        /// <summary>
        /// 扣除/增加余额
        /// </summary>
        /// <param name="amout"></param>
        public void doB(decimal amout) {
            lock (this) {
                //扣除余额
                Console.WriteLine("Current Thread:" + Thread.CurrentThread.Name);
                Console.WriteLine(b.ToString() + "-" + amout.ToString());
                if (b > amout)
                {
                    Thread.Sleep(100);
                    b = b - amout;
                    Console.WriteLine("当前余额：" + b.ToString());
                }
                else {
                    Console.WriteLine("当前余额：" + b.ToString() + "，余额不足");
                }
            }
        }

        public void doS() {
            Console.WriteLine(Thread.CurrentThread.Name + "号线程启动");
            for (int i = 0; i < 10; i++) {
                doB(r.Next(-50, 100));
            }
            Console.WriteLine(Thread.CurrentThread.Name + "号线程启动结束");
        }
    }

    /// <summary>
    /// 生产者与消费者
    /// </summary>
    public class Cell {
        public int content;
        /// <summary>
        /// 当前是否可拿出
        /// </summary>
        public bool readFlag = false;

        /// <summary>
        /// 取出
        /// </summary>
        public void get() {
            lock (this) {
                if (!readFlag) {
                    Monitor.Wait(this);
                }
                Console.WriteLine("取出{0}", this.content);
                this.readFlag = false;
                Monitor.Pulse(this);
            }
        }

        /// <summary>
        /// 放置
        /// </summary>
        public void set(int c) {
            lock (this) {
                if (readFlag) {
                    Monitor.Wait(this);
                }
                this.content = c;
                Console.WriteLine("放入{0}", this.content);
                this.readFlag = true;
                Monitor.Pulse(this);
            }
        }
    }

    public class GetCell {
        Cell c;
        int count;
        public GetCell(Cell box, int max) {
            this.c = box;
            this.count = max;
        }

        public void doGetCell() {
            for (int i = 0; i < count; i++)
            {
                c.get();
            }
        }
    }

    public class SetCell {
        Cell c;
        int count;
        public SetCell(Cell box, int max)
        {
            this.c = box;
            this.count = max;
        }

        public void doSetCell()
        {
            for (int i = 0; i < count; i++)
            {
                c.set(i);
            }
        }
    }

    /// <summary>
    /// 定时器
    /// </summary>
    public class TimerTest {
        public int counter;
        public Timer tim;
    }
}
