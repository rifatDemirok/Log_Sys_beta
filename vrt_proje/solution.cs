using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vrt_proje
{
    internal class solution
    {
        problem Prob;
        Form2 form2;
        public List<int> Route;
        public List<int> BestRoute;
        public double maliyet = 0;
        public double EnİyiMaliyet = double.MaxValue;

        public void obsolution()
        {
          //  int lenght = form2.Lngs00.Count;
            Prob = new problem();
            form2 = new Form2();
            int GüncelMüş, SıradakiMüş = 0;
            Route = new List<int>(0);
            BestRoute = new List<int>(0);

            for (int i = 0; i < form2.lnght; i++)
            {
                Route.Clear();
                maliyet = 0;
                GüncelMüş = i;
                Route.Add(GüncelMüş);

                do
                {
                    SıradakiMüş = YakındakiMüşBul(GüncelMüş);
                    Route.Add(SıradakiMüş);
                    GüncelMüş = SıradakiMüş;
                } while (Route.Count != form2.lnght);
               
                
                //Route.Add(GüncelMüş);
                int FirstCust = Route.First();
                int LastCust = Route.Last();
                maliyet += Prob.distMat[LastCust, FirstCust];

                if (maliyet < EnİyiMaliyet)
                {
                    EnİyiMaliyet = maliyet;
                    BestRoute = Route.ToList();
                }
            }

        }

        public int YakındakiMüşBul(int GüncelMüş)
        {
            Prob = new problem();
            form2 = new Form2();
            int SıradakiMüş = 0;
            double MinVal = double.MaxValue;
            for (int i = 0; i < form2.lnght; i++)
            {
                int Position = Route.IndexOf(i);
                if (Position == -1 && GüncelMüş != i && Prob.distMat[GüncelMüş, i] < MinVal)
                {
                    MinVal = Prob.distMat[GüncelMüş, i];
                    SıradakiMüş = i;
                }
            }
            maliyet += MinVal;
            return SıradakiMüş;

        }

    }
}
