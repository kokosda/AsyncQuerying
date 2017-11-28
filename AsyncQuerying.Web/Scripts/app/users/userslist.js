(function($) {
    $(function () {
        var data = $("form").serialize();

        var checkDelayedQuery = function () {
            $.ajax({
                url: "/userslist/list",
                data: data,
                type: "GET",
                cache: false,
                success: function (responseData) {
                    if (responseData && responseData.Filter && responseData.Filter.DelayedQueryState) {
                        var delayedQueryState = responseData.Filter.DelayedQueryState;
                        if (delayedQueryState.StateString == "Completed") {
                            document.cookie = data;
                            window.location.replace("/");
                        }
                        else if (delayedQueryState.StateString == "Failed") {
                            showError("Ошибка при выполнении запроса к данным. Повторите запрос.");
                        }
                        else {
                            setTimeout(checkDelayedQuery, 50);
                        }
                    }
                    else {
                        showError("Неожиданный формат ответа.");
                    }
                },
                error: function (responseData) {
                    
                }
            });
        };

        var showError = function (message) {
            if (typeof message == "string") {
                var errorsHolder = $(".errorsholder");
                errorsHolder.find(".alert").text(message);
                errorsHolder.removeClass("hidden");
            }
            else {
                log("Cannot show error in UI because message type is unsupported.")
            }
        };

        var log = function (message) {
            if (window["console"]) {
                console.log(message);
            }
        };

        checkDelayedQuery();
    });
})(jQuery)