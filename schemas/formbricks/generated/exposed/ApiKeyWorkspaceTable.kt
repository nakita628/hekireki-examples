package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyWorkspaceTable : IdTable<String>("\"ApiKeyWorkspace\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val apiKeyId =
        reference(
            "apiKeyId",
            ApiKeyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKeyWorkspace_apiKeyId_fkey\"",
        )
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKeyWorkspace_workspaceId_fkey\"",
        )
    val permission = pgEnum("permission", "\"ApiKeyPermission\"", ApiKeyPermission.entries, ApiKeyPermission::dbName)
    override val primaryKey = PrimaryKey(id, name = "\"ApiKeyWorkspace_pkey\"")

    init {
        index("\"ApiKeyWorkspace_workspaceId_idx\"", false, workspaceId)
        uniqueIndex("\"ApiKeyWorkspace_apiKeyId_workspaceId_key\"", apiKeyId, workspaceId)
    }
}
