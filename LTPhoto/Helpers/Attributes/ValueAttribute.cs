using System;

namespace LTPhoto.Helpers.Attributes
{
     [AttributeUsage(
    AttributeTargets.Parameter | AttributeTargets.Field |
    AttributeTargets.Property, Inherited = true)]
    public class ValueAttribute : Attribute
    {
        private string _description;
        public ValueAttribute(string description)
        {
            _description = description;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
