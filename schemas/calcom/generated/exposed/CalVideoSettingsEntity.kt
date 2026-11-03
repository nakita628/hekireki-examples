package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalVideoSettingsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CalVideoSettingsEntity>(CalVideoSettingsTable)

    val eventType by EventTypeEntity referencedOn CalVideoSettingsTable.id
    var disableRecordingForOrganizer by CalVideoSettingsTable.disableRecordingForOrganizer
    var disableRecordingForGuests by CalVideoSettingsTable.disableRecordingForGuests
    var enableAutomaticTranscription by CalVideoSettingsTable.enableAutomaticTranscription
    var enableAutomaticRecordingForOrganizer by CalVideoSettingsTable.enableAutomaticRecordingForOrganizer
    var redirectUrlOnExit by CalVideoSettingsTable.redirectUrlOnExit
    var disableTranscriptionForGuests by CalVideoSettingsTable.disableTranscriptionForGuests
    var disableTranscriptionForOrganizer by CalVideoSettingsTable.disableTranscriptionForOrganizer
    var requireEmailForGuests by CalVideoSettingsTable.requireEmailForGuests
    var createdAt by CalVideoSettingsTable.createdAt
    var updatedAt by CalVideoSettingsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalVideoSettingsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
