package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AlertOccurrenceSeenTable : CompositeIdTable("\"AlertOccurrenceSeen\"") {
    val alertId = text("alertId").entityId()
    val sourceEventId = text("sourceEventId").entityId()
    val rowTime = pgTimestamptz("rowTime", 6)
    val seenAt = pgTimestamptz("seenAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamptz("expiresAt", 6)
    override val primaryKey = PrimaryKey(alertId, sourceEventId, name = "\"AlertOccurrenceSeen_pkey\"")

    init {
        index("\"AlertOccurrenceSeen_expiresAt_idx\"", false, expiresAt)
    }
}
