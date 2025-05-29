using BlHell_per.Core.Compat;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq.Expressions;

namespace BlHell_per.Components;

public abstract partial class QuestionViewBase : TemplateComponentBase
{
    protected virtual RenderFragment? ChildContent => (builder) => this.BuildRenderTree(builder);
    protected abstract RenderFragment BaseContent { get; }

    public QuestionViewBase()
    {
        _renderFragment = builder =>
        {
            _hasPendingQueuedRender = false;
            _hasNeverRendered = false;
            builder.AddContent(0, BaseContent);
        };
    }


}
