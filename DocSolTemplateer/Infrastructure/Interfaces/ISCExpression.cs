using DocSolTemplateer.Infrastructure.BaseTypes;
using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISCExpression
    {        
        string Name { get; set; }
        bool ShouldCheck { get; set; }
        IEnumerable<SCTControlBase> TemplateControls { get; set; }        
    }
}
