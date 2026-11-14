package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PromptTagTable : IdTable<String>("\"PromptTag\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val name = text("name")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"PromptTag_pkey\"")

    init {
        index("\"PromptTag_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"PromptTag_organizationId_name_key\"", organizationId, name)
    }
}
