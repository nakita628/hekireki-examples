package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PublishedDocsTable : IdTable<String>("\"PublishedDocs\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val slug = text("slug")
    val title = text("title")
    val collectionID = text("collectionID")
    val creatorUid = text("creatorUid")
    val version = text("version")
    val autoSync = bool("autoSync")
    val documentTree = jsonb<String>("documentTree", { it }, { it }).nullable()
    val workspaceType = pgEnum("workspaceType", "\"WorkspaceType\"", WorkspaceType.entries, WorkspaceType::dbName)
    val workspaceID = text("workspaceID")
    val environmentID = text("environmentID").nullable()
    val environmentName = text("environmentName").nullable()
    val environmentVariables = jsonb<String>("environmentVariables", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PublishedDocs_pkey\"")

    init {
        index("\"PublishedDocs_collectionID_idx\"", false, collectionID)
        uniqueIndex("\"PublishedDocs_slug_version_key\"", slug, version)
    }
}
