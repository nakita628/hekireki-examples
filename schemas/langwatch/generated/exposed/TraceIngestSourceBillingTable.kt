package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TraceIngestSourceBillingTable : CompositeIdTable("\"TraceIngestSourceBilling\"") {
    val organizationId = text("organizationId").entityId()
    val sourceType = text("sourceType").entityId()
    val billed = bool("billed")
    val recordedAt = pgTimestamp("recordedAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(organizationId, sourceType, name = "\"TraceIngestSourceBilling_pkey\"")
}
