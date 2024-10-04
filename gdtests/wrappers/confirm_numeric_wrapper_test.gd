class_name ConfirmNumericWrapperTest extends TestClass

func confirm_is_positive_when_is_positive() -> void:
	ConfirmNumeric.is_positive(2137)
	ConfirmNumeric.is_positive(69.00)


func confirm_is_not_positive_when_is_not_positive() -> void:
	ConfirmNumeric.is_not_positive(-2137)
	ConfirmNumeric.is_not_positive(-69.00)


func confirm_is_negative_when_is_negative() -> void:
	ConfirmNumeric.is_negative(-2137)
	ConfirmNumeric.is_negative(-69.00)


func confirm_is_not_negative_when_is_not_negative() -> void:
	ConfirmNumeric.is_not_negative(2137)
	ConfirmNumeric.is_not_negative(69.00)


func confirm_sign() -> void:
	ConfirmNumeric.sign(5, false)
	ConfirmNumeric.sign(-5, true)


func confirm_is_zero_when_is_zero() -> void:
	ConfirmNumeric.is_zero(0)


func confirm_is_not_zero_when_is_not_zero() -> void:
	ConfirmNumeric.is_not_zero(1)


func confirm_is_odd_when_is_odd() -> void:
	ConfirmNumeric.is_odd(1)


func confirm_is_even_when_is_even() -> void:
	ConfirmNumeric.is_even(2)


func confirm_close_to_int_when_close_to() -> void:
	ConfirmNumeric.close_to_int(2, 4, 5)


func confirm_close_to_float_when_close_to() -> void:
	ConfirmNumeric.close_to_float(2, 2.1, 0.3)
