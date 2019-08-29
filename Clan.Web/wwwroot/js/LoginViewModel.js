LoginViewModel = function (data) {
    "use strict";

    var self = this,
        inputRememberMe = $("#input-remember-me"),
        formLogin = $("#form-login"),
        buttonLogin = $("#button-login");

    self.Email = ko.observable(data.Email);
    self.Password = ko.observable(data.Password);
    self.RememberMe = ko.observable(data.RememberMe);
    self.ReturnUrl = ko.observable(data.ReturnUrl);

    self.init = function () {
        $(".ui.checkbox").checkbox();

        formLogin.form({
            fields: {
                email: {
                    identifier: "email",
                    rules: [{
                        type: "email",
                        prompt: "Email address is invalid"
                    }]
                },
                password: {
                    identifier: "password",
                    rules: [{
                        type: "empty",
                        prompt: "Password is required"
                    }]
                }
            },
            onSuccess: function (e) {
                e.preventDefault();

                $.ajax({
                    url: "/account/login",
                    type: "POST",
                    data: ko.toJSON(self),
                    cache: false,
                    contentType: "application/json",
                    beforeSend: function () {
                        buttonLogin.addClass("loading");
                    },
                    success: function (json, status, xhr) {
                        if (xhr.readyState === 4) {
                            if (json.status === 200) {
                                window.location = json.redirect;
                            } else if (json.status === 403) {
                                window.location = "/account/locked";
                            } else {
                                formLogin.form("add errors", ["Email or password was incorrect"]);
                            }
                        }
                    },
                    complete: function (xhr) {
                        buttonLogin.removeClass("loading");
                    }
                });
            }
        });
    };

    self.rememberMeChanged = function () {
        self.RememberMe(inputRememberMe.prop("checked"));
    };

    self.login = function () {
        formLogin.form("submit");
    };

    self.init();
};