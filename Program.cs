namespace cryptopalschallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO - arg check
            //TODO - split by challenges


            Challenge01 c = new Challenge01();
            c.DoChallenge01(args[0]);


        }

    }
}
