package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AnswerV2Entity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AnswerV2Entity>(AnswerV2Table)

    var createdAt by AnswerV2Table.createdAt
    var blockId by AnswerV2Table.blockId
    var content by AnswerV2Table.content
    var attachedFileUrls by AnswerV2Table.attachedFileUrls
    var resultId by AnswerV2Table.resultId
    var result by ResultEntity referencedOn AnswerV2Table.resultId
}
