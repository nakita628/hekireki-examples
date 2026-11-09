package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConfigurationObjectTable : IdTable<String>("\"ConfigurationObject\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val deleted = bool("deleted").nullable().default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = text("type")
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConfigurationObject_workspaceId_fkey\"",
        )
    val config = jsonb<String>("config", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ConfigurationObject_pkey\"")

    init {
        index("\"ConfigurationObject_updatedAt_idx\"", false, functions = listOf(indexExpression("\"updatedAt\" DESC")))
    }
}
