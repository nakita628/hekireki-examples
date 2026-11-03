package rallly.models

enum class VoteType(
    val dbName: String,
) {
    YES("yes"),
    NO("no"),
    IF_NEED_BE("ifNeedBe"),
}
