package openpanel.models

enum class AccessLevel(
    val dbName: String,
) {
    READ("read"),
    WRITE("write"),
    ADMIN("admin"),
}
