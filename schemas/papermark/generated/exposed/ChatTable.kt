package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatTable : IdTable<String>("\"Chat\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val title = text("title").nullable()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_teamId_fkey\"",
        )
    val documentId =
        optReference(
            "documentId",
            DocumentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_documentId_fkey\"",
        )
    val dataroomId =
        optReference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_dataroomId_fkey\"",
        )
    val linkId =
        optReference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_linkId_fkey\"",
        )
    val viewId =
        optReference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_viewId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_userId_fkey\"",
        )
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_viewerId_fkey\"",
        )
    val vectorStoreId = text("vectorStoreId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastMessageAt = pgTimestamp("lastMessageAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Chat_pkey\"")

    init {
        index("\"Chat_teamId_idx\"", false, teamId)
        index("\"Chat_documentId_idx\"", false, documentId)
        index("\"Chat_dataroomId_idx\"", false, dataroomId)
        index("\"Chat_linkId_idx\"", false, linkId)
        index("\"Chat_userId_idx\"", false, userId)
        index("\"Chat_viewerId_idx\"", false, viewerId)
        index("\"Chat_viewId_idx\"", false, viewId)
        index("\"Chat_createdAt_idx\"", false, functions = listOf(indexExpression("\"createdAt\" DESC")))
    }
}
