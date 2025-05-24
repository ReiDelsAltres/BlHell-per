window.renderKaTeX = (element, latex) => {
    if (window.katex) {
        katex.render(latex, element, {
            throwOnError: false
        });
    }
};