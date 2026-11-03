package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class CollaboratorsOnTypebotsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<CollaboratorsOnTypebotsEntity>(CollaboratorsOnTypebotsTable)

    var createdAt by CollaboratorsOnTypebotsTable.createdAt
    var updatedAt by CollaboratorsOnTypebotsTable.updatedAt
    val userId by CollaboratorsOnTypebotsTable.userId
    val typebotId by CollaboratorsOnTypebotsTable.typebotId
    var type by CollaboratorsOnTypebotsTable.type
    val typebot by TypebotEntity referencedOn CollaboratorsOnTypebotsTable.typebotId
    val user by UserEntity referencedOn CollaboratorsOnTypebotsTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CollaboratorsOnTypebotsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
