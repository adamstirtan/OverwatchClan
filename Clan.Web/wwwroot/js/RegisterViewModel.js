RegisterViewModel = function (data) {
    "use strict";

    var self = this,
        formRegister = $("#form-register"),
        buttonRegister = $("#button-register");

    self.Email = ko.observable(data.Email);
    self.Password = ko.observable(data.Password);
    self.ConfirmPassword = ko.observable(data.ConfirmPassword);
    self.BattleTag = ko.observable(data.BattleTag);

    self.init = function () {
        $.fn.form.settings.rules.validBattleTag = function (value) {
            try {
                if (value.indexOf("#") < 0) {
                    return false;
                }
                var split = value.split("#");
                return split.length === 2 && Number.isInteger(Number(split[1]));
            } catch {
                return false;
            }
        };

        formRegister.form({
            fields: {
                email: {
                    identifier: "email",
                    rules: [{
                        type: "email",
                        prompt: "Email address is not valid"
                    }]
                },
                password: {
                    identifier: "password",
                    rules: [{
                        type: "minLength[6]",
                        prompt: "Password must be 6 characters or more"
                    }]
                },
                passwordC: {
                    identifier: "confirm",
                    rules: [{
                        type: "empty",
                        prompt: "Password confirmation is required"
                    }, {
                        type: "match[password]",
                        prompt: "Passwords do not match"
                    }]
                },
                battleTag: {
                    identifier: "battletag",
                    rules: [{
                        type: "validBattleTag",
                        prompt: "BattleTag™ is invalid"
                    }]
                }
            },
            onSuccess: function (e) {
                e.preventDefault();

                $.ajax({
                    url: "/account/register",
                    type: "POST",
                    data: ko.toJSON(self),
                    cache: false,
                    contentType: "application/json",
                    beforeSend: function () {
                        buttonRegister.addClass("loading");
                    },
                    success: function (json, status, xhr) {
                        if (xhr.readyState === 4) {
                            if (json.success) {
                                window.location = json.redirect;
                            } else if (json.status === 409) {
                                formRegister.form("add errors", ["That email address is already in use"]);
                            } else {
                                formRegister.form("add errors", ["There was a problem joining the clan"]);
                            }
                        }
                    },
                    complete: function (xhr) {
                        buttonRegister.removeClass("loading");
                    }
                });
            }
        });
    };

    self.register = function () {
        formRegister.form("submit");
    };

    self.init();
};