using System;

namespace Cirrious.Sphero.WorkBench.UI.Wpf
{
    public class RegionAttribute : Attribute
    {
        public RegionAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }    
    }
}