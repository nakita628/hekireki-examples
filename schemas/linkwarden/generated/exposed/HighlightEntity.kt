package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class HighlightEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, HighlightEntity>(HighlightTable)

    var color by HighlightTable.color
    var comment by HighlightTable.comment
    var link by LinkEntity referencedOn HighlightTable.linkId
    var linkId by HighlightTable.linkId
    var user by UserEntity referencedOn HighlightTable.userId
    var userId by HighlightTable.userId
    var startOffset by HighlightTable.startOffset
    var endOffset by HighlightTable.endOffset
    var text by HighlightTable.text
    var createdAt by HighlightTable.createdAt
    var updatedAt by HighlightTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(HighlightTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
