using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerializationRules.Entities;
using Sitecore.Rules;

namespace SerializationRules.Conditions
{
    public class SerializationRuleContext : RuleContext
    {
        public SerializationDefinition SerializationDefinition { get; set; }
    }
}