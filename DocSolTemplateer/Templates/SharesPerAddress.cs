using DocSolTemplateer.Infrastructure.BaseTypes;
using DocSolTemplateer.Infrastructure.Enums;
using DocSolTemplateer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.DocSolTemplateer.Templates
{
    public class SharesPerAddressData
    {
        public string Address { get; set; }
        public string Share { get; set; }
    }

    public class SCTemplateBase
    {
        public IEnumerable<ISCExpression> UsedExpressions = new List<ISCExpression>();
    }

    public class SharePerAddressSCTeplate : SCTemplateBase, ISContractTemplate
    {        
        public Type DataType = typeof(SharesPerAddressData);

        public string Name
        {
            get
            {
                return "Shares per Address smart contract";
            }
        }

        public bool ShouldCheck
        {
            get
            {
                return DefaultExpressions.Any(e => e.ShouldCheck);
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

        public IEnumerable<ISCExpression> DefaultExpressions
        {
            get
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

                this.AddUsedExpressionRange(list);

                return list;
            }
        }

        IEnumerable<ISCExpression> ISContractTemplate.UsedExpressions
        {
            get
            {
                return this.UsedExpressions;
            }
        }

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
            return this.UsedExpressions.SelectMany(x => x.GetAtrributionExpression());         
        }

        public IEnumerable<string> GetContractUnamedFunctionBody()
        {
            return new List<string>() { string.Empty };
        }
    }
}
