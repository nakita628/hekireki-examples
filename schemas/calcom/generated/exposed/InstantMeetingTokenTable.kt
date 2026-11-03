package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InstantMeetingTokenTable : IdTable<Int>("\"InstantMeetingToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val token = text("token").uniqueIndex("\"InstantMeetingToken_token_key\"")
    val expires = pgTimestamp("expires", 3)
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InstantMeetingToken_teamId_fkey\"",
        )
    val bookingId =
        optReference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InstantMeetingToken_bookingId_fkey\"",
        ).uniqueIndex("\"InstantMeetingToken_bookingId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"InstantMeetingToken_pkey\"")

    init {
        index("\"InstantMeetingToken_token_idx\"", false, token)
    }
}
