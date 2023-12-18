using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Object
{

    public enum VisualTargetSelection
    {
        Potato,
        Grape,
    }

    public class TargetObject
    {


        private VisualTargetSelection objectTarget;
        private int[] targetRange = new int[6];

        public void SetVisualTargetSelection (VisualTargetSelection selection)
        {
            objectTarget= selection;    
        }

        public VisualTargetSelection GetVisualTargetSelection ()
        {
            return objectTarget;
        }

        public int[] GetTargetColor ()
        {
            return targetRange;
        }

        public void SetTargetColor()
        {
            int h_channel_min;
            int s_channel_min;
            int v_channel_min;
            int h_channel_max;
            int s_channel_max;
            int v_channel_max;

            switch(this.objectTarget)
            {
                case VisualTargetSelection.Potato:
                    h_channel_min = 0;
                    s_channel_min = 1;
                    v_channel_min = 2;
                    h_channel_max = 3;
                    s_channel_max= 4;
                    v_channel_max = 5;
                    break;
                case VisualTargetSelection.Grape:
                    h_channel_min = 0;
                    s_channel_min = 1;
                    v_channel_min = 2;
                    h_channel_max = 3;
                    s_channel_max = 4;
                    v_channel_max = 5;
                    break;
                default:
                    h_channel_min = 0;
                    s_channel_min = 1;
                    v_channel_min = 2;
                    h_channel_max = 3;
                    s_channel_max = 4;
                    v_channel_max = 5;
                    break;
            }

            this.targetRange[0] = h_channel_min;
            this.targetRange[1] = s_channel_min;
            this.targetRange[2] = v_channel_min;
            this.targetRange[3] = h_channel_max;
            this.targetRange[4] = s_channel_max;
            this.targetRange[5] = v_channel_max;
        }

    }

    public class SaveData
    {

        public int acceptView { get; set; }
        public int rejectView { get; set; }

        public int totalView { get; private set; }

        public void Total()
        {
            totalView = this.acceptView + this.rejectView;
        }


    }

}