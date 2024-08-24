class_name Confirm

static var exts: CSharpScript = load(
	GdHelper.get_plugin_path() + "/wrappers/ConfirmWrapper.cs"
).new()


static func is_null(actual, message := ""):
	exts.ConfirmNull(actual, message)

	return actual


static func is_not_null(actual, message := ""):
	exts.ConfirmNotNull(actual, message)

	return actual
