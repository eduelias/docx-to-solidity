﻿using DocSolTemplateer.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DocSolGenerator
{
    public static class Generator
    {
        private const string TAB = @"   ";
        private const string DEC_CONTRACT_NAME = "contract {0}";
        private const string DEC_PRAGMA = "pragma solidity ^0.4.6;";

        public static string GenerateContract(string contractName, IEnumerable<ISContractTemplate> templates, string documentData)
        {
            var contractBody = new List<string>();

            contractBody.Add(DEC_PRAGMA);
            contractBody.Add(string.Format(DEC_CONTRACT_NAME, contractName));
            contractBody.Add("{");

            contractBody.Add(" ");

            foreach (var template in templates)
            {
                foreach (var lines in template.GetContractGlobalFields())
                {
                    contractBody.Add(string.Format("{0}{1}", TAB, lines));
                }
            }

            contractBody.Add(" ");

            contractBody.Add(TAB + "function " + contractName + "() {");
            foreach (var ctorData in templates.SelectMany(x => x.GetConstructorData()))
            {
                contractBody.Add(string.Format("{0}{0}{1}", TAB, ctorData));
            }
            contractBody.Add(TAB + "}");

            contractBody.Add(" ");

            contractBody.Add(string.Format("{0}function() payable ", TAB));
            contractBody.Add(TAB + "{");

            foreach (var unamedData in templates.SelectMany(x => x.GetContractUnamedFunctionBody()))
            {
                contractBody.Add(string.Format("{0}{0}{0}{1}", TAB, unamedData));
            }
            contractBody.Add(TAB + "}");

            contractBody.Add(" ");

            contractBody.Add("}");
            return string.Join("\r\n", contractBody);
        }
    }
}
