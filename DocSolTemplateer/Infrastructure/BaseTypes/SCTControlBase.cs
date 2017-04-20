using DocSolTemplateer.Infrastructure.Enums;
using DocSolTemplateer.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DocSolTemplateer.Infrastructure.BaseTypes
{
    public class SCTControlBase
    {
        public int Order { get; set; }        
        public string DisplayText { get; set; }
        public string InternalName { get; set; }        

        public SCTControlKindEnum Kind { get; set; }

        public Dictionary<string, object> MetaInformation = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
    }

    public class SCTControl<TData> : SCTControlBase
    {
        public Expression<Func<TData, object>> Mapping { get; set; }
        
        public void SetValue(TData model, object theValue)
        {
            var prop = Mapping.GetPropertyInfo();
            prop.SetValue(model, theValue);
        }
    }

}
