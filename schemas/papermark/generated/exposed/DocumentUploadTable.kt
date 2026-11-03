package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentUploadTable : IdTable<String>("\"DocumentUpload\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val documentId =
        reference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_documentId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_teamId_fkey\"",
        )
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_viewerId_fkey\"",
        )
    val viewId =
        optReference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_viewId_fkey\"",
        )
    val linkId =
        reference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_linkId_fkey\"",
        )
    val dataroomId =
        optReference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_dataroomId_fkey\"",
        )
    val dataroomDocumentId =
        optReference(
            "dataroomDocumentId",
            DataroomDocumentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_dataroomDocumentId_fkey\"",
        )
    val taskId =
        optReference(
            "taskId",
            TaskTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentUpload_taskId_fkey\"",
        )
    val originalFilename = text("originalFilename").nullable()
    val fileSize = long("fileSize").nullable()
    val numPages = integer("numPages").nullable()
    val mimeType = text("mimeType").nullable()
    val uploadedAt = pgTimestamp("uploadedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentUpload_pkey\"")

    init {
        index("\"DocumentUpload_documentId_idx\"", false, documentId)
        index("\"DocumentUpload_viewerId_idx\"", false, viewerId)
        index("\"DocumentUpload_viewId_idx\"", false, viewId)
        index("\"DocumentUpload_linkId_idx\"", false, linkId)
        index("\"DocumentUpload_teamId_idx\"", false, teamId)
        index("\"DocumentUpload_dataroomId_idx\"", false, dataroomId)
        index("\"DocumentUpload_dataroomDocumentId_idx\"", false, dataroomDocumentId)
        index("\"DocumentUpload_taskId_idx\"", false, taskId)
    }
}
