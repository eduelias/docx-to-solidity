﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.DocSolCreator.Templates
{
    public enum SCTControlKindEnum
    {
        Date,
        String,        
        Integer            
    }

    public class SCTControlBase
    {
        public int Order { get; set; }
        public SCTControlKindEnum Kind { get; set; }
        public string DisplayText { get; set; }
        public string InternalName { get; set; }
        public Dictionary<string, object> MetaInformation = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
    }

    public interface ISCExpression
    {
        string Name { get; set; }
        bool ShouldCheck { get; set; }  
        IEnumerable<SCTControlBase> GetControls();
    }

    public interface ISContractTemplate
    {
        bool ShouldCheck { get; }
        string Name { get; }
        string GetContractGlobalFields<T>(T Data);
        string GetParticularFunctions<T>(T Data);

        string GetValorFunctionBody();

        IEnumerable<ISCExpression> GetExpressionList();        
    }

    public class SCExpression : ISCExpression
    {
        //private string regex_address = @"([13][a-km-zA-HJ-NP-Z1-9]{25,34}$)";
        //private string regex_sharepc = @"(\d+|\d+[.]\d+)%?";
        public string Name { get; set; }

        public bool ShouldCheck { get; set; }                

        public IEnumerable<SCTControlBase> GetControls()
        {
            var lt = new List<SCTControlBase>();

            lt.Add(new SCTControlBase() { Order = 1, Kind = SCTControlKindEnum.String, DisplayText = "Wallet addres (0x0000)", InternalName = "SharesPerAddress_address" });
            lt.Add(new SCTControlBase() { Order = 2, Kind = SCTControlKindEnum.Integer, DisplayText = "Share percent", InternalName = "SharesPerAddress_shares" });

            return lt;
        }
    }

    public class SharesPerAddressData
    {
        public string Address { get; set; }
        public int Share { get; set; }
    }

    public class SharePerAddressSCTeplate : ISContractTemplate
    {
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
                new SCExpression {
                    Name = "Address/share"                                        
                }
            };
        }

        public string GetValorFunctionBody()
        {
            return string.Empty;
        }
    }
}
