using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vrt_proje
{
    internal class marker_add
    {
        public Form2 form;

        public GMapOverlay markerOverlay;
        public GMapOverlay markeroverlay2map;
        public GMapOverlay markeroverlay3map;
        public void add_marker(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);//markerin içine tool tanımladık
            marker.ToolTipText = Convert.ToString(CustID);//markerin içine yazı yazdırdık
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;//markerin içine yazının ne zaman görüneceğini yazdık
            form.markerOverlay.Markers.Add(marker);
        }
        public void add_marker_2th(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);//markerin içine tool tanımladık
            marker.ToolTipText = Convert.ToString(CustID);//markerin içine yazı yazdırdık
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;//markerin içine yazının ne zaman görüneceğini yazdık
            form.markeroverlay2map.Markers.Add(marker);
        }
        public void add_marker_3th(double lat, double lon, int CustID)
        {
            PointLatLng marker_point = new PointLatLng(lat, lon);
            GMarkerGoogle marker = new GMarkerGoogle(marker_point, GMarkerGoogleType.lightblue_dot);
            marker.ToolTip = new GMapToolTip(marker);//markerin içine tool tanımladık
            marker.ToolTipText = Convert.ToString(CustID);//markerin içine yazı yazdırdık
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;//markerin içine yazının ne zaman görüneceğini yazdık
            form.markeroverlay3map.Markers.Add(marker);
        }
    }
}
