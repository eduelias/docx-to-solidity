using DocSolTemplateer.Infrastructure.Enums;
using DocSolTemplateer.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DocSolTemplateer.Infrastructure.BaseTypes
{
    public class SCTControlBase
    {
        public Type DataType { get; set; }
        public PropertyInfo Property { get; set; }

        private List<string> ControlIds = new List<string>();
        
        public int Order { get; set; }        
        public string DisplayText { get; set; }
        public string InternalName { get; set; }        

        public SCTControlKindEnum Kind { get; set; }

        public Dictionary<string, object> MetaInformation = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);

        public IEnumerable<string> GetObjectIds()
        {
            return ControlIds;
        }

        public void AddObjectId(string Id)
        {
            ControlIds.Add(Id);
        }

        public void SetValue<TData>(TData model, object theValue)
        {
            this.Property.SetValue(model, theValue);
        }
    }

    public class SCTControl<TData> : SCTControlBase
    {
        private Expression<Func<TData, object>> _mapping;

        public SCTControl()
        {
            this.DataType = typeof(TData);
        }

        public Expression<Func<TData, object>> Mapping
        {
            get
            {
                return _mapping;
            }
            set
            {
                this.Property = value.GetPropertyInfo();
                _mapping = value;
            }
        }               
    }

}
