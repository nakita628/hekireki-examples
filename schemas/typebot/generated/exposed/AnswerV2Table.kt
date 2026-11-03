package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnswerV2Table : IdTable<Int>("\"AnswerV2\"") {
    override val id = integer("id").autoIncrement().entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val blockId = text("blockId")
    val content = text("content")
    val attachedFileUrls = jsonb<String>("attachedFileUrls", { it }, { it }).nullable()
    val resultId =
        reference(
            "resultId",
            ResultTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AnswerV2_resultId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"AnswerV2_pkey\"")

    init {
        index("\"AnswerV2_blockId_idx\"", false, blockId)
    }
}
