

            var $calc = $('#calculator');
            var $button = $('#calculate-total');
            var $treatmentCost = $('#cost');
            var $downPayment = $('#down-payment');
            var $insuranceCoverage = $('#insurance-coverage');
            var $rangeSlider = $('#financing-term');
            var $expectedPayment = $('#expected-payment');
            var $estimated = $('#estimate');
    
            var $ft = $("#financing-term");
    
            $ft.ionRangeSlider({
                min: 6,
                max: 24,
                grid: true,
                grid_snap: true,
                hide_min_max: true,
                from: 6,
                step: 6,
                skin: "round",
            });
    
            $ftInstance = $ft.data("ionRangeSlider");
    
            $expectedPayment.on("propertychange input", function (e) {
                var val = parseFloat($expectedPayment.val());
    
                if (val > 0) {
                    $ftInstance.update({
                        disable: true
                    });
                } else {
                    $ftInstance.update({
                        disable: false
                    });
                }
            });
    
            function calculate() {
    
                var cost = parseFloat($treatmentCost.val());
                if (isNaN(cost) || (cost === "") || (cost < 0)) {
                    cost = 0;
                    $treatmentCost.val(0);
                }
    
                var down = parseFloat($downPayment.val());
                if (isNaN(down) || (down === "") || (down < 0)) {
                    down = 0;
                    $downPayment.val(0);
                }
    
                var insurance = parseFloat($insuranceCoverage.val());
                if (isNaN(insurance) || (insurance === "") || (insurance < 0)) {
                    insurance = 0;
                    $insuranceCoverage.val(0);
                }
    
                var expected = parseFloat($expectedPayment.val());
                if (isNaN(expected) || (expected === "") || (expected < 0)) {
                    expected = 0;
                    $expectedPayment.val(0);
                }
    
                var total = "";
                var from = $ft.prop("value");
                var from2 = $ft.data("from");
    
                if (expected > 0) {
                    total = (cost - down - insurance) / expected;
                    $ftInstance.update({
                        from: total
                    });
                    var result = "";
    
                    if (total > 24) {
                        result = "It will take more than 32 months to pay off.";
                        $estimated.addClass("over");
                    } else {
                        result = "It will take " + Math.round(total) + ' months to pay off.';
                        $estimated.removeClass("over");
                    }
    
                    $estimated.html(result);
                } else {
                    total = (cost - down - insurance) / from2;
                    if (total < 0) {
                        total = 0;
                    }
                    var result = 'Your estimated payment is: $' + Math.round(total) + ' / month.';
                    $estimated.html(result);
    
                    if ($estimated.hasClass('over')) {
                        $estimated.removeClass('over');
                    }
                }
            }
    
            $button.click(function (e) {
                e.preventDefault();
                calculate();
                $button.val("Recalculate Total");
            });
    