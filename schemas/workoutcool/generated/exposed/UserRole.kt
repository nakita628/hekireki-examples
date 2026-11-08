package workoutcool.models

enum class UserRole(
    val dbName: String,
) {
    ADMIN("admin"),
    USER("user"),
}
