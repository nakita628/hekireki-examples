package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IngestionTemplateTable : IdTable<String>("\"IngestionTemplate\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId").nullable()
    val slug = text("slug")
    val sourceType = text("sourceType")
    val displayName = text("displayName")
    val description = text("description").nullable()
    val iconAsset = text("iconAsset").nullable()
    val credentialSchema = text("credentialSchema").nullable()
    val ottlRules = text("ottlRules").default("")
    val platformPublished = bool("platformPublished").default(false)
    val enabled = bool("enabled").default(true)
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"IngestionTemplate_pkey\"")

    init {
        index("\"IngestionTemplate_organizationId_archivedAt_idx\"", false, organizationId, archivedAt)
        index("\"IngestionTemplate_sourceType_idx\"", false, sourceType)
        uniqueIndex("\"IngestionTemplate_organizationId_slug_key\"", organizationId, slug)
    }
}
