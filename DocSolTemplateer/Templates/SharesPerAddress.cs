using DocSolTemplateer.Infrastructure.BaseTypes;
using DocSolTemplateer.Infrastructure.Enums;
using DocSolTemplateer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.DocSolTemplateer.Templates
{        
    public class SharePerAddressSCTeplate : ISContractTemplate
    {                
        public string Name
        {
            get
            {
                return "Shares per Address smart contract";
            }
        }

        public string GetContractGlobalFields()
        {
            return "mapping (address => int) private shares;";
        }

        public override string ToString()
        {
            return this.Name;
        }        

        public IEnumerable<ISCExpression> GetDefaultExpression()
        {
            var list = new List<ISCExpression>();
            var expr = new SCExpressionBase()
            {
                Name = "Address/share"
            };

            expr.TemplateControls = new List<SCTControlBase>()
                {
                    new SCTControlBase() {                            
                        Order = 1,
                        Kind = SCTControlKindEnum.String,
                        DisplayText = "Wallet addres (0x0000)",
                        InternalName = "SharesPerAddress_address"
                    },
                    new SCTControlBase() {                            
                        Order = 2,
                        Kind = SCTControlKindEnum.Integer,
                        DisplayText = "Share percent",
                        InternalName = "SharesPerAddress_shares"
                    }
                };

            list.Add(expr);                

            return list;         
        }

        public IEnumerable<ISCExpression> UsedExpressions { get; }

        public string GetValorFunctionBody()
        {
            return string.Empty;
        }        

        public void AddUsedExpressionRange(IEnumerable<ISCExpression> expressions)
        {   
            (UsedExpressions as List<ISCExpression>).AddRange(expressions);
        }

        public IEnumerable<string> GetConstructorData()
        {
            foreach (var ue in UsedExpressions)
            {
                foreach (var tc in ue.TemplateControls.GroupBy(x => x.Grouping))
                {
                    foreach (var g in tc)
                    {
                        yield return string.Format("{0} = {1}", g, g.DisplayText);
                    }
                }
            }
            //return this.UsedExpressions.SelectMany(x => x.GetAtrributionExpression());         
        }

        public IEnumerable<string> GetContractUnamedFunctionBody()
        {
            return new List<string>() { string.Empty };
        }
    }
}
