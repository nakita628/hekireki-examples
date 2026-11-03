package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnswerTable : CompositeIdTable("\"Answer\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val resultId =
        reference(
            "resultId",
            ResultTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Answer_resultId_fkey\"",
        )
    val blockId = text("blockId").entityId()
    val groupId = text("groupId").entityId()
    val variableId = text("variableId").nullable()
    val content = text("content")

    init {
        addIdColumn(resultId)
        uniqueIndex("\"Answer_resultId_blockId_groupId_key\"", resultId, blockId, groupId)
    }
}
