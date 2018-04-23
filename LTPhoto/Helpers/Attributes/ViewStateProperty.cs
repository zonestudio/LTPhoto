using System;

namespace LTPhoto.Helpers.Attributes
{
    /// <summary>
    /// ViewStateProperty
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ViewStateProperty : Attribute
    {
        public string ViewStateName { get; private set; }

        public ViewStateProperty()
        {
            this.ViewStateName = string.Empty;
        }

        public ViewStateProperty(string in_ViewStateName)
        {
            this.ViewStateName = in_ViewStateName;
        }
    }
}