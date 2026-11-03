package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SheetSlotEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SheetSlotEntity>(SheetSlotTable)

    var sheetId by SheetSlotTable.sheetId
    var eventTypeId by SheetSlotTable.eventTypeId
    var startTime by SheetSlotTable.startTime
    var createdAt by SheetSlotTable.createdAt
    var updatedAt by SheetSlotTable.updatedAt
    var sheet by SheetEntity referencedOn SheetSlotTable.sheetId
    var eventType by EventTypeEntity referencedOn SheetSlotTable.eventTypeId
    val scheduledEvent by ScheduledEventEntity optionalBackReferencedOn ScheduledEventTable.sheetSlotId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SheetSlotTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
