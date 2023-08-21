using System;

namespace yawNoise
{
    public class LabyrinthNoise
    {
        #region Fields
        public int Seed { get; private set; }
        WhiteNoiseInt innerNoise;
        bool hasAloneRooms;
        float doorChance;
        #endregion

        #region Constructors
        public LabyrinthNoise()
        {
            Seed = new Random().Next();
            innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
            doorChance = 0.5f;
        }
        public LabyrinthNoise(int seed)
        {
            Seed = seed;
            innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
            doorChance = 0.5f;
            hasAloneRooms = false;
        }
        public LabyrinthNoise(float DoorChance)
        {
            Seed = new Random().Next();
            innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
            doorChance = DoorChance;
            hasAloneRooms = false;
        }
        public LabyrinthNoise(int seed, float DoorChance, bool HasAloneRooms)
        {
            Seed = seed;
            innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
            doorChance = DoorChance;
            hasAloneRooms = HasAloneRooms;
        }
        #endregion

        #region Functions
        public bool IsWall(int x, int y)
        {
            //return x % 2 != y % 2 ? innerNoise[x, y] >= doorChance : x % 2 == 1 ? true : IsWall(x + 1, y) && IsWall(x - 1, y) && IsWall(x, y + 1) && IsWall(x, y - 1);
            if (x % 2 != y % 2)
            {
                //Normal
                return innerNoise[x, y] >= doorChance;
                /*if (x % 2 == 1)
                    return innerNoise[x / 2, y / 2] >= doorChance;
                else
                    return innerNoise[x / 2, y / 2] < doorChance;*/

            }
            else
            {
                if (x % 2 == 1)
                    return true;
                else if (!hasAloneRooms)
                    return IsWall(x + 1, y) && IsWall(x - 1, y) && IsWall(x, y + 1) && IsWall(x, y - 1);
                else
                    return false;
            }
        }
        #endregion

        #region Indexers
        public bool this[int x, int y] { get => IsWall(x, y); }
        #endregion
    }
}
