package documenso.models

enum class DocumentDataType(
    val dbName: String,
) {
    S3_PATH("S3_PATH"),
    BYTES("BYTES"),
    BYTES_64("BYTES_64"),
}
