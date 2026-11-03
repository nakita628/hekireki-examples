package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ContactAttributeKeyTable : IdTable<String>("\"ContactAttributeKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val isUnique = bool("isUnique").default(false)
    val key = text("key")
    val name = text("name").nullable()
    val description = text("description").nullable()
    val type =
        pgEnum("type", "\"ContactAttributeType\"", ContactAttributeType.entries, ContactAttributeType::dbName).default(
            ContactAttributeType.CUSTOM,
        )
    val dataType =
        pgEnum("dataType", "\"ContactAttributeDataType\"", ContactAttributeDataType.entries, ContactAttributeDataType::dbName).default(
            ContactAttributeDataType.STRING,
        )
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ContactAttributeKey_workspaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ContactAttributeKey_pkey\"")

    init {
        index("\"ContactAttributeKey_workspaceId_created_at_idx\"", false, workspaceId, createdAt)
        uniqueIndex("\"ContactAttributeKey_key_workspaceId_key\"", key, workspaceId)
    }
}
