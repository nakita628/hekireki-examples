package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkspaceOptionsTable : IdTable<String>("\"WorkspaceOptions\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkspaceOptions_workspaceId_fkey\"",
        )
    val namespace = text("namespace")
    val value = jsonb<String>("value", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"WorkspaceOptions_pkey\"")

    init {
        index("\"WorkspaceOptions_workspaceId_namespace_idx\"", false, workspaceId, namespace)
    }
}
