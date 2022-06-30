namespace test1.Controllers
{
   public class node
    {
        public int id { get; set; }
        public string label { get; set; }
        public int m { get; set; }
        public node(int _id, string _label)
        {
            label = _label;
            id = _id;
        }
    }

}