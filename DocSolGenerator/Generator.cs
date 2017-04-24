using DocSolTemplateer.Infrastructure.Interfaces;
using PoC.DocSolTemplateer.Templates;
using System.Collections.Generic;

namespace DocSolGenerator
{
    public static class Generator
    {
        private const string TAB = @"   ";
        private const string DEC_CONTRACT_NAME = @"contract {0} {";
        private const string DEC_PRAGMA = @"pragma solidity ^0.4.6;";

        public static string GenerateContract(string contractName, IEnumerable<ISContractTemplate> templates, string documentData)
        {
            var contractBody = new List<string>();

            contractBody.Add(DEC_PRAGMA);
            contractBody.Add(string.Format(DEC_CONTRACT_NAME, contractName));

            foreach (var template in templates)
            {
                contractBody.Add(string.Format("{0}{1}", TAB, template.GetContractGlobalFields()));
            }
        }
    }
}
