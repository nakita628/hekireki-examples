package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProfileBuilderTable : IdTable<String>("\"ProfileBuilder\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val deleted = bool("deleted").nullable().default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProfileBuilder_workspaceId_fkey\"",
        )
    val intermediateStorageCredentials = jsonb<String>("intermediateStorageCredentials", { it }, { it })
    val destinationId =
        optReference(
            "destinationId",
            ConfigurationObjectTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProfileBuilder_destinationId_fkey\"",
        )
    val connectionOptions = jsonb<String>("connectionOptions", { it }, { it }).nullable()
    val version = integer("version").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"ProfileBuilder_pkey\"")
}
