package inboxzero.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReplyMemorySourceTable : CompositeIdTable("\"ReplyMemorySource\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val learnedWritingStyleAnalyzedAt = pgTimestamp("learnedWritingStyleAnalyzedAt", 3).nullable()
    val replyMemoryId =
        reference(
            "replyMemoryId",
            ReplyMemoryTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ReplyMemorySource_replyMemoryId_fkey\"",
        )
    val draftSendLogId =
        reference(
            "draftSendLogId",
            DraftSendLogTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ReplyMemorySource_draftSendLogId_fkey\"",
        )
    override val primaryKey = PrimaryKey(replyMemoryId, draftSendLogId, name = "\"ReplyMemorySource_pkey\"")

    init {
        addIdColumn(replyMemoryId)
        addIdColumn(draftSendLogId)
        index("\"ReplyMemorySource_draftSendLogId_idx\"", false, draftSendLogId)
        index(
            "ReplyMemorySource_replyMemoryId_learnedWritingStyleAnalyzed_idx",
            false,
            replyMemoryId,
            learnedWritingStyleAnalyzedAt,
            createdAt,
        )
    }
}
