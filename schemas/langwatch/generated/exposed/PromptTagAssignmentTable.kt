package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PromptTagAssignmentTable : IdTable<String>("\"PromptTagAssignment\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val configId = text("configId")
    val versionId = text("versionId")
    val tagId = text("tagId")
    val projectId = text("projectId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"PromptTagAssignment_pkey\"")

    init {
        index("\"PromptTagAssignment_configId_idx\"", false, configId)
        index("\"PromptTagAssignment_versionId_idx\"", false, versionId)
        index("\"PromptTagAssignment_projectId_idx\"", false, projectId)
        index("\"PromptTagAssignment_tagId_idx\"", false, tagId)
        uniqueIndex("\"PromptTagAssignment_configId_tagId_key\"", configId, tagId)
    }
}
