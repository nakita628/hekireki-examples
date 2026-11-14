package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SeatRequestTable : IdTable<String>("\"SeatRequest\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val email = text("email")
    val userId = text("userId").nullable()
    val name = text("name").nullable()
    val status = text("status")
    val source1 = text("source")
    val idpRefused = bool("idpRefused").default(false)
    val idpKind = text("idpKind").nullable()
    val salesNotifiedAt = pgTimestamp("salesNotifiedAt", 3).nullable()
    val openedAt = pgTimestamp("openedAt", 3)
    val resolvedAt = pgTimestamp("resolvedAt", 3).nullable()
    val resolvedByUserId = text("resolvedByUserId").nullable()
    val seatPendingSince = pgTimestamp("seatPendingSince", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SeatRequest_pkey\"")

    init {
        index("\"SeatRequest_organizationId_status_idx\"", false, organizationId, status)
        index("\"SeatRequest_userId_idx\"", false, userId)
        uniqueIndex("\"SeatRequest_organizationId_email_key\"", organizationId, email)
    }
}
