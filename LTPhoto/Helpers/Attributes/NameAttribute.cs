using System;

namespace LTPhoto.Helpers.Attributes
{
     [AttributeUsage(
    AttributeTargets.Parameter | AttributeTargets.Field |
    AttributeTargets.Property, Inherited = true)]
    public class NameAttribute : Attribute
    {
        private string _description;
        public NameAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }
    }
}
