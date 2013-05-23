using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Rules;

namespace SerializationRules.Extensions
{
    public static class Rules
    {
        public static bool Evaluate(this RuleList<RuleContext> rules, RuleContext context)
        {
            return rules.Rules.All(rule => rule.Evaluate(context));
        }
    }
}