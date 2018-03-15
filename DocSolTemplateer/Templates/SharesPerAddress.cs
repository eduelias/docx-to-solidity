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
        public const string AddressFieldInternalName = "SharesPerAddress_address";
        public const string SharesFieldInternalName = "SharesPerAddress_shares";

        public object[] CurrentValues;

        public SharePerAddressSCTeplate()
        {
            UsedExpressions = new List<ISCExpression>();
        }

        /// <summary>
        /// Name shown on the themplate chooser combobox
        /// </summary>
        public string Name
        {
            get
            {
                return "Shares per Address smart contract";
            }
        }       

        public override string ToString()
        {
            return this.Name;
        }           

        /// <summary>
        /// Gets the default components setup, to insert on document
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISCExpression> GetDefaultExpression()
        {
            var list = new List<ISCExpression>();
            var expr = new SCExpressionBase
            {
                Name = "Address/share",
                ContainerTemplate = this,
                Controls = new List<SCTControlBase>()
                {
                    new SCTControlBase()
                    {
                        Order = 1,
                        Kind = SCTControlKindEnum.String,
                        DisplayText = "Wallet addres (0x0000)",
                        InternalName = SharePerAddressSCTeplate.AddressFieldInternalName
                    },
                    new SCTControlBase()
                    {
                        Order = 2,
                        Kind = SCTControlKindEnum.Integer,
                        DisplayText = "Share percent",
                        InternalName = SharePerAddressSCTeplate.SharesFieldInternalName
                    }
                }
            };

            list.Add(expr);

            return list;
        }

        /// <summary>
        /// Used components on screen
        /// </summary>
        public IEnumerable<ISCExpression> UsedExpressions { get; set;  }
        
        public void AddUsedExpressionRange(IEnumerable<ISCExpression> expressions)
        {   
            (UsedExpressions as List<ISCExpression>).AddRange(expressions);
        }

        public void SetControlValues(object[] values)
        {
            if (values.Length != 2)
                throw new ArgumentOutOfRangeException();

            CurrentValues = values;
        }

        public List<string> Validate()
        {
            var list = new List<string>();

            //var sum = UsedExpressions.SelectMany(x => x.ControlValues.Where(w => w.Key == SharesFieldInternalName).Select(q => q.Value))
            //    .Cast<int>()
            //    .Sum(x => x);

            foreach (var ue in UsedExpressions)
            {
                var address = ue.ControlValues.First();
                var shares = ue.ControlValues.Last();

                list.Add(string.Format("shares[{0}] = {1}; ", address, shares));
                list.Add(string.Format("addresses.push({0}); ", address));
            }

            return list;
        }

        public IEnumerable<string> GetConstructorData()
        {
            var list = new List<string>();

            foreach (var ue in UsedExpressions)
            {
                var address = ue.ControlValues.First();
                var shares = ue.ControlValues.Last();             

                list.Add(string.Format("shares[{0}] = {1}; ", address, shares));
                list.Add(string.Format("addresses.push({0}); ", address));
            }

            return list;                 
        }

        public IEnumerable<string> GetContractUnamedFunctionBody()
        {
            var lst = new List<string>();

            lst.Add("for (var i = 0; i < addresses.length; i++) {");
            lst.Add("   addresses[i].send(msg.value * (shares[addresses[i]]/100));");
            lst.Add("}");

            return  lst;
        }

        /// <summary>
        /// Retrieves the Contract global fields from this template
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetContractGlobalFields()
        {
            var lst = new List<string>();

            lst.Add("mapping (address => uint) private shares;");
            lst.Add($"address[{UsedExpressions.Count()}] addresses;");

            return lst;
        }
    }
}

