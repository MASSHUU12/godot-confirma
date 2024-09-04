class_name ConfirmRangeWrapperTest

func confirm_in_range_when_in_range() -> void:
	ConfirmRange.in_range_int(5, 0, 6)
	ConfirmRange.in_range_float(5.5, 3.2, 6.8)


func confirm_not_in_range_when_not_in_range() -> void:
	ConfirmRange.not_in_range_int(10, 5, 6)
	ConfirmRange.not_in_range_float(5.5, 5.8, 6.2)


func confirm_greater_than_when_greater_than() -> void:
	ConfirmRange.greater_than_int(5, 3)
	ConfirmRange.greater_than_float(5.5, 3.3)


func confirm_greater_than_or_equal_when_greater_than_or_equal() -> void:
	ConfirmRange.greater_than_or_equal_int(5, 3)
	ConfirmRange.greater_than_or_equal_int(5, 5)
	ConfirmRange.greater_than_or_equal_float(5.5, 3.3)
	ConfirmRange.greater_than_or_equal_float(5.5, 5.5)


func confirm_less_than_when_less_than() -> void:
	ConfirmRange.less_than_int(3, 5)
	ConfirmRange.less_than_float(3.3, 5.5)


func confirm_less_than_or_equal_when_less_than_or_equal() -> void:
	ConfirmRange.less_than_or_equal_int(3, 5)
	ConfirmRange.less_than_or_equal_int(5, 5)
	ConfirmRange.less_than_or_equal_float(3.3, 5.5)
	ConfirmRange.less_than_or_equal_float(5.5, 5.5)
