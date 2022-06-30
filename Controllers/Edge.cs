namespace test1.Controllers
{
    public class Edge
    {
        public string from { get; set; }
        public string to { get; set; }
        public int weight { get; set; }

        public Edge(string _from, string _to, int _weight)
        {
            from = _from;
            to = _to;
            weight = _weight;
        }
    }
}