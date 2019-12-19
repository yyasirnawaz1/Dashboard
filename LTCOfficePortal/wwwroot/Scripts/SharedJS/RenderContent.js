var RenderContent = {
    renderedForm: null,
    renderData:null,

    RenderContentInIframe: function (Content) {
        RenderContent.renderedForm = $('#render-container').formRender({
            formData: RenderContent.removeSelectedTitles(JSON.stringify(Content))
        }); 
    },

    removeSelectedTitles: function (content) {
        var json = JSON.parse(content);
        for (i = 0; i < json.length; i++)
            if (json[i].type == 'LineSeprator' || json[i].type == 'NewLine')
                json[i].label = '';
        return JSON.stringify(json)
    },
}


jQuery(document).ready(function ($) {
    
    if (!window.fbControls) window.fbControls = new Array();
    window.fbControls.push(function (controlClass) {
        class controlLineSeprator extends controlClass {
            build() {
                return '<hr>';
            }
        }
        controlClass.register('LineSeprator', controlLineSeprator);
        return controlLineSeprator;
    });
    window.fbControls.push(function (controlClass) {
        class controlNewLine extends controlClass {
            build() {
                return '<br/>';
            }
        }
        controlClass.register('NewLine', controlNewLine);
        return controlNewLine;
    });
    window.fbControls.push(function (controlClass) {
        class controlStarRating extends controlClass {
            build() {
                return this.markup('span', null, { id: this.config.name });
            }
            onRender() {
                let value = this.config.value || 3.6;
                $('#' + this.config.name).rateYo({ rating: value });
            }
        }
        controlClass.register('starRating', controlStarRating);
        return controlStarRating;
    });
});

window.getFormBuilderData = function () {
    return JSON.stringify($('#render-container').formRender('userData'));
}

window.getFormBuilderHTML = function () {
    //return JSON.stringify($('#render-container').formRender('userData'));
    return $('#render-container').formRender('html'); 
}