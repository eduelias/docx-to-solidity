using System.Collections.Generic;

namespace DocSolTemplateer.Infrastructure.Interfaces
{
    public interface ISContractTemplate
    { 
        string Name { get; }        
        //string GetParticularFunctions<T>(T Data);

        //string GetValorFunctionBody();
        IEnumerable<ISCExpression> GetDefaultExpression();
        IEnumerable<ISCExpression> UsedExpressions { get; set; }

        IEnumerable<string> GetContractGlobalFields();
        IEnumerable<string> GetConstructorData();
        IEnumerable<string> GetContractUnamedFunctionBody();        
    }
}
