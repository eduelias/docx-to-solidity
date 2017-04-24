using DocSolTemplateer.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocSolTemplateer.Infrastructure.BaseTypes
{
    public class SCExpressionBase : ISCExpression
    {           
        public Guid GroupIdentification { get; set; }
        public string Name { get; set; }        
        public IEnumerable<SCTControlBase> TemplateControls { get; set; }

        public IEnumerable<string> GetAtrributionExpression()
        {
            var allct = TemplateControls.SelectMany(x => x.ControlValues);
            foreach (var a in allct)
            {
                yield return string.Format("shares[{0}] = {1};", a.Value, a.Key);
            }
            

            //var str = new List<string>();
            //var items = this.TemplateControls.Select(x => new { k = x.InternalName, v = x.GetValue(x.InternalName) }).ToArray();            

            //for (var i = 0; i < items.Count(); i++)
            //{                
            //    var addresses = items[i].v.Split(',');
            //    var shares = items[++i].v.Split(',');
            //    for (var j = 0; j < addresses.Length; j++)
            //    {
            //        str.Add(string.Format("shares[{0}] = {1}; \r\n", addresses[j], shares[j]));
            //    }

            //}

            //return str;
        }

        public void SetAsUsed()
        {
            this.Used
        }
    }
}
