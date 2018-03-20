$(document).ready(function(){
    "use strict";

    // Options for Message
    //----------------------------------------------
    var options = {
        'btn-loading': '<i class="fa fa-spinner fa-pulse"></i>',
        'btn-success': '<i class="fa fa-check"></i>',
        'btn-error': '<i class="fa fa-remove"></i>',
        'msg-success': 'All Good! Redirecting...',
        'msg-error': 'Wrong login credentials!',
        'useAJAX': true,
    };

    // Login Form
    //----------------------------------------------
    // Validation
    $("#login-form").validate({
        rules: {
            lg_username: "required",
            lg_password: "required",
        },
        errorClass: "form-invalid"
    });

    // Form Submission
    $("#login-form").submit(function(e) {
        e.preventDefault();

        remove_loading($(this));
        login($(this));
        return false;
    });

    // Register Form
    //----------------------------------------------
    // Validation
    
    $("#register-form").validate({
        rules: {
            reg_username: "required",
            reg_password: {
                required: true,
                minlength: 5
            },
            reg_password_confirm: {
                required: true,
                minlength: 5,
                equalTo: "#register-form [name=reg_password]"
            },
            reg_email: {
                required: true,
                email: true
            },
            reg_agree: "required",
        },
        errorClass: "form-invalid",
        errorPlacement: function( label, element ) {
            if( element.attr( "type" ) === "checkbox" || element.attr( "type" ) === "radio" ) {
                element.parent().append( label ); // this would append the label after all your checkboxes/labels (so the error-label will be the last element in <div class="controls"> )
            }
            else {
                label.insertAfter( element ); // standard behaviour
            }
        }
    });
    
    // Form Submission
    $("#register-form").submit(function(e) {
        e.preventDefault();

        remove_loading($(this));
        register($(this));
        return false;
    });

    // Forgot Password Form
    //----------------------------------------------
    // Validation
    
    $("#forgot-password-form").validate({
        rules: {
            fp_email: "required",
        },
        errorClass: "form-invalid"
    });
    
    // Form Submission
    $("#forgot-password-form").submit(function() {
        remove_loading($(this));

        if(options['useAJAX'] == true)
        {
            // Dummy AJAX request (Replace this with your AJAX code)
            // If you don't want to use AJAX, remove this
            dummy_submit_form($(this));

            // Cancel the normal submission.
            // If you don't want to use AJAX, remove this
            return false;
        }
    });

    // Loading
    //----------------------------------------------
    function remove_loading($form)
    {
        $form.find('[type=submit]').removeClass('error success');
        $form.find('.login-form-main-message').removeClass('show error success').html('');
    }

    function form_loading($form)
    {
        $form.find('[type=submit]').addClass('clicked').html(options['btn-loading']);
    }

    function form_success($form)
    {
        $form.find('[type=submit]').addClass('success').html(options['btn-success']);
        $form.find('.login-form-main-message').addClass('show success').html(options['msg-success']);

        //document.location.href = "decks.html";
    }

    function form_failed($form, msg)
    {
        $form.find('[type=submit]').addClass('error').html(options['btn-error']);
        $form.find('.login-form-main-message').addClass('show error').html(msg);
    }

    // Dummy Submit Form (Remove this)
    //----------------------------------------------
    // This is just a dummy form submission. You should use your AJAX function or remove this function if you are not using AJAX.

    /*
    * Fetches a resource from and endpoint that expects authorization.
    * Achieves this by automatically appending the firebase id token
    * for the currently signed in user to the request header. Returns a promise
    * .then(function(result)): result=response
    * .catch(function(error)): error = either firebase error or fetch error
    * Params: [url] = authenticated endpoint, [fetchProps] = optional properties to use with the fetch call
    * For more information on how to use fetch: "https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API/Using_Fetch"
    */
    function fetchWithAuth(url, fetchProps = {}) {
        return new Promise(function (resolve, reject) {
            try {
                firebase.auth().currentUser.getIdToken(true).then(function (idToken) {
                    if (!fetchProps.headers) fetchProps.headers = {};
                    fetchProps.headers['Authorization'] = "Bearer " + idToken;
                    fetch(url, fetchProps).then(function (result) {
                        resolve(result);
                    }).catch(function (error) {
                        reject(error);
                    });
                })
            } catch (error) {
                reject(error);
            }
        })
    }

    /*
    * Returns a promise that finds a list of image links for the given card name.
    * result will be a json Object containing the following properties:
    * {
    *   small: string,
        normal: string,
        large: string,
        png: string,
        art_crop: string,
        border_crop: string
    * }
    */
    function getCardImage(cardName) {
        const scryImageSearchURI = "https://api.scryfall.com/cards/named?exact=";
        return new Promise(function (resolve, reject) {
            try {
                fetch(scryImageSearchURI+cardName).then(function(result) {
                    return result.json()
                }).then(function(json){
                    resolve(json.image_uris)
                }).catch(function(error){
                    reject(error)
                })
            } catch(error) {
                reject(error)
            }
        })
    }

    function login($form)
    {
        if($form.valid()) 
        {
            form_loading($form);

            var email = $('#username').val();
            var password = $('#password').val();

            firebase.auth().signInWithEmailAndPassword(email, password)
                .then(function () {
                    if (!firebase.auth().currentUser.emailVerified) {
                        firebase.auth().signOut()
                        form_failed($form, "Email must be verified")
                    }
                    else {
                        fetchWithAuth("http://localhost:5000/api/login").then(form_success($form))
                    }
                })
                .catch(function(error) {
                    // Handle Errors here.
                    var errorCode = error.code;
                    var errorMessage = error.message;

                    form_failed($form, errorMessage);
                });
        }
    }

    function register($form) {
        if($form.valid())
        {
            form_loading($form);

            var email = $('#reg_username').val();
            var password = $('#reg_password').val();

            firebase.auth().createUserWithEmailAndPassword(email, password)
                .then(function () {
                    firebase.auth().currentUser.sendEmailVerification().then(function () {
                        form_success($form);
                        firebase.auth().signOut()
                    });
                })
                .catch(function(error) {
                    // Handle Errors here.
                    var errorCode = error.code;
                    var errorMessage = error.message;

                    form_failed($form, errorMessage);
                });
        }
    }
});
