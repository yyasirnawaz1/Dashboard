/**
 * Star rating class - show 5 stars with the ability to select a rating
 */

// configure the class for runtime loading
if (!window.fbControls) window.fbControls = [];
window.fbControls.push(function (controlClass) {
    /**
     * Star rating class
     */
    class controlSignature extends controlClass {

        /**
         * Class configuration - return the icons & label related to this control
         * @returndefinition object
         */
        static get definition() {
            return {
                icon: '🌟',
                i18n: {
                    default: 'Signature'
                }
            };
        }

        /**
         * javascript & css to load
         */
        configure() {
            this.js = '//keith-wood.name/js/jquery.signature.js';
            this.css = '//keith-wood.name/css/jquery.signature.css';
        }

        /**
         * build a text DOM element, supporting other jquery text form-control's
         * @return {Object} DOM Element to be injected into the form.
         */
        build() {
            return '<div id="' + this.config.name + '"></div>';

        }
        onRender() {
            alert(this.config.name);
            $('#' + this.config.name).signature();
        }
    }

    // register this control for the following types & text subtypes
    controlClass.register('signature', controlSignature);
    return controlSignature;
});
