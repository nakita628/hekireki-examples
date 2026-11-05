package openpanel.models

enum class ClientType(
    val dbName: String,
) {
    READ("read"),
    WRITE("write"),
    ROOT("root"),
}
