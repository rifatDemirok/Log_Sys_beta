using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vrt_proje
{
    internal class problem
    {
        public double[,] distMat;
        Form2 form2;
        public void EvaluateDistMat()
        {
            form2 = new Form2();
        
            distMat = new double[form2.lnght, form2.lnght];

            for (int i = 0; i < form2.lnght; i++)
            {
                for (int j = (i + 1); j < form2.lnght; j++)
                {
                    GeoCoordinate P1 = new GeoCoordinate(form2.Lats00[i], form2.Lngs00[i]);
                    GeoCoordinate P2 = new GeoCoordinate(form2.Lats00[j], form2.Lngs00[i]);

                    double Uzaklık = P1.GetDistanceTo(P2);
                    distMat[i, j] = Uzaklık / 1000;
                    distMat[j, i] = distMat[i, j];
                }
            }
        }
    }
}
