/* Create an application that sorts or reverses (methods that take an array and returns an array) an array according to the choice
of the user. Use a delegate to solve this problem. Decide the method to be invoked (sort or reverse) based on the choice of
the user and invoke the delegate */
namespace Q2_AssignmentOneDelegatesandEvents
{
    // 1. Defining Delegate
    public delegate int[] SortReverseDelegate(int[] array);

    public class Program
    {
        static void Main(string[] args)
        {
            Program objprogram = new Program();

            // 2. Instantiating the delegate
            SortReverseDelegate sortDelegate = new SortReverseDelegate(objprogram.GetSort);
            SortReverseDelegate reverseDelegate = new SortReverseDelegate(objprogram.GetReverse);

            int[] array = GetArrayFromUser();

            Console.WriteLine("Enter your choice: 1 for Sort, 2 for Reverse");
            int choice = int.Parse(Console.ReadLine());

            int[] result;
            switch (choice)
            {
                case 1:
                    result = sortDelegate.Invoke(array);
                    Console.WriteLine("Sorted Array:");
                    break;
                case 2:
                    result = reverseDelegate.Invoke(array);
                    Console.WriteLine("Reversed Array:");
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    return;
            }

            // Display the result
            foreach (int item in result)
            {
                Console.Write(item + " ");
            }
            Console.ReadKey();
        }

        // Method to get the array from the user
        static int[] GetArrayFromUser()
        {
            Console.WriteLine("Enter the limit of the array:");
            int limit = int.Parse(Console.ReadLine());
            int[] array = new int[limit];

            Console.WriteLine("Enter the array elements:");
            for (int i = 0; i < limit; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }

            return array;
        }

        // Method to sort the array
        public int[] GetSort(int[] array)
        {
            Array.Sort(array);
            return array;
        }

        // Method to reverse the array
        public int[] GetReverse(int[] array)
        {
            Array.Reverse(array);
            return array;
        }
    }
}
