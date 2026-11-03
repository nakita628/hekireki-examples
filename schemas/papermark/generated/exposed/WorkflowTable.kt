package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowTable : IdTable<String>("\"Workflow\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val entryLinkId =
        reference(
            "entryLinkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Workflow_entryLinkId_fkey\"",
        ).uniqueIndex("\"Workflow_entryLinkId_key\"")
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Workflow_teamId_fkey\"",
        )
    val isActive = bool("isActive").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Workflow_pkey\"")

    init {
        index("\"Workflow_entryLinkId_idx\"", false, entryLinkId)
        index("\"Workflow_teamId_idx\"", false, teamId)
        index("\"Workflow_isActive_idx\"", false, isActive)
    }
}
