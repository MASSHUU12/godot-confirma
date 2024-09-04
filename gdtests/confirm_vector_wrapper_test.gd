class_name ConfirmVectorWrapperTest

func confirm_equal_approx_when_equal_approx() -> void:
	ConfirmVector.equal_approx_2(
		Vector2(1.0, 2.0),
		Vector2(1.0, 2.0),
		0.0001
	)


func confirm_not_equal_approx_when_not_equal_approx() -> void:
	ConfirmVector.not_equal_approx_2(
		Vector2(1.0, 2.0),
		Vector2(1.1, 2.1),
		0.0001
	)


func confirm_less_than_when_less_than() -> void:
	ConfirmVector.less_than_2(Vector2(-5.0, -5.0), Vector2(0.0, -2.0))


func confirm_less_than_or_equal_when_less_than_or_equal() -> void:
	ConfirmVector.less_than_or_equal_2(
		Vector2(1.0, 2.0),
		Vector2(1.0, 2.0)
	)


func confirm_greater_than_when_greater_than() -> void:
	ConfirmVector.greater_than_2(Vector2(0.0, -2.0), Vector2(-5.0, -5.0))


func confirm_greater_than_or_equal_when_greater_than_or_equal() -> void:
	ConfirmVector.greater_than_or_equal_2(
		Vector2(1.0, 2.0),
		Vector2(1.0, 2.0)
	)


func confirm_between_when_between() -> void:
	ConfirmVector.between_2(
		Vector2(0.5, 0.5),
		Vector2(0.0, 0.0),
		Vector2(1.0, 1.0)
	)


func confirm_not_between_when_not_between() -> void:
	ConfirmVector.not_between_2(
		Vector2(0.5, 0.5),
		Vector2(0.0, 0.0),
		Vector2(0.4, 0.4)
	)
