package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ViewTable : IdTable<String>("\"View\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val linkId =
        reference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"View_linkId_fkey\"",
        )
    val documentId =
        optReference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"View_documentId_fkey\"",
        )
    val dataroomId =
        optReference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"View_dataroomId_fkey\"",
        )
    val dataroomViewId = text("dataroomViewId").nullable()
    val viewerEmail = text("viewerEmail").nullable()
    val viewerName = text("viewerName").nullable()
    val verified = bool("verified").default(false)
    val viewedAt = pgTimestamp("viewedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val downloadedAt = pgTimestamp("downloadedAt", 3).nullable()
    val downloadType = pgEnum("downloadType", "\"DownloadType\"", DownloadType.entries, DownloadType::dbName).nullable()
    val downloadMetadata = jsonb<String>("downloadMetadata", { it }, { it }).nullable()
    val viewType = pgEnum("viewType", "\"ViewType\"", ViewType.entries, ViewType::dbName).default(ViewType.DOCUMENT_VIEW)
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"View_viewerId_fkey\"",
        )
    val groupId =
        optReference(
            "groupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"View_groupId_fkey\"",
        )
    val isArchived = bool("isArchived").default(false)
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"View_teamId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"View_pkey\"")

    init {
        index("\"View_linkId_idx\"", false, linkId)
        index("\"View_documentId_idx\"", false, documentId)
        index("\"View_dataroomId_idx\"", false, dataroomId)
        index("\"View_dataroomViewId_idx\"", false, dataroomViewId)
        index("\"View_viewerId_idx\"", false, viewerId)
        index("\"View_groupId_idx\"", false, groupId)
        index("\"View_teamId_idx\"", false, teamId)
        index("\"View_viewedAt_idx\"", false, functions = listOf(indexExpression("\"viewedAt\" DESC")))
        index("\"View_viewerId_documentId_idx\"", false, viewerId, documentId)
        index("\"View_viewerEmail_idx\"", false, viewerEmail)
        index("\"View_documentId_isArchived_idx\"", false, documentId, isArchived)
        index(
            "\"View_documentId_viewedAt_idx\"",
            false,
            functions = listOf(indexExpression("\"documentId\""), indexExpression("\"viewedAt\" DESC")),
        )
    }
}
