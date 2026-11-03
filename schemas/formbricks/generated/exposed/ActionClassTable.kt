package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ActionClassTable : IdTable<String>("\"ActionClass\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val description = text("description").nullable()
    val type = pgEnum("type", "\"ActionType\"", ActionType.entries, ActionType::dbName)
    val key = text("key").nullable()
    val noCodeConfig = jsonb<String>("noCodeConfig", { it }, { it }).nullable()
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ActionClass_workspaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ActionClass_pkey\"")

    init {
        index("\"ActionClass_workspaceId_created_at_idx\"", false, workspaceId, createdAt)
        uniqueIndex("\"ActionClass_key_workspaceId_key\"", key, workspaceId)
        uniqueIndex("\"ActionClass_name_workspaceId_key\"", name, workspaceId)
    }
}
