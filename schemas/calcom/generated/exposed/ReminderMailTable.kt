package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReminderMailTable : IdTable<Int>("\"ReminderMail\"") {
    override val id = integer("id").autoIncrement().entityId()
    val referenceId = integer("referenceId")
    val reminderType = pgEnum("reminderType", "\"ReminderType\"", ReminderType.entries, ReminderType::dbName)
    val elapsedMinutes = integer("elapsedMinutes")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ReminderMail_pkey\"")

    init {
        index("\"ReminderMail_referenceId_idx\"", false, referenceId)
        index("\"ReminderMail_reminderType_idx\"", false, reminderType)
    }
}
