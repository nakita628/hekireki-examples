package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IssuedLicenseTable : IdTable<String>("\"IssuedLicense\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val licenseId = text("licenseId").uniqueIndex("\"IssuedLicense_licenseId_key\"")
    val tokenHash = text("tokenHash").uniqueIndex("\"IssuedLicense_tokenHash_key\"")
    val organizationId = text("organizationId").nullable()
    val organizationName = text("organizationName")
    val email = text("email")
    val planType = text("planType")
    val maxMembers = integer("maxMembers")
    val maxMembersLite = integer("maxMembersLite").nullable()
    val liteSeatsUnlimited = bool("liteSeatsUnlimited").default(false)
    val maxMembersDeveloper = integer("maxMembersDeveloper").nullable()
    val issuedAt = pgTimestamp("issuedAt", 3)
    val expiresAt = pgTimestamp("expiresAt", 3)
    val source1 = pgEnum("source", "\"IssuedLicenseSource\"", IssuedLicenseSource.entries, IssuedLicenseSource::dbName)
    val issuedById = text("issuedById").nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revokedById = text("revokedById").nullable()
    val revokedReason = text("revokedReason").nullable()
    val supersededAt = pgTimestamp("supersededAt", 3).nullable()
    val replacesId = text("replacesId").nullable().uniqueIndex("\"IssuedLicense_replacesId_key\"")
    val pendingDeliveryLicense = text("pendingDeliveryLicense").nullable()
    val services = array("services", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val seatRateCents = integer("seatRateCents").nullable()
    val seatCurrency = pgEnum("seatCurrency", "\"Currency\"", Currency.entries, Currency::dbName).nullable()
    val commitUsdCents = integer("commitUsdCents").default(0)
    val overageEnabled = bool("overageEnabled").default(false)
    val overageMaxUsdCents = integer("overageMaxUsdCents").nullable()
    val instanceId = text("instanceId").nullable()
    val instanceBoundAt = pgTimestamp("instanceBoundAt", 3).nullable()
    val lastSyncAt = pgTimestamp("lastSyncAt", 3).nullable()
    val lastSyncVersion = text("lastSyncVersion").nullable()
    val reportedMembers = integer("reportedMembers").nullable()
    val reportedMembersLite = integer("reportedMembersLite").nullable()
    val virtualKeyId = text("virtualKeyId").nullable().uniqueIndex("\"IssuedLicense_virtualKeyId_key\"")
    val seatsRaisedFrom = integer("seatsRaisedFrom").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IssuedLicense_pkey\"")

    init {
        index("\"IssuedLicense_organizationId_idx\"", false, organizationId)
        index("\"IssuedLicense_expiresAt_idx\"", false, expiresAt)
        index("\"IssuedLicense_seatsRaisedFrom_idx\"", false, seatsRaisedFrom)
    }
}
