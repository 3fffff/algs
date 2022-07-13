namespace Threads
{
    class Interlock {
        private static int usingResource = 0;
        private const int numThreadIters = 5;
        private const int numThreads = 10;
        static void Main() {
            Thread thr;
            Random rnd = new Random();
            for (int i = 0; i < numThreads; i++) {
                thr = new Thread(new ThreadStart(ThreadProc));
                thr.Name = String.Format("Thread{0}", i + 1);
                Thread.Sleep(rnd.Next(0,1000));
                thr.Start();
            }
        }
        private static void ThreadProc() {
            for (int i = 0; i < numThreadIters; i++) {
                UseResource();
                Thread.Sleep(1000);
            }
        }
        static bool UseResource()
        {
            if (Interlocked.Exchange(ref usingResource, 1) == 0)
            {
                Console.WriteLine("{0} acquired existing lock", Thread.CurrentThread.Name);
                //simulate
                Thread.Sleep(500);
                Console.WriteLine("{0} existing lock", Thread.CurrentThread.Name);
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else {
                Console.WriteLine("{0} was denied by lock",Thread.CurrentThread.Name);
                return false;
            }
        }
    }
   
