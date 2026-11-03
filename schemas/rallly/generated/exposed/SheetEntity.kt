package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SheetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SheetEntity>(SheetTable)

    var spaceId by SheetTable.spaceId
    var hostId by SheetTable.hostId
    var title by SheetTable.title
    var description by SheetTable.description
    var urlId by SheetTable.urlId
    var createdAt by SheetTable.createdAt
    var updatedAt by SheetTable.updatedAt
    var deleted by SheetTable.deleted
    var deletedAt by SheetTable.deletedAt
    var space by SpaceEntity referencedOn SheetTable.spaceId
    var host by UserEntity referencedOn SheetTable.hostId
    val slots by SheetSlotEntity referrersOn SheetSlotTable.sheetId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SheetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
