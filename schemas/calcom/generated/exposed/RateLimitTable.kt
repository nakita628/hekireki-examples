package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object RateLimitTable : IdTable<String>("\"RateLimit\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val apiKeyId =
        reference(
            "apiKeyId",
            ApiKeyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RateLimit_apiKeyId_fkey\"",
        )
    val ttl = integer("ttl")
    val limit = integer("limit")
    val blockDuration = integer("blockDuration")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RateLimit_pkey\"")

    init {
        index("\"RateLimit_apiKeyId_idx\"", false, apiKeyId)
    }
}
