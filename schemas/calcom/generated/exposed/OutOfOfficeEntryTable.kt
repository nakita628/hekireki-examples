package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OutOfOfficeEntryTable : IdTable<Int>("\"OutOfOfficeEntry\"") {
    override val id = integer("id").autoIncrement().entityId()
    val uuid = text("uuid").uniqueIndex("\"OutOfOfficeEntry_uuid_key\"")
    val start = pgTimestamp("start", 3)
    val end = pgTimestamp("end", 3)
    val notes = text("notes").nullable()
    val showNotePublicly = bool("showNotePublicly").default(false)
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OutOfOfficeEntry_userId_fkey\"",
        )
    val toUserId =
        optReference(
            "toUserId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OutOfOfficeEntry_toUserId_fkey\"",
        )
    val reasonId =
        optReference(
            "reasonId",
            OutOfOfficeReasonTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OutOfOfficeEntry_reasonId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OutOfOfficeEntry_pkey\"")

    init {
        index("\"OutOfOfficeEntry_uuid_idx\"", false, uuid)
        index("\"OutOfOfficeEntry_userId_idx\"", false, userId)
        index("\"OutOfOfficeEntry_toUserId_idx\"", false, toUserId)
        index("\"OutOfOfficeEntry_start_end_idx\"", false, start, end)
    }
}
