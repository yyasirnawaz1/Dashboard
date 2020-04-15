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
        class controlSignature extends controlClass {
            build() {

                return '<div><div id="' + this.config.name + '"></div></div><p style="clear: both;"><button id="clear' + this.config.name + '">Clear</button></p>';

            }
            onRender() {

                var signature = $('#' + this.config.name);
                signature.signature();
                var signature = $('#' + this.config.name).signature();
                $('#clear' + this.config.name).click(function () {
                    signature.signature('clear');
                });
                //$('#json' + this.config.name).click(function () {
                //    alert(signature.signature('toJSON'));
                //});
                //$('#svg' + this.config.name).click(function () {
                //    alert(signature.signature('toSVG'));
                //});
            }

        }
        controlClass.register('signature', controlSignature);
        return controlSignature;
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