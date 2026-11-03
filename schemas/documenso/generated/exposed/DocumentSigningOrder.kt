package documenso.models

enum class DocumentSigningOrder(
    val dbName: String,
) {
    PARALLEL("PARALLEL"),
    SEQUENTIAL("SEQUENTIAL"),
}
