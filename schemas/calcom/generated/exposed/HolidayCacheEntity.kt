package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class HolidayCacheEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, HolidayCacheEntity>(HolidayCacheTable)

    var countryCode by HolidayCacheTable.countryCode
    var calendarId by HolidayCacheTable.calendarId
    var eventId by HolidayCacheTable.eventId
    var name by HolidayCacheTable.name
    var date by HolidayCacheTable.date
    var year by HolidayCacheTable.year
    var createdAt by HolidayCacheTable.createdAt
    var updatedAt by HolidayCacheTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(HolidayCacheTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
