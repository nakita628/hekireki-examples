package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalendarCacheEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<CalendarCacheEntity>(CalendarCacheTable)

    var id1 by CalendarCacheTable.id1
    val key by CalendarCacheTable.key
    var value by CalendarCacheTable.value
    var expiresAt by CalendarCacheTable.expiresAt
    var updatedAt by CalendarCacheTable.updatedAt
    val credentialId by CalendarCacheTable.credentialId
    var userId by CalendarCacheTable.userId
    val credential by CredentialEntity referencedOn CalendarCacheTable.credentialId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalendarCacheTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
