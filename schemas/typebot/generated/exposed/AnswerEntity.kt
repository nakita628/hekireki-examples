package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnswerEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AnswerEntity>(AnswerTable)

    var createdAt by AnswerTable.createdAt
    val resultId by AnswerTable.resultId
    val blockId by AnswerTable.blockId
    val groupId by AnswerTable.groupId
    var variableId by AnswerTable.variableId
    var content by AnswerTable.content
    val result by ResultEntity referencedOn AnswerTable.resultId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnswerTable.createdAt)) {
                createdAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
