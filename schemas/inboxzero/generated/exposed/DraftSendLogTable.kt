package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DraftSendLogTable : IdTable<String>("\"DraftSendLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val executedActionId =
        reference(
            "executedActionId",
            ExecutedActionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DraftSendLog_executedActionId_fkey\"",
        ).uniqueIndex("\"DraftSendLog_executedActionId_key\"")
    val sentMessageId = text("sentMessageId")
    val similarityScore = double("similarityScore")
    val bodySimilarityScore = double("bodySimilarityScore").nullable()
    val bodySimilarityStatus = text("bodySimilarityStatus").nullable()
    val similarityMetadata = jsonb<String>("similarityMetadata", { it }, { it }).nullable()
    val sentText = text("sentText").nullable()
    val replyMemorySentText = text("replyMemorySentText").nullable()
    val replyMemoryAttemptCount = integer("replyMemoryAttemptCount").default(0)
    val replyMemoryProcessedAt = pgTimestamp("replyMemoryProcessedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"DraftSendLog_pkey\"")

    init {
        index("\"DraftSendLog_executedActionId_idx\"", false, executedActionId)
        index(
            "DraftSendLog_replyMemoryProcessedAt_replyMemoryAttemptCount_idx",
            false,
            replyMemoryProcessedAt,
            replyMemoryAttemptCount,
            createdAt,
        )
    }
}
