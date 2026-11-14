package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object SsoConnectionTable : IdTable<String>("\"SsoConnection\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val type = text("type")
    val state = text("state")
    val claimedDomains = array("claimedDomains", TextColumnType()).nullable()
    val domainClaims = jsonb<String>("domainClaims", { it }, { it }).default("[]")
    val approvedDomains = array("approvedDomains", TextColumnType()).nullable()
    val verifiedDomains = array("verifiedDomains", TextColumnType()).nullable()
    val lapsedDomains = array("lapsedDomains", TextColumnType()).nullable()
    val domainVerifications = jsonb<String>("domainVerifications", { it }, { it }).default("[]")
    val pendingVerification = jsonb<String>("pendingVerification", { it }, { it }).nullable()
    val idpMetadata = jsonb<String>("idpMetadata", { it }, { it })
    val arrivalPolicy = text("arrivalPolicy").default("refuse")
    val allowsJit = bool("allowsJit").default(false)
    val arrivalPolicyDecidedAt = pgTimestamp("arrivalPolicyDecidedAt", 3).nullable()
    val source1 = text("source")
    val testLoginAccountId = text("testLoginAccountId").nullable()
    val replacesConnectionId = text("replacesConnectionId").nullable()
    val migrationPhase =
        pgEnum(
            "migrationPhase",
            "\"SsoConnectionMigrationPhase\"",
            SsoConnectionMigrationPhase.entries,
            SsoConnectionMigrationPhase::dbName,
        ).nullable()
    val graceStartedAt = pgTimestamp("graceStartedAt", 3).nullable()
    val routeChangedAt = pgTimestamp("routeChangedAt", 3).nullable()
    val finalizationRequestedAt = pgTimestamp("finalizationRequestedAt", 3).nullable()
    val finalizedAt = pgTimestamp("finalizedAt", 3).nullable()
    val rejection = jsonb<String>("rejection", { it }, { it }).nullable()
    val createdBy = text("createdBy").nullable()
    val tearDownAfter = pgTimestamp("tearDownAfter", 3).nullable()
    val occurredAt = pgTimestamp("occurredAt", 3)
    val lastEventId = text("lastEventId")
    val acceptedAt = pgTimestamp("acceptedAt", 3)
    val projectionVersion = text("projectionVersion")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"SsoConnection_pkey\"")

    init {
        index("\"SsoConnection_organizationId_idx\"", false, organizationId)
        index("\"SsoConnection_organizationId_migrationPhase_idx\"", false, organizationId, migrationPhase)
        index("\"SsoConnection_state_idx\"", false, state)
        index("\"SsoConnection_verifiedDomains_idx\"", false, verifiedDomains, indexType = "gin")
        index("\"SsoConnection_lapsedDomains_idx\"", false, lapsedDomains, indexType = "gin")
    }
}
