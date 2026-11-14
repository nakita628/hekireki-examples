package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyTable : IdTable<String>("\"ApiKey\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val lookupId = text("lookupId").uniqueIndex("\"ApiKey_lookupId_key\"")
    val hashedSecret = text("hashedSecret")
    val permissionMode = text("permissionMode").default("all")
    val userId = text("userId").nullable()
    val createdByUserId = text("createdByUserId").nullable()
    val organizationId = text("organizationId")
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revocationCause = text("revocationCause").nullable()
    val parentApiKeyId = text("parentApiKeyId").nullable()
    val ingestSourceType = text("ingestSourceType").nullable()
    val ingestionTemplateId = text("ingestionTemplateId").nullable()
    val createdByDeviceLabel = text("createdByDeviceLabel").nullable()
    val isSystemManaged = bool("isSystemManaged").default(false)
    val createdByBrowserSessionId = text("createdByBrowserSessionId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ApiKey_pkey\"")

    init {
        index("\"ApiKey_userId_idx\"", false, userId)
        index("\"ApiKey_organizationId_idx\"", false, organizationId)
        index("\"ApiKey_lookupId_idx\"", false, lookupId)
        index("\"ApiKey_ingestSourceType_idx\"", false, ingestSourceType)
        index("\"ApiKey_ingestionTemplateId_idx\"", false, ingestionTemplateId)
    }
}
