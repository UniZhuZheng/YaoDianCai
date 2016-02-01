using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniDaoChart.EntityResponse
{
    public class VoiceMessage : BaseMessage
    {
        private VoiceEntity voice;
        public VoiceEntity Voice
        {
            get { return voice; }
            set { voice = value; }
        }
    }
}
