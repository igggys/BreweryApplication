let ThisRegistrationPage = new function () {

    this.CountriesIsShow = false;
    this.CountryCodeSelectedIndex = 0;
    this.CountryCodeSelectedText = countryCodes[0].DisplayText;
    this.CountryCodeIsChanged = false;

    this.DisplaySelectedCode = function () {
        if (this.CountryCodeSelectedIndex > -1) {
            document.getElementById("CountryCode").options[this.CountryCodeSelectedIndex].innerHTML = this.CountryCodeSelectedText;
        }
        this.CountryCodeSelectedIndex = document.getElementById("CountryCode").selectedIndex;
        this.CountryCodeSelectedText = document.getElementById("CountryCode").selectedOptions[0].innerHTML;
        document.getElementById("CountryCode").selectedOptions[0].innerHTML = document.getElementById("CountryCode").selectedOptions[0].value;
        this.CountryCodeIsChanged = !this.CountryCodeIsChanged;
    }

    this.CountryCodesOpen = function () {
        if (this.CountryCodeSelectedIndex > -1 && !this.CountryCodeIsChanged) {
            document.getElementById("CountryCode").options[this.CountryCodeSelectedIndex].innerHTML = this.CountryCodeSelectedText;
            this.CountryCodeSelectedIndex = -1;
            this.CountryCodeSelectedText = "";
            document.getElementById("CountryCode").selectedIndex = -1;
        }
        else {
            this.CountryCodeIsChanged = !this.CountryCodeIsChanged;
        }
    }

    this.CountryCodesOnBlur = function () {
        if (document.getElementById("CountryCode").selectedIndex == -1) {
            this.CountryCodeSelectedIndex = 0;
            this.CountryCodeSelectedText = countryCodes[0].DisplayText;
            this.CountryCodeIsChanged = false;
            document.getElementById("CountryCode").selectedIndex = -1;
        }
    }

    this.Start = function () {
        document.getElementById("CountryCode").selectedIndex = -1;
    }

    this.PhoneInput = function () {
        let PhoneNumberTextBox = document.getElementById("PhoneNumberTextBox");
        let currentValue = PhoneNumberTextBox.value;
        let numbersOnly = currentValue.replace(/[^\d]/g, '');
        if (currentValue != numbersOnly)
            PhoneNumberTextBox.value = numbersOnly;
    }
}