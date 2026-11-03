package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentTable : IdTable<String>("\"Document\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val file = text("file")
    val originalFile = text("originalFile").nullable()
    val type = text("type").nullable()
    val contentType = text("contentType").nullable()
    val storageType =
        pgEnum("storageType", "\"DocumentStorageType\"", DocumentStorageType.entries, DocumentStorageType::dbName).default(
            DocumentStorageType.VERCEL_BLOB,
        )
    val numPages = integer("numPages").nullable()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Document_teamId_fkey\"",
        )
    val ownerId =
        optReference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Document_ownerId_fkey\"",
        )
    val assistantEnabled = bool("assistantEnabled").default(false)
    val advancedExcelEnabled = bool("advancedExcelEnabled").default(false)
    val agentsEnabled = bool("agentsEnabled").default(false)
    val downloadOnly = bool("downloadOnly").default(false)
    val hiddenInAllDocuments = bool("hiddenInAllDocuments").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val folderId =
        optReference(
            "folderId",
            FolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Document_folderId_fkey\"",
        )
    val isExternalUpload = bool("isExternalUpload").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"Document_pkey\"")

    init {
        index("\"Document_ownerId_idx\"", false, ownerId)
        index("\"Document_teamId_idx\"", false, teamId)
        index("\"Document_folderId_idx\"", false, folderId)
        index("\"Document_teamId_folderId_idx\"", false, teamId, folderId)
        index("\"Document_teamId_name_idx\"", false, teamId, name)
        index("\"Document_teamId_hiddenInAllDocuments_idx\"", false, teamId, hiddenInAllDocuments)
    }
}
