package formbricks.models

enum class ApiKeyPermission(
    val dbName: String,
) {
    READ("read"),
    WRITE("write"),
    MANAGE("manage"),
}
