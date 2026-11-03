package calcom.models

enum class CreationSource(
    val dbName: String,
) {
    API_V1("api_v1"),
    API_V2("api_v2"),
    WEBAPP("webapp"),
}
