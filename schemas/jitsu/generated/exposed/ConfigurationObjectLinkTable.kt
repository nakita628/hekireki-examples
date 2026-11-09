package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConfigurationObjectLinkTable : IdTable<String>("\"ConfigurationObjectLink\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val deleted = bool("deleted").nullable().default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = text("type").nullable().default("push")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConfigurationObjectLink_workspaceId_fkey\"",
        )
    val fromId =
        reference(
            "fromId",
            ConfigurationObjectTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConfigurationObjectLink_fromId_fkey\"",
        )
    val toId =
        reference(
            "toId",
            ConfigurationObjectTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConfigurationObjectLink_toId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ConfigurationObjectLink_pkey\"")

    init {
        index("\"ConfigurationObjectLink_updatedAt_idx\"", false, functions = listOf(indexExpression("\"updatedAt\" DESC")))
        index("\"ConfigurationObjectLink_workspaceId_idx\"", false, workspaceId)
    }
}
