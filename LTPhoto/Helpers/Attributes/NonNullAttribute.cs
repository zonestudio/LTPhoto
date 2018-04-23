using System;

namespace LTPhoto.Helpers.Attributes
{
    /// <summary>
    /// 非空的特性标志
    /// </summary>
    public class NotNullAttribute : Attribute
    {
        public NotNullAttribute()
        {
            
        }

        private string _description;
        public NotNullAttribute(string description)
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
