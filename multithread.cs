#include<iostream>
#include<fstream>
using namespace std;
struct listNode {
    char data;
    listNode* nextPtr;
};

typedef struct listNode ListNode;
typedef ListNode* ListNodePtr;
float reqroot(float x, int num) {
    if (num == 0)
        return std::sqrt(1 + x);
    return std::sqrt(1+x*reqroot(x, num--));
}
int main() {
#ifdef CF
    std::istream& is = std::cin;
    std::ostream& os = std::cout;
#else
    std::ifstream isf( "input.txt" );
    std::ofstream os( "output.txt" );
#endif
    int T, rooms, a;
    for (cin >> T; T > 0; T--) {
        long long v = 0, ans = 0;
        for (cin >> rooms; rooms > 1; rooms--) {
            cin >> a;
            v |= a;
            int temp = (!a && v);
            ans += a + (!a && v);
        }
        cin >> a;
        cout << ans << endl;
    }
    float res = reqroot(15, 15);
    cout << res << endl;
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithr
{
    class ReorderTest {
        private  int _a = 1;
        public void Foo() {
            var task = new Task(Bar);
            task.Start();
            Thread.Sleep(1000);
            _a = 0;
            task.Wait();
        }
        public void Bar() {
            Console.WriteLine(_a);
            //Thread.MemoryBarrier();
            _a = 1;
            while (_a == 1) {
                Console.WriteLine(_a);
            }
        }
    }
    interface ILock {
        void Request(int pid);
        void Release(int pid);
    }
    class NoLock : ILock {
        public void Request(int pid) { }
        public void Release(int pid) { }
    }
    class StandardLock : ILock
    {
        private object _sync = new object();
        public void Release(int pid)
        {
            Monitor.Enter(_sync);
        }

        public void Request(int pid)
        {
            Monitor.Exit(_sync);
        }
    }

    class PetersonLock : ILock {
        private volatile bool[] _wantCs = new bool[2];
        private volatile int _turn;

        public void Release(int pid)
        {
            _wantCs[pid] = false;
        }

        public void Request(int pid)
        {
            int j = pid == 1 ? 0 : 1;
            _wantCs[pid] = true;
            Thread.MemoryBarrier();
            _turn = j;
            while (_wantCs[j] && _turn == j)
                Thread.Sleep(0);
        }
    }
    class Program
    {
        private static volatile int _i = 0;
        static void Main(string[] args)
        {
            var ReorderTest = new ReorderTest();
            ReorderTest.Foo();
            for (int i = 0; i < 1000; i++)
                RunTest();
            Console.Read();
        }
        static void RunTest() {
            _i = 0;
            var lockType = new PetersonLock();
            var t1 = new Thread(() => Inc(0, lockType));
            var t2 = new Thread(()=>Inc(0,lockType));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine(_i);
        }
        static void Inc(int pid, ILock lockType) {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    lockType.Request(pid);
                    _i++;
                    lockType.Release(pid);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}
