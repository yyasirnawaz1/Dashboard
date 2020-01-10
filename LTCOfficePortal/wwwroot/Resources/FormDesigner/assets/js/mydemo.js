jQuery(function ($) {
    var fields = [
        {
            type: 'autocomplete',
            label: 'Custom Autocomplete',
            required: true,
            values: [
                { label: 'SQL' },
                { label: 'C#' },
                { label: 'JavaScript' },
                { label: 'Java' },
                { label: 'Python' },
                { label: 'C++' },
                { label: 'PHP' },
                { label: 'Swift' },
                { label: 'Ruby' }
            ]
        },
        {
            label: 'Star Rating',
            attrs: {
                type: 'starRating'
            },
            icon: 'ðŸŒŸ'
        }
    ];

    var replaceFields = [
        {
            type: 'textarea',
            subtype: 'tinymce',
            label: 'tinyMCE',
            required: true,
        }
    ];

    var actionButtons = [{
        id: 'smile',
        className: 'btn btn-success',
        label: 'ðŸ˜',
        type: 'button',
        events: {
            click: function () {
                alert('ðŸ˜ðŸ˜ðŸ˜ !SMILE! ðŸ˜ðŸ˜ðŸ˜');
            }
        }
    }];

    var templates = {
        starRating: function (fieldData) {
            return {
                field: '<span id="' + fieldData.name + '">',
                onRender: function () {
                    $(document.getElementById(fieldData.name)).rateYo({ rating: 3.6 });
                }
            };
        }
    };

    var inputSets = [{
        label: 'User Details',
        icon: 'ðŸ‘¨',
        name: 'user-details', // optional
        showHeader: true, // optional
        fields: [{
            type: 'text',
            label: 'First Name',
            className: 'form-control'
        }, {
            type: 'select',
            label: 'Profession',
            className: 'form-control',
            values: [{
                label: 'Street Sweeper',
                value: 'option-2',
                selected: false
            }, {
                label: 'Brain Surgeon',
                value: 'option-3',
                selected: false
            }]
        }, {
            type: 'textarea',
            label: 'Short Bio:',
            className: 'form-control'
        }]
    }, {
        label: 'User Agreement',
        fields: [{
            type: 'header',
            subtype: 'h3',
            label: 'Terms & Conditions',
            className: 'header'
        }, {
            type: 'paragraph',
            label: 'Leverage agile frameworks to provide a robust synopsis for high level overviews. Iterative approaches to corporate strategy foster collaborative thinking to further the overall value proposition. Organically grow the holistic world view of disruptive innovation via workplace diversity and empowerment.',
        }, {
            type: 'paragraph',
            label: 'Bring to the table win-win survival strategies to ensure proactive domination. At the end of the day, going forward, a new normal that has evolved from generation X is on the runway heading towards a streamlined cloud solution. User generated content in real-time will have multiple touchpoints for offshoring.',
        }, {
            type: 'checkbox',
            label: 'Do you agree to the terms and conditions?',
        }]
    }];

    var typeUserDisabledAttrs = {
        autocomplete: ['access']
    };

    var typeUserAttrs = {
        text: {
            className: {
                label: 'Class',
                options: {
                    'red form-control': 'Red',
                    'green form-control': 'Green',
                    'blue form-control': 'Blue'
                },
                style: 'border: 1px solid red'
            }
        }
    };

    // test disabledAttrs
    var disabledAttrs = ['placeholder'];

    var fbOptions = {
        subtypes: {
            text: ['datetime-local']
        },
        onSave: function (e, formData) {
            toggleEdit();
            $('.render-wrap').formRender({
                formData: formData,
                templates: templates
            });
            window.sessionStorage.setItem('formData', JSON.stringify(formData));
        },
        stickyControls: {
            enable: true
        },
        sortableControls: true,
        fields: fields,
        templates: templates,
        inputSets: inputSets,
        typeUserDisabledAttrs: typeUserDisabledAttrs,
        typeUserAttrs: typeUserAttrs,
        disableInjectedStyle: false,
        actionButtons: actionButtons,
        disableFields: ['autocomplete'],
        replaceFields: replaceFields,
        disabledFieldButtons: {
            text: ['copy']
        }
        // controlPosition: 'left'
        // disabledAttrs
    };
    var formData = window.sessionStorage.getItem('formData');
    var editing = true;

    if (formData) {
        fbOptions.formData = JSON.parse(formData);
    }

    /**
     * Toggles the edit mode for the demo
     * @return {Boolean} editMode
     */
    function toggleEdit() {
        document.body.classList.toggle('form-rendered', editing);
        return editing = !editing;
    }

    var setFormData = '[{"type":"text","label":"Full Name","subtype":"text","className":"form-control","name":"text-1476748004559"},{"type":"select","label":"Occupation","className":"form-control","name":"select-1476748006618","values":[{"label":"Street Sweeper","value":"option-1","selected":true},{"label":"Moth Man","value":"option-2"},{"label":"Chemist","value":"option-3"}]},{"type":"textarea","label":"Short Bio","rows":"5","className":"form-control","name":"textarea-1476748007461"}]';

    var formBuilder = $('.build-wrap').formBuilder(fbOptions);
    var fbPromise = formBuilder.promise;

    fbPromise.then(function (fb) {
        var apiBtns = {
            showData: fb.actions.showData,
            clearFields: fb.actions.clearFields,
            getData: function () {
                alert(fb.actions.getData());
                console.log(fb.actions.getData());
            },
            setData: function () {
                fb.actions.setData(setFormData);
            },
            addField: function () {
                var field = {
                    type: 'text',
                    class: 'form-control',
                    label: 'Text Field added at: ' + new Date().getTime()
                };
                fb.actions.addField(field);
            },
            removeField: function () {
                fb.actions.removeField();
            },
            testSubmit: function () {
                var formData = new FormData(document.forms[0]);
                console.log('Can submit: ', document.forms[0].checkValidity());
                // Display the key/value pairs
                console.log('FormData:', formData);
                for (var pair of formData.entries()) {
                    console.log(pair[0] + ': ' + pair[1]);
                }
            },
            resetDemo: function () {
                window.sessionStorage.removeItem('formData');
                location.reload();
            }
        };

        Object.keys(apiBtns).forEach(function (action) {
            document.getElementById(action)
                .addEventListener('click', function (e) {
                    apiBtns[action]();
                });
        });



        document.getElementById('getXML').addEventListener('click', function () {
            alert(formBuilder.actions.getData('xml'));
        });
        document.getElementById('getJSON').addEventListener('click', function () {
            alert(formBuilder.actions.getData('json', true));

        });
        document.getElementById('getJS').addEventListener('click', function () {
            alert('check console');
            console.log(formBuilder.actions.getData());
        });

        document.getElementById('renderHTML').addEventListener('click', function () {
            // alert('check console');
            console.log(formBuilder.actions.getData());

            $('.render-wrap').formRender({
                formData: formBuilder.actions.getData(),
                templates: templates
            });
        });
        document.getElementById('RetrieveHTML').addEventListener('click', function () {
            var DATAURL = window.location.origin + "/Home/gethtml";
            $.ajax({
                type: "GET",
                url: DATAURL,
                //contentType: "application/json; charset=utf-8",
                //dataType: "json",
                success: function (response) {
                    if (response !== null) {
                        console.log(response.DesignHTML);
                        $('.render-wrap').formRender({
                            formData: response.DesignHTML,
                            templates: templates
                        });
                    } else {
                        alert("Something went wrong");
                    }
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });



        });


        document.getElementById('showHTML').addEventListener('click', function () {
            //alert(formBuilder.actions.getData('json',true));
            var escapeEl = document.createElement('textarea'),
                code = document.getElementById('markup'),
                escapeHTML = function (html) {
                    escapeEl.textContent = html;
                    return escapeEl.innerHTML;
                },
                formData = formBuilder.actions.getData('json', true),
                addLineBreaks = function (html) {
                    return html.replace(new RegExp('&gt; &lt;', 'g'), '&gt;\n&lt;').replace(new RegExp('&gt;&lt;', 'g'), '&gt;\n&lt;');
                };

            // Grab markup and escape it
            var $markup = $('<div/>');
            $markup.formRender({ formData });

            // set < code > innerHTML with escaped markup
            code.innerHTML = addLineBreaks(escapeHTML($markup[0].innerHTML));

            var contentFull = "THIS IS <h> HTML ";
            var url2 = window.location.origin + "/Home/SaveHTML";
            $.ajax({
                url: url2,
                type: 'POST',
                async: true,
                data: {
                    Content: JSON.stringify(formBuilder.actions.getData())
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(XMLHttpRequest);
                    console.log(textStatus);
                    console.log(errorThrown);
                    alert(XMLHttpRequest);
                    alert(textStatus);
                    alert(errorThrown);
                    alert("Error while posting SendResult");
                },
                success: function (result) {
                    alert("Yey?");
                }
            });

        });
    });

    document.getElementById('edit-form').onclick = function () {
        toggleEdit();
    };





});

//jQuery(function ($) {
//    var escapeEl = document.createElement('textarea'),
//        code = document.getElementById('markup'),
//        escapeHTML = function (html) {
//            escapeEl.textContent = html;
//            return escapeEl.innerHTML;
//        },
//        formData = '[{"type":"textarea","label":"Text Area","className":"form-control","name":"textarea-1492616908223","subtype":"textarea"},{"type":"select","label":"Select","className":"form-control","name":"select-1492616913781","values":[{"label":"Option 1","value":"option-1","selected":true},{"label":"Option 2","value":"option-2"},{"label":"Option 3","value":"option-3"}]},{"type":"checkbox-group","label":"Checkbox Group","name":"checkbox-group-1492616915392","values":[{"label":"Option 1","value":"option-1","selected":true}]}]',
//        addLineBreaks = function (html) {
//            return html.replace(new RegExp('&gt; &lt;', 'g'), '&gt;\n&lt;').replace(new RegExp('&gt;&lt;', 'g'), '&gt;\n&lt;');
//        };

//    // Grab markup and escape it
//    var $markup = $('<div/>');
//    $markup.formRender({ formData });

//    // set < code > innerHTML with escaped markup
//    code.innerHTML = addLineBreaks(escapeHTML($markup[0].innerHTML));

//});