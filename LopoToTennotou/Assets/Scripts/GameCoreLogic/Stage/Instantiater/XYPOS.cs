namespace GameCore
{
    struct XYPOS
    {
        public byte x;
        public byte y;
        public int mapScale;

        public XYPOS(byte x, byte y, int mapScale)
        {
            this.x = x;
            this.y = y;
            this.mapScale = mapScale;
        }
    }
}
