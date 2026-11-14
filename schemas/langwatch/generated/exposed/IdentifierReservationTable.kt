package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IdentifierReservationTable : IdTable<String>("\"IdentifierReservation\"") {
    override val id = text("normalizedValue").entityId()
    val userId = text("userId")
    val identifierId = text("identifierId")
    val commandId = text("commandId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IdentifierReservation_pkey\"")

    init {
        index("\"IdentifierReservation_userId_idx\"", false, userId)
        index("\"IdentifierReservation_identifierId_idx\"", false, identifierId)
    }
}
