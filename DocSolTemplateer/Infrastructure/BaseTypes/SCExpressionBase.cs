using DocSolTemplateer.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.BaseTypes
{
    public class SCExpressionBase : ISCExpression
    {
        //private string regex_address = @"([13][a-km-zA-HJ-NP-Z1-9]{25,34}$)";
        //private string regex_sharepc = @"(\d+|\d+[.]\d+)%?";
        public string Name { get; set; }
        public bool ShouldCheck { get; set; }
        public IEnumerable<SCTControlBase> TemplateControls { get; set; }        
    }
}
