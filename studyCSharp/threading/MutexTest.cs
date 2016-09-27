using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threading
{
    /// <summary>
    /// 互斥对象
    /// </summary>
    class MutexTest
    {

        static Mutex gM1;
        static Mutex gM2;
        const int ITERS = 100;
        static AutoResetEvent Event1 = new AutoResetEvent(false);
        static AutoResetEvent Event2 = new AutoResetEvent(false);
        static AutoResetEvent Event3 = new AutoResetEvent(false);
        static AutoResetEvent Event4 = new AutoResetEvent(false);


        public static void test() {
            Console.WriteLine("开始");


            //创建一个Mutex对象，并且命名为MyMutex
            gM1 = new Mutex(true, "MyMutex");
            //创建一个未命名的Mutex 对象.
            gM2 = new Mutex(true);

            Console.WriteLine(" - Main Owns gM1 and gM2");

            AutoResetEvent[] evs = new AutoResetEvent[4];
            evs[0] = Event1; //为后面的线程t1,t2,t3,t4定义AutoResetEvent对象
            evs[1] = Event2;
            evs[2] = Event3;
            evs[3] = Event4;
            MutexTest t = new MutexTest();

            Thread t1 = new Thread(new ThreadStart(t.t1Start));
            Thread t2 = new Thread(new ThreadStart(t.t2Start));
            Thread t3 = new Thread(new ThreadStart(t.t3Start));
            Thread t4 = new Thread(new ThreadStart(t.t4Start));
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            Thread.Sleep(2000);
            Console.WriteLine("主线程释放gm1 与 gm2");
            gM1.ReleaseMutex(); //线程t2,t3结束条件满足
            gM2.ReleaseMutex(); //线程t1,t4结束条件满足
            

            //等待所有四个线程结束
            WaitHandle.WaitAll(evs);
            Console.WriteLine(" Mutex Sample");
            Console.ReadLine();
        }

        public void t1Start() {
            Console.WriteLine("线程1开始执行，等待gm1与gm2释放信号");
            Mutex[] gMs = new Mutex[2];
            gMs[0] = gM1;//创建一个Mutex数组作为Mutex.WaitAll()方法的参数
            gMs[1] = gM2;
            Mutex.WaitAll(gMs);//等待gM1和gM2都被释放
            Console.WriteLine("线程1收到gm1与gm2释放信号，开始执行");
            Thread.Sleep(2000);
            Console.WriteLine("线程1执行完毕");
            gM1.ReleaseMutex();
            gM2.ReleaseMutex();
            Event1.Set(); //线程结束，将Event1设置为有信号状态
        }

        public void t2Start()
        {
            Console.WriteLine("线程2开始执行，等待gm1释放信息");
            gM1.WaitOne();//等待gM1的释放
            Console.WriteLine("线程2收到gm1释放信号，执行完毕");
            gM1.ReleaseMutex();
            Event2.Set();//线程结束，将Event2设置为有信号状态
        }

        public void t3Start()
        {
            Console.WriteLine("线程3开始执行，等待gm1或者gm2释放信息");
            Mutex[] gMs = new Mutex[2];
            gMs[0] = gM1;//创建一个Mutex数组作为Mutex.WaitAny()方法的参数
            gMs[1] = gM2;
            int s = WaitHandle.WaitAny(gMs);//等待数组中任意一个Mutex对象被释放
            Console.WriteLine("线程3收到信息，执行完毕");
            gMs[s].ReleaseMutex();
            Event3.Set();//线程结束，将Event3设置为有信号状态
        }

        public void t4Start()
        {
            Console.WriteLine("线程4开始执行，等待gm1释放");
            gM1.WaitOne();//等待gM2被释放
            Console.WriteLine("线程4收到信号，执行完毕");
            Thread.Sleep(1000);
            gM1.ReleaseMutex();
            Console.WriteLine("线程4执行完毕");
            Event4.Set();//线程结束，将Event4设置为有信号状态
        }
    }
}
