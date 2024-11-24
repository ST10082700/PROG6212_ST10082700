$(document).ready(function () {
    // Elements
    const hoursWorkedInput = $('#HoursWorked');
    const hourlyRateInput = $('#HourlyRate');
    const totalAmountDisplay = $('<div id="totalAmount" class="form-group"><label>Total Amount:</label><span class="total-value"></span></div>');

    // Insert total amount display after hourly rate
    hourlyRateInput.closest('.form-group').after(totalAmountDisplay);

    // Calculation function
    function calculateTotal() {
        const hours = parseFloat(hoursWorkedInput.val()) || 0;
        const rate = parseFloat(hourlyRateInput.val()) || 0;
        const total = (hours * rate).toFixed(2);

        $('.total-value').text(`R ${total}`);

        // Validate inputs
        validateInput(hoursWorkedInput, 0.5, 24, 'Hours must be between 0.5 and 24');
        validateInput(hourlyRateInput, 0.1, 1000, 'Rate must be between R0.10 and R1000.00');
    }

    // Validation function
    function validateInput($input, min, max, message) {
        const value = parseFloat($input.val());
        const isValid = value >= min && value <= max;

        const errorDiv = $input.siblings('.validation-error');
        if (!isValid) {
            if (!errorDiv.length) {
                $input.after(`<div class="validation-error text-danger">${message}</div>`);
            }
            $input.addClass('is-invalid');
        } else {
            errorDiv.remove();
            $input.removeClass('is-invalid');
        }

        return isValid;
    }

    // Real-time calculation and validation
    hoursWorkedInput.on('input', calculateTotal);
    hourlyRateInput.on('input', calculateTotal);

    // Form submission validation
    $('#claimForm').on('submit', function (e) {
        const hoursValid = validateInput(hoursWorkedInput, 0.5, 24, 'Hours must be between 0.5 and 24');
        const rateValid = validateInput(hourlyRateInput, 0.1, 1000, 'Rate must be between R0.10 and R1000.00');

        if (!hoursValid || !rateValid) {
            e.preventDefault();
            return false;
        }
    });
});