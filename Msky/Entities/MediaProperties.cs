using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Msky.Entities
{
    public class MediaProperties : BaseObject
    {
        public double Width { get { return Value<double>("width", -1); } }
        public double Height { get { return Value<double>("height", -1); } }

        public override string ToString()
        {
            return string.Format("Properties: {0}x{1}", Width, Height);
        }
    }
}
