﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;
using Lucky.Core.Infrastructure;

namespace Lucky.Web.Framework
{
    public class LuckyValidatorFactory : AttributedValidatorFactory
    {
        public override IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
                if ((attribute != null) && (attribute.ValidatorType != null))
                {
                    //validators can depend on some customer specific settings (such as working language)
                    //that's why we do not cache validators
                    //var instance = _cache.GetOrCreateInstance(attribute.ValidatorType,
                    //                           x => EngineContext.Current.ContainerManager.ResolveUnregistered(x));
                    var instance = EngineContext.Current.ContainerManager.ResolveUnregistered(attribute.ValidatorType);
                    return instance as IValidator;
                }
            }
            return null;

        }
    }
}
