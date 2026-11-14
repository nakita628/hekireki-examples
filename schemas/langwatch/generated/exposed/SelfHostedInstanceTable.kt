package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SelfHostedInstanceTable : IdTable<String>("\"SelfHostedInstance\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val instanceId = text("instanceId").uniqueIndex("\"SelfHostedInstance_instanceId_key\"")
    val firstSeenAt = pgTimestamp("firstSeenAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastSeenAt = pgTimestamp("lastSeenAt", 3)
    val version = text("version").nullable()
    val installMethod = text("installMethod").nullable()
    val chartVersion = text("chartVersion").nullable()
    val hostname = text("hostname").nullable()
    val environment = text("environment").nullable()
    val installedAt = pgTimestamp("installedAt", 3).nullable()
    val reportSchemaVersion = integer("reportSchemaVersion").nullable()
    val organizationId = text("organizationId").nullable()
    val issuedLicenseId = text("issuedLicenseId").nullable()
    val userEmailDomains = jsonb<String>("userEmailDomains", { it }, { it }).nullable()
    val userDomains = array("userDomains", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val latestReport = jsonb<String>("latestReport", { it }, { it }).nullable()
    val optionalMetricsReported = bool("optionalMetricsReported").default(true)
    val hostnameReported = bool("hostnameReported").default(true)
    val reportCount = integer("reportCount").default(0)
    val lastUnknownFields = integer("lastUnknownFields").default(0)
    val raisedSignals = array("raisedSignals", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SelfHostedInstance_pkey\"")

    init {
        index("\"SelfHostedInstance_lastSeenAt_idx\"", false, lastSeenAt)
        index("\"SelfHostedInstance_organizationId_idx\"", false, organizationId)
        index("\"SelfHostedInstance_issuedLicenseId_idx\"", false, issuedLicenseId)
    }
}
