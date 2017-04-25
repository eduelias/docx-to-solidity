using DocSolTemplateer.Infrastructure.BaseTypes;
using System;
using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISCExpression
    {
        Guid GroupIdentification { get; set; }

        string Name { get; set; }
        
        IEnumerable<SCTControlBase> Controls { get; set; }        

        ISContractTemplate ContainerTemplate { get; set; }

        IEnumerable<string> GetControlIds();

        List<object> ControlValues { get; set; }
    }
}
