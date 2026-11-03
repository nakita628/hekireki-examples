package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomDocumentTable : IdTable<String>("\"DataroomDocument\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomDocument_dataroomId_fkey\"",
        )
    val documentId =
        reference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomDocument_documentId_fkey\"",
        )
    val folderId =
        optReference(
            "folderId",
            DataroomFolderTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomDocument_folderId_fkey\"",
        )
    val orderIndex = integer("orderIndex").nullable()
    val hierarchicalIndex = text("hierarchicalIndex").nullable()
    val vectorStoreFileId = text("vectorStoreFileId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataroomDocument_pkey\"")

    init {
        index("\"DataroomDocument_folderId_idx\"", false, folderId)
        index("\"DataroomDocument_dataroomId_folderId_orderIndex_idx\"", false, dataroomId, folderId, orderIndex)
        index("\"DataroomDocument_documentId_idx\"", false, documentId)
        uniqueIndex("\"DataroomDocument_dataroomId_documentId_key\"", dataroomId, documentId)
    }
}
