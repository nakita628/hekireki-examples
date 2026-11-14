package langwatch.models

enum class VirtualKeyPurpose(
    val dbName: String,
) {
    USER("USER"),
    LANGY("LANGY"),
    CONNECT("CONNECT"),
}
