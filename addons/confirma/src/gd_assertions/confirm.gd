class_name Confirm


static func is_null(obj, message := ""):
	if (obj != null):
		Confirma.emit_signal(
			"GdAssertionFailed", 
			"Expected null but got '%s'." % obj \
			if message.is_empty() else message
		)
	return obj


static func is_not_null(obj, message := ""):
	if (obj == null):
		Confirma.emit_signal(
			"GdAssertionFailed", 
			"Expected a non-null value." \
			if message.is_empty() else message
		)
	return obj
