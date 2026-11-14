package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object VirtualKeyTable : IdTable<String>("\"VirtualKey\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val name = text("name")
    val description = text("description").nullable()
    val status =
        pgEnum("status", "\"VirtualKeyStatus\"", VirtualKeyStatus.entries, VirtualKeyStatus::dbName).default(VirtualKeyStatus.ACTIVE)
    val purpose =
        pgEnum("purpose", "\"VirtualKeyPurpose\"", VirtualKeyPurpose.entries, VirtualKeyPurpose::dbName).default(VirtualKeyPurpose.USER)
    val externalId = text("externalId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val disabledAt = pgTimestamp("disabledAt", 3).nullable()
    val disabledReason = text("disabledReason").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val hashedSecret = text("hashedSecret").uniqueIndex("\"VirtualKey_hashedSecret_key\"")
    val displayPrefix = text("displayPrefix")
    val principalUserId = text("principalUserId").nullable()
    val traceProjectId = text("traceProjectId").nullable()
    val config = jsonb<String>("config", { it }, { it }).default("{}")
    val revision = long("revision").default(0L)
    val previousHashedSecret = text("previousHashedSecret").nullable()
    val previousSecretValidUntil = pgTimestamp("previousSecretValidUntil", 3).nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revokedById = text("revokedById").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById")
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val routingPolicyId = text("routingPolicyId").nullable()
    val routingMode =
        pgEnum("routingMode", "\"VirtualKeyRoutingMode\"", VirtualKeyRoutingMode.entries, VirtualKeyRoutingMode::dbName).default(
            VirtualKeyRoutingMode.NONE,
        )
    val connectServices = array("connectServices", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val licenseTokenHash = text("licenseTokenHash").nullable().uniqueIndex("\"VirtualKey_licenseTokenHash_key\"")
    val licenseInstanceId = text("licenseInstanceId").nullable()
    val licenseExpiresAt = pgTimestamp("licenseExpiresAt", 3).nullable()
    val licenseId = text("licenseId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"VirtualKey_pkey\"")

    init {
        index("\"VirtualKey_organizationId_idx\"", false, organizationId)
        index("\"VirtualKey_displayPrefix_idx\"", false, displayPrefix)
        index("\"VirtualKey_status_idx\"", false, status)
        index("\"VirtualKey_principalUserId_idx\"", false, principalUserId)
        index("\"VirtualKey_revision_idx\"", false, revision)
        index("\"VirtualKey_routingPolicyId_idx\"", false, routingPolicyId)
        index("\"VirtualKey_organizationId_purpose_idx\"", false, organizationId, purpose)
        uniqueIndex("\"VirtualKey_organizationId_externalId_key\"", organizationId, externalId)
    }
}
