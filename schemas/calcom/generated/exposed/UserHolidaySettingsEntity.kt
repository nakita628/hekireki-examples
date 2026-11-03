package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserHolidaySettingsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserHolidaySettingsEntity>(UserHolidaySettingsTable)

    var userId by UserHolidaySettingsTable.userId
    var user by UserEntity referencedOn UserHolidaySettingsTable.userId
    var countryCode by UserHolidaySettingsTable.countryCode
    var disabledIds by UserHolidaySettingsTable.disabledIds
    var createdAt by UserHolidaySettingsTable.createdAt
    var updatedAt by UserHolidaySettingsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserHolidaySettingsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
