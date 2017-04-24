﻿using DocSolTemplateer.Infrastructure.BaseTypes;
using System;
using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISCExpression
    {
        //Guid GroupIdentification { get; set; }

        string Name { get; set; }
        bool ShouldCheck { get; set; }
        IEnumerable<SCTControlBase> TemplateControls { get; set; }

        IEnumerable<string> GetAtrributionExpression();
    }
}
