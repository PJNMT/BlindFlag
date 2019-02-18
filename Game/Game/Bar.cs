using System;

namespace Game
{
    public class Bar
    {
        private Random r = new Random();
        protected int peoples { get; set; } 
        protected enum Choose_Action
        {
            None,
            Play_Simon,
            Pay_Out,
            I_am_drinking_every_day
        }

        public Bar()
        {
            peoples = r.Next(20) + 10;
        }
    }
}