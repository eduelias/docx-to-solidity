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
        public int Share { get; set; }
    }

    public class SharePerAddressSCTeplate : ISContractTemplate
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
            return new List<ISCExpression>()
            {
                new SCExpressionBase {
                    Name = "Address/share",
                    TemplateControls = new List<SCTControl<SharesPerAddressData>>()
                    {
                        new SCTControl<SharesPerAddressData>() {
                            Order = 1,
                            Kind = SCTControlKindEnum.String,
                            DisplayText = "Wallet addres (0x0000)",
                            InternalName = "SharesPerAddress_address",
                            Mapping = x => x.Address                       
                        },
                        new SCTControl<SharesPerAddressData>() {
                            Order = 2,
                            Kind = SCTControlKindEnum.Integer,
                            DisplayText = "Share percent",
                            InternalName = "SharesPerAddress_shares",
                            Mapping = x => x.Share
                        }
                    }
                }
            };
        }

        public string GetValorFunctionBody()
        {
            return string.Empty;
        }
    }
}
