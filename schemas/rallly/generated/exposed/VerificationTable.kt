package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object VerificationTable : IdTable<String>("verifications") {
    override val id = text("id").entityId()
    val identifier = text("identifier").uniqueIndex("verifications_identifier_key")
    val value = text("value")
    val expiresAt = pgTimestamp("expires_at", 3)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "verifications_pkey")

    init {
        index("verifications_identifier_idx", false, identifier)
    }
}
