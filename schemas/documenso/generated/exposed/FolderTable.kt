package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FolderTable : IdTable<String>("\"Folder\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Folder_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Folder_teamId_fkey\"",
        )
    val pinned = bool("pinned").default(false)
    val parentId =
        optReference(
            "parentId",
            FolderTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Folder_parentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val visibility =
        pgEnum("visibility", "\"DocumentVisibility\"", DocumentVisibility.entries, DocumentVisibility::dbName).default(
            DocumentVisibility.EVERYONE,
        )
    val type = pgEnum("type", "\"FolderType\"", FolderType.entries, FolderType::dbName)
    override val primaryKey = PrimaryKey(id, name = "\"Folder_pkey\"")

    init {
        index("\"Folder_userId_idx\"", false, userId)
        index("\"Folder_teamId_idx\"", false, teamId)
        index("\"Folder_parentId_idx\"", false, parentId)
        index("\"Folder_type_idx\"", false, type)
    }
}
