    internal class Program
    {

        static void Main(string[] args)
        {
            Thread[] TwoThreads = new Thread[2];
            bool[] bools = new bool[2];
            int[] arr = new int[6] { -1, 0, 3, 5, 9, 12 };
            int res = BinSearch(arr, 9);
            Console.WriteLine(res);
        }
        static int BinSearch(int[] arr,int find) {
            int res = -1;
            int low=0,high=arr.Length;
            while (low<high) {
                int mid = (int)Math.Floor((double)(low+(high-low)/2));
                if (arr[mid] == find)
                {
                    res= mid;
                    break;
                }

                if (arr[mid]<find)
                    low = mid;
                if (arr[mid]>find)
                    high = mid;
            }
            return res;
        }
    }
        static int BinSearchReq(int[] arr,int low,int high,int target) {
            int mid = low + (int)Math.Floor((double)((high - low) / 2));
            if (arr[mid] == target)
                return low;
            if (arr[mid] > target)
                high = mid-1;
            if (arr[mid] < target)
                low = mid+1;
            if (low < high)
                return BinSearchReq(arr, low, high, target);
            else return -1;
        }
