namespace ItemSync2.Core
{
    class Item
    {
        public int ID;
        public int ClassID;
        public int SubclassID;
        public int SoundOverrideSubclassID;
        public int Material;
        public int DisplayID;
        public int InventoryType;
        public int SheatheType;

        public Item()
        {

        }

        public Item(int ID, int ClassID, int SubclassID, int SoundOverrideSubclassID, int Material, int DisplayID, int InventoryType, int SheatheType)
        {
            this.ID = ID;
            this.ClassID = ClassID;
            this.SubclassID = SubclassID;
            this.SoundOverrideSubclassID = SoundOverrideSubclassID;
            this.Material = Material;
            this.DisplayID = DisplayID;
            this.InventoryType = InventoryType;
            this.SheatheType = SheatheType;
        }
    }
}
