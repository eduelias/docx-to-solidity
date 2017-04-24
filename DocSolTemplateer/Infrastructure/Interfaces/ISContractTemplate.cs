using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISContractTemplate
    {
        bool ShouldCheck { get; }
        string Name { get; }

        string GetContractGlobalFields();
        //string GetParticularFunctions<T>(T Data);

        //string GetValorFunctionBody();

        IEnumerable<ISCExpression> DefaultExpressions { get; }
        IEnumerable<ISCExpression> UsedExpressions { get; }

        IEnumerable<string> GetConstructorData();
        IEnumerable<string> GetContractUnamedFunctionBody();
    }
}
