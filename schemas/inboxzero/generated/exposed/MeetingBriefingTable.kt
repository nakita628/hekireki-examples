package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MeetingBriefingTable : IdTable<String>("\"MeetingBriefing\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val calendarEventId = text("calendarEventId")
    val eventTitle = text("eventTitle")
    val eventStartTime = pgTimestamp("eventStartTime", 3)
    val guestCount = integer("guestCount")
    val status = pgEnum("status", "\"MeetingBriefingStatus\"", MeetingBriefingStatus.entries, MeetingBriefingStatus::dbName)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MeetingBriefing_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MeetingBriefing_pkey\"")

    init {
        index("\"MeetingBriefing_emailAccountId_idx\"", false, emailAccountId)
        uniqueIndex("\"MeetingBriefing_emailAccountId_calendarEventId_key\"", emailAccountId, calendarEventId)
    }
}
