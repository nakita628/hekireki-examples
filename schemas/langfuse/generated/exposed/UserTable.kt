package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("users") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name").nullable()
    val email = text("email").nullable().uniqueIndex("users_email_key")
    val emailVerified = pgTimestamp("email_verified", 3).nullable()
    val password = text("password").nullable()
    val sessionsExpiredAt = pgTimestamp("sessions_expired_at", 3).nullable()
    val image = text("image").nullable()
    val admin = bool("admin").default(false)
    val v4BetaEnabled = bool("v4_beta_enabled").default(false)
    val supportAccess = jsonb<String>("support_access", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val featureFlags = array("feature_flags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "users_pkey")
}
