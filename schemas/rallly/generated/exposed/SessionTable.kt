package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionTable : IdTable<String>("sessions") {
    override val id = text("id").entityId()
    val expiresAt = pgTimestamp("expires_at", 3)
    val token = text("token").uniqueIndex("sessions_token_key")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val ipAddress = text("ip_address").nullable()
    val userAgent = text("user_agent").nullable()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sessions_user_id_fkey",
        )
    val impersonatedBy =
        optReference(
            "impersonated_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sessions_impersonated_by_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "sessions_pkey")
}
