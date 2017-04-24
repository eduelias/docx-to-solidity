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
        public static IEnumerable<ISCExpression> UsedExpressions = new List<ISCExpression>();
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
                return GetExpressionList().Any(e => e.ShouldCheck);
            }
        }        

        public string GetContractGlobalFields<T>(T Data)
        {
            return "mapping (address => int) private shares;";
        }



        public string GetParticularFunctions<T>(T Data)
        {
            if (!typeof(T).IsAssignableFrom(typeof(SharesPerAddressData)))
                throw new Exception(typeof(SharesPerAddressData).Name);

            return @" pragma solidity ^0.4.6;
                        contract eAgreement {    
                        " + GetContractGlobalFields<T>(Data) + @"

                        function eAgreement() {
                        " + GetCtorData<T>(Data) + @"
                        }
            }";
        }

        public string GetCtorData<T>(T Data)
        {
            return "shares[0x2a3f5381c3bb6aa77029b48316e9c441675c9dd4] = 100;";
        }

        public override string ToString()
        {
            return this.Name;
        }

        public IEnumerable<ISCExpression> GetExpressionList()
        {
            var list = new List<ISCExpression>();
            var guid = Guid.NewGuid();
            var expr = new SCExpressionBase()
            {
                Name = "Address/share"
            };

            expr.TemplateControls = new List<SCTControl<SharesPerAddressData>>()
                    {
                        new SCTControl<SharesPerAddressData>() {                            
                            Order = 1,
                            Kind = SCTControlKindEnum.String,
                            DisplayText = "Wallet addres (0x0000)",
                            InternalName = string.Format("SharesPerAddress_address_{0}", guid),
                            Mapping = x => x.Address
                        },
                        new SCTControl<SharesPerAddressData>() {                            
                            Order = 2,
                            Kind = SCTControlKindEnum.Integer,
                            DisplayText = "Share percent",
                            InternalName = string.Format("SharesPerAddress_shares_{0}", guid),
                            Mapping = x => x.Share
                        }
                    };
            list.Add(expr);

            this.AddUsedExpressionRange(list);

            return list;
        }

        public string GetValorFunctionBody()
        {
            return string.Empty;
        }

        public IEnumerable<ISCExpression> GetUsedExpressions()
        {
            return UsedExpressions;
        }

        public void AddUsedExpressionRange(IEnumerable<ISCExpression> expressions)
        {
            (UsedExpressions as List<ISCExpression>).AddRange(expressions);
        }
    }
}
