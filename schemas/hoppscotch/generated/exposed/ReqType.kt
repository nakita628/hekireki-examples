package hoppscotch.models

enum class ReqType(
    val dbName: String,
) {
    REST("REST"),
    GQL("GQL"),
}
