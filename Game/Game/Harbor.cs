namespace Game
{
    public class Harbour : Place
    {
        protected bool Got_Bar { get; }
        protected bool Got_ShipShop{ get; }
        protected bool Got_OldBudy { get; }

        protected Bar bar { get; }
        protected Shipshop SShop { get; }
        protected OlbTimer OldBudy { get; }
        
        protected enum Choice_Arrival
        {
            None,
            Go_To_Bar, // = equivalent vocal
            Go_To_The_ShipShop, // idem
            Share_The_Loot, // idem
            Departure, // idem
        
        }

        public Harbour( bool bar = true, bool Shop = true, bool OldB = true)
        {
            Got_Bar = bar;
            Got_ShipShop = Shop;
            Got_OldBudy = OldB;

            if (Got_Bar)
            {
                bar = new Bar();
            }

            if (Got_OldBudy)
            {
                OldBudy = new OldTimer();
            }

            if (Got_ShipShop)
            {
                SShop = new ShipShop();
                
            }
            
        }
        
        
    }
}