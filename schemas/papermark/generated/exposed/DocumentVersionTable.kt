package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentVersionTable : IdTable<String>("\"DocumentVersion\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val versionNumber = integer("versionNumber")
    val documentId =
        reference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentVersion_documentId_fkey\"",
        )
    val file = text("file")
    val originalFile = text("originalFile").nullable()
    val type = text("type").nullable()
    val contentType = text("contentType").nullable()
    val fileSize = long("fileSize").nullable()
    val storageType =
        pgEnum("storageType", "\"DocumentStorageType\"", DocumentStorageType.entries, DocumentStorageType::dbName).default(
            DocumentStorageType.VERCEL_BLOB,
        )
    val numPages = integer("numPages").nullable()
    val isPrimary = bool("isPrimary").default(false)
    val isVertical = bool("isVertical").default(false)
    val fileId = text("fileId").nullable()
    val vectorStoreFileId = text("vectorStoreFileId").nullable()
    val hasPages = bool("hasPages").default(false)
    val length = integer("length").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DocumentVersion_pkey\"")

    init {
        index("\"DocumentVersion_documentId_idx\"", false, documentId)
        index("\"DocumentVersion_documentId_isPrimary_idx\"", false, documentId, isPrimary)
        index(
            "\"DocumentVersion_documentId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"documentId\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"DocumentVersion_documentId_isPrimary_createdAt_idx\"", false, documentId, isPrimary, createdAt)
        uniqueIndex("\"DocumentVersion_versionNumber_documentId_key\"", versionNumber, documentId)
    }
}
