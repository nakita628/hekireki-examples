package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object UserTable : IdTable<String>("user") {
    override val id = text("id").entityId()
    val firstName = text("firstName").default("")
    val lastName = text("lastName").default("")
    val name = text("name")
    val email = text("email").uniqueIndex("user_email_key")
    val emailVerified = bool("emailVerified")
    val image = text("image").nullable()
    val locale = text("locale").nullable().default("fr")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    val role = pgEnum("role", "\"UserRole\"", UserRole.entries, UserRole::dbName).nullable().default(UserRole.USER)
    val banned = bool("banned").nullable().default(false)
    val banReason = text("banReason").nullable()
    val banExpires = pgTimestamp("banExpires", 3).nullable()
    val isPremium = bool("isPremium").nullable().default(false)
    val onboardingPreferences = jsonb<String>("onboardingPreferences", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "user_pkey")
}
