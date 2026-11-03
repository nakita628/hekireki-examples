package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MockServerTable : IdTable<String>("\"MockServer\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val subdomain = text("subdomain").uniqueIndex("\"MockServer_subdomain_key\"")
    val creatorUid =
        optReference(
            "creatorUid",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MockServer_creatorUid_fkey\"",
        )
    val collectionID = text("collectionID")
    val workspaceType = pgEnum("workspaceType", "\"WorkspaceType\"", WorkspaceType.entries, WorkspaceType::dbName)
    val workspaceID = text("workspaceID")
    val delayInMs = integer("delayInMs").default(0)
    val isPublic = bool("isPublic").default(true)
    val isActive = bool("isActive").default(true)
    val hitCount = integer("hitCount").default(0)
    val lastHitAt = pgTimestamptz("lastHitAt", 3).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamptz("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"MockServer_pkey\"")
}
