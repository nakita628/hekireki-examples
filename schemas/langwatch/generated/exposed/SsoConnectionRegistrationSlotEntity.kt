package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class SsoConnectionRegistrationSlotEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SsoConnectionRegistrationSlotEntity>(SsoConnectionRegistrationSlotTable)

    val organizationId by SsoConnectionRegistrationSlotTable.organizationId
    val kind by SsoConnectionRegistrationSlotTable.kind
    var connectionId by SsoConnectionRegistrationSlotTable.connectionId
    var replacesConnectionId by SsoConnectionRegistrationSlotTable.replacesConnectionId
    var commandId by SsoConnectionRegistrationSlotTable.commandId
    var createdAt by SsoConnectionRegistrationSlotTable.createdAt
    var updatedAt by SsoConnectionRegistrationSlotTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SsoConnectionRegistrationSlotTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
