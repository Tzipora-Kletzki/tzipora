using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchApiController : ControllerBase
    {
       public  class  retValus {
            public List<node> nodes{ get; set; }
            public List<Edge> edgesL { get; set; }
            
                }
        public static List<node> nodes;
        public static Dictionary<string, string> prop;

        [HttpGet]
        [Route("getAll")]
        public Dictionary<string,string> get()
        {
            prop = new Dictionary<string, string>();
            Main();
            return prop;
        }

        [HttpGet]

        public retValus get(string word)
        {
             Search(word);
            retValus ret = new retValus();
            ret.nodes = nodes;
            ret.edgesL = Edges;
            return ret;
        }

     


      static  List<Edge> Edges;
         List<Edge> EdgesNo;
         int edges;
         void Main()
        {

            WeightedNetwork();// שליפת הקשתות לרשימה ושמירת משקלים משימה 1
            getNodes();//שליפת הקודקודים
            getEdges();//שליפת מספר הקשתות
            getAaverageDegree();//חישוב דרגה ממוצעת
            getAaverageWeightedDegree();//חישוב דרגה ממוצעת ממושקלת
            getDensity();//חישוב צפיפות
        }

        private  void Search(string v)//חיפוש ע"פ אלגוריטמים לווינשטיין
        {
            foreach (var item in nodes)
            {
                var d = levenshtein(v, item.label);//שליחת כל אחד מהקודקודים לנוסחה
                item.m = d;
            }
            nodes = nodes.OrderBy(n => n.m).ToList();//מיון מהקרוב ביותר לרחוק
            
        }

        private  void getDensity()
        {
            decimal Density = Convert.ToDecimal(Edges.Count) / (Convert.ToDecimal(nodes.Count) * Convert.ToDecimal(nodes.Count));//נוסחת הצפיפות לגרף מוכוון הוא : כמות הקשתות חלקי הקודקודים בשניה
            decimal DensityAsUndirected = Convert.ToDecimal(EdgesNo.Count) / (Convert.ToDecimal(nodes.Count) * (nodes.Count + 1) / 2);//נוסחת הצפיפות לגרף שאינו מוכוון כמות הקשתות חלקי מספר הקוקודים כפול מספר הקוקודים +1
            prop.Add("Density", Density.ToString());
            prop.Add("DensityAsUndirected", DensityAsUndirected.ToString());
    
        }

        private  void getAaverageWeightedDegree()
        {
            decimal Average = Convert.ToDecimal(edges) / nodes.Count;//דרגה ממוצעת ממושקל חישוב כל הקשתות חלקי הקודקודים
            prop.Add("Aaverage Weighted Degree:", Average.ToString());
        }

        private  void getAaverageDegree()
        {
            decimal Average = Convert.ToDecimal(Edges.Count) / nodes.Count;//דרגה ממוצעת= קשתות חלקי קודקודים()
            prop.Add("Aaverage Degree:", Average.ToString());

        }

        private  void getEdges()
        {
            prop.Add("Edges:", Edges.Count.ToString());
        }

        private  void getNodes()
        {
            string strFileName = @"C:\Users\User\Downloads\aaaa.csv";
            using (var reader = new StreamReader(strFileName))
            {
                nodes = new List<node>();
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    bool isInt = int.TryParse(values[0], out int number);
                    if (isInt)
                    {
                        nodes.Add(new node(number, values[1]));

                    }
                }
                prop.Add("Nodes:", nodes.Count.ToString());
            }
        }

        private  void WeightedNetwork()
        {
            string strFileName = @"C:\Users\User\Downloads\bbb.csv";
            using (var reader = new StreamReader(strFileName))
            {
                reader.ReadLine();
                edges = 0;
                Edges = new List<Edge>();
                EdgesNo = new List<Edge>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)//קריאה מהקובץ
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Edge edge = Edges.Where(u => u.from == values[0] && u.to == values[1]).FirstOrDefault();//  אם לא קיימת קשת ברשימת הקשתות עבור רשת מוכוונת
                    Edge dNo = EdgesNo.Where(u => u.from == values[0] && u.to == values[1] || u.from == values[1] && u.to == values[0]).FirstOrDefault();//  אם לא קיימת קשת ברשימת הקשתות עבור רשת לא מוכוונת
                    if (edge != null)
                    {
                        edge.weight++;//אם קיים תוספת 1 למשקל
                    }
                    else
                    {
                        Edges.Add(new Edge(values[0], values[1], 1));//אם לא קיים הוספת הקשת לרשימת הקשתות
                    }
                    if (dNo == null)//טיפול לרשת לא ממושקלת
                    {
                        EdgesNo.Add(new Edge(values[0], values[1], 1));

                    }
                    edges++;//שמירת מספר הקשתות ברשת

                }
            }
        }


        private static Int32 levenshtein(String a, String b)//אלגוריטמים לווינשטיין
        {

            if (string.IsNullOrEmpty(a))
            {
                if (!string.IsNullOrEmpty(b))
                {
                    return b.Length;
                }
                return 0;
            }

            if (string.IsNullOrEmpty(b))
            {
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Length;
                }
                return 0;
            }

            Int32 cost;
            Int32[,] d = new int[a.Length + 1, b.Length + 1];
            Int32 min1;
            Int32 min2;
            Int32 min3;

            for (Int32 i = 0; i <= d.GetUpperBound(0); i += 1)
            {
                d[i, 0] = i;
            }

            for (Int32 i = 0; i <= d.GetUpperBound(1); i += 1)
            {
                d[0, i] = i;
            }

            for (Int32 i = 1; i <= d.GetUpperBound(0); i += 1)
            {
                for (Int32 j = 1; j <= d.GetUpperBound(1); j += 1)
                {
                    cost = Convert.ToInt32(!(a[i - 1] == b[j - 1]));

                    min1 = d[i - 1, j] + 1;
                    min2 = d[i, j - 1] + 1;
                    min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }

            return d[d.GetUpperBound(0), d.GetUpperBound(1)];

        }


    }
}
