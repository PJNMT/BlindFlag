namespace Game
{
    public abstract class Place
    {
        private int Next_Place; 
        /* nb d'evenements avant la prochaine place */

        public int GetNextPlace()
        {
            return Next_Place;
        }

    }
}