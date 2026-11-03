package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FolderTable : IdTable<String>("\"Folder\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val path = text("path")
    val parentId =
        optReference(
            "parentId",
            FolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Folder_parentId_fkey\"",
        )
    val hiddenInAllDocuments = bool("hiddenInAllDocuments").default(false)
    val icon = text("icon").nullable()
    val color = text("color").nullable()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Folder_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Folder_pkey\"")

    init {
        index("\"Folder_parentId_idx\"", false, parentId)
        index("\"Folder_teamId_hiddenInAllDocuments_idx\"", false, teamId, hiddenInAllDocuments)
        uniqueIndex("\"Folder_teamId_path_key\"", teamId, path)
    }
}
