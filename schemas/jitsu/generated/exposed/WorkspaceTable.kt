package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkspaceTable : IdTable<String>("\"Workspace\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val deleted = bool("deleted").default(false)
    val slug = text("slug").nullable().uniqueIndex("\"Workspace_slug_key\"")
    val featuresEnabled = array("featuresEnabled", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "\"Workspace_pkey\"")

    init {
        index("\"Workspace_slug_idx\"", false, slug)
        index("\"Workspace_updatedAt_idx\"", false, functions = listOf(indexExpression("\"updatedAt\" DESC")))
    }
}
