/*Create a counter that indicates rise or fall of temperature. 
 * If the temperature goes beyond 100 or below 0 then raise an
event which gives the message “Critical temperature reached”.
 */
namespace Q3_AssignmentOneDelegatesandEvents
{
    //1.Defining Delegate
    public delegate void RiseFallDelegate(int temp);
    public class Program
    {
        //2.Define Event
        event RiseFallDelegate risefallEvent;
        static void Main(string[] args)
        {
            Program objprogram= new Program();
            Console.WriteLine("Enter the temperature");
            int temp = int.Parse(Console.ReadLine());
            //3.Subscribing to the event
            objprogram.risefallEvent += objprogram.CriticalTemp;
            //Printing output
            Console.WriteLine("Event is raising.....");
            objprogram.risefallEvent(temp);
            Console.ReadKey();

        }
        //Get Temperature
        public void CriticalTemp(int temp)
        {
            if (temp < 0||temp > 100)
            {
                Console.WriteLine("Critical Temperature Reached");
            }
            else
            {
                Console.WriteLine("Normal Temperature");
            }
        }
        
    }
}