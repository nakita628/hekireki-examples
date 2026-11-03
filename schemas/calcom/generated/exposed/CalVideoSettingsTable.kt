package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CalVideoSettingsTable : IdTable<Int>("\"CalVideoSettings\"") {
    override val id =
        integer("eventTypeId")
            .references(
                EventTypeTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"CalVideoSettings_eventTypeId_fkey\"",
            ).entityId()
    val disableRecordingForOrganizer = bool("disableRecordingForOrganizer").default(false)
    val disableRecordingForGuests = bool("disableRecordingForGuests").default(false)
    val enableAutomaticTranscription = bool("enableAutomaticTranscription").default(false)
    val enableAutomaticRecordingForOrganizer = bool("enableAutomaticRecordingForOrganizer").default(false)
    val redirectUrlOnExit = text("redirectUrlOnExit").nullable()
    val disableTranscriptionForGuests = bool("disableTranscriptionForGuests").default(false)
    val disableTranscriptionForOrganizer = bool("disableTranscriptionForOrganizer").default(false)
    val requireEmailForGuests = bool("requireEmailForGuests").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"CalVideoSettings_pkey\"")
}
