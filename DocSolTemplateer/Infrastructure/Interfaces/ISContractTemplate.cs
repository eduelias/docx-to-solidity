using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISContractTemplate
    { 
        string Name { get; }

        string GetContractGlobalFields();
        //string GetParticularFunctions<T>(T Data);

        //string GetValorFunctionBody();

        IEnumerable<ISCExpression> GetDefaultExpression();
        IEnumerable<ISCExpression> UsedExpressions { get; }

        IEnumerable<string> GetConstructorData();
        IEnumerable<string> GetContractUnamedFunctionBody();
    }
}
