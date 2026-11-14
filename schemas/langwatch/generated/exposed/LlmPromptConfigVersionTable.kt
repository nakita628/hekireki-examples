package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LlmPromptConfigVersionTable : IdTable<String>("\"LlmPromptConfigVersion\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val version = integer("version")
    val commitMessage = text("commitMessage").nullable()
    val authorId = text("authorId").nullable()
    val configId = text("configId")
    val configData = jsonb<String>("config", { it }, { it })
    val schemaVersion = text("schemaVersion")
    val runtimeParameters = jsonb<String>("runtimeParameters", { it }, { it }).default("{}")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId = text("projectId")
    override val primaryKey = PrimaryKey(id, name = "\"LlmPromptConfigVersion_pkey\"")

    init {
        index("\"LlmPromptConfigVersion_configId_idx\"", false, configId)
        index("\"LlmPromptConfigVersion_authorId_idx\"", false, authorId)
        index("\"LlmPromptConfigVersion_createdAt_idx\"", false, createdAt)
        uniqueIndex("\"LlmPromptConfigVersion_configId_version_key\"", configId, version)
    }
}
