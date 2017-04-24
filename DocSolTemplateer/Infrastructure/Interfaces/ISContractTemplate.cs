using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISContractTemplate
    {
        bool ShouldCheck { get; }
        string Name { get; }

        //string GetContractGlobalFields<T>(T Data);
        //string GetParticularFunctions<T>(T Data);

        //string GetValorFunctionBody();

        IEnumerable<ISCExpression> GetExpressionList();                
    }
}
