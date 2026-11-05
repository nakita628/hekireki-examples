package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AttachmentExportTable : IdTable<String>("\"AttachmentExport\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val surveyId = text("surveyId")
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttachmentExport_workspaceId_fkey\"",
        )
    val requestedById =
        optReference(
            "requestedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttachmentExport_requestedById_fkey\"",
        )
    val status =
        pgEnum("status", "\"AttachmentExportStatus\"", AttachmentExportStatus.entries, AttachmentExportStatus::dbName).default(
            AttachmentExportStatus.QUEUED,
        )
    val filterCriteria = jsonb<String>("filterCriteria", { it }, { it }).nullable()
    val timeZone = text("timeZone")
    val dispatchCount = integer("dispatchCount").default(0)
    val fileCount = integer("fileCount")
    val responseCount = integer("responseCount")
    val includedFileCount = integer("includedFileCount").nullable()
    val totalBytes = long("totalBytes").nullable()
    val truncated = bool("truncated").default(false)
    val storageKey = text("storageKey").nullable()
    val errorCode = text("errorCode").nullable()
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val finishedAt = pgTimestamp("finishedAt", 3).nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AttachmentExport_pkey\"")

    init {
        index("\"AttachmentExport_surveyId_requestedById_created_at_idx\"", false, surveyId, requestedById, createdAt)
        index("\"AttachmentExport_status_created_at_idx\"", false, status, createdAt)
        index("\"AttachmentExport_expiresAt_idx\"", false, expiresAt)
        index("\"AttachmentExport_workspaceId_idx\"", false, workspaceId)
        foreignKey(
            surveyId to SurveyTable.id,
            workspaceId to SurveyTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"AttachmentExport_surveyId_workspaceId_fkey\"",
        )
    }
}
