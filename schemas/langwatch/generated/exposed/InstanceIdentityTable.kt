package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InstanceIdentityTable : IdTable<String>("\"InstanceIdentity\"") {
    override val id = text("id").default("self").entityId()
    val instanceId = text("instanceId").uniqueIndex("\"InstanceIdentity_instanceId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastReportAt = pgTimestamp("lastReportAt", 3).nullable()
    val lastReportError = text("lastReportError").nullable()
    val optionalMetricsOptOut = bool("optionalMetricsOptOut").default(false)
    val hostnameOptOut = bool("hostnameOptOut").default(false)
    val upgradeDiagnosticsOptOut = bool("upgradeDiagnosticsOptOut").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"InstanceIdentity_pkey\"")
}
