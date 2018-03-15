using DocSolTemplateer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocSolTemplateer.Infrastructure.BaseTypes
{
    public class SCExpressionBase : ISCExpression
    {
        public SCExpressionBase()
        {
            ControlValues = new List<object>();
        }

        public ISContractTemplate ContainerTemplate { get; set; }

        public Guid GroupIdentification { get; set; }
        public string Name { get; set; }        
        public IEnumerable<SCTControlBase> Controls { get; set; }

        public List<object> ControlValues { get; set; }        

        public IEnumerable<string> GetControlIds()
        {
            return Controls.Where(x => x.Grouping == GroupIdentification).SelectMany(x => x.GetObjectIds(GroupIdentification.ToString()));
        }

    }
}
