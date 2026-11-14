package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SeatHoldTable : IdTable<String>("\"SeatHold\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val userId = text("userId")
    val email = text("email").nullable()
    val name = text("name").nullable()
    val seatTakenAt = pgTimestamp("seatTakenAt", 3)
    val heldUntil = pgTimestamp("heldUntil", 3)
    val reason = text("reason")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SeatHold_pkey\"")

    init {
        index("\"SeatHold_heldUntil_idx\"", false, heldUntil)
        uniqueIndex("\"SeatHold_organizationId_userId_key\"", organizationId, userId)
    }
}
