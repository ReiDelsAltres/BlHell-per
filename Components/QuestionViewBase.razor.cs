using BlHell_per.Core.Compat;
using Microsoft.AspNetCore.Components;

namespace BlHell_per.Components;

public abstract partial class QuestionViewBase : MathJaxContentTemplateComponentFixed
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
