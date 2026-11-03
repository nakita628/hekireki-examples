package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PromptVersionTable : IdTable<String>("prompt_versions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val version = integer("version")
    val promptId =
        reference(
            "promptId",
            PromptTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"prompt_versions_promptId_fkey\"",
        )
    val textContent = text("textContent").nullable()
    val chatContent = jsonb<String>("chatContent", { it }, { it }).nullable()
    val model = text("model").nullable()
    val config = jsonb<String>("config", { it }, { it }).nullable()
    val source1 = text("source")
    val commitMessage = text("commitMessage").nullable()
    val contentHash = text("contentHash")
    val labels = array("labels", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val workerId =
        optReference(
            "workerId",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"prompt_versions_workerId_fkey\"",
        )
    val createdBy = text("createdBy").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "prompt_versions_pkey")

    init {
        index("\"prompt_versions_promptId_idx\"", false, promptId)
        index("\"prompt_versions_contentHash_idx\"", false, contentHash)
        uniqueIndex("\"prompt_versions_promptId_version_key\"", promptId, version)
    }
}
