﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Immutable;
using Microsoft.VisualStudio.LanguageServer.Protocol;

namespace Microsoft.Quantum.QsCompiler
{
    internal class TypeRewriteStepsLoader : AbstractRewriteStepsLoader
    {
        public TypeRewriteStepsLoader(Action<Diagnostic> onDiagnostic = null, Action<Exception> onException = null) : base(onDiagnostic, onException)
        {
        }

        public override ImmutableArray<LoadedStep> GetLoadedSteps(CompilationLoader.Configuration config)
        {
            if (config.RewriteStepTypes == null)
            {
                return ImmutableArray<LoadedStep>.Empty;
            }

            var rewriteSteps = ImmutableArray.CreateBuilder<LoadedStep>();

            foreach (var definedStep in config.RewriteStepTypes)
            {
                var loadedStep = this.CreateStep(definedStep.Item1, new Uri(definedStep.Item1.Assembly.Location), definedStep.Item2);
                if (loadedStep != null)
                {
                    rewriteSteps.Add(loadedStep);
                }
            }

            return rewriteSteps.ToImmutable();
        }
    }
}
