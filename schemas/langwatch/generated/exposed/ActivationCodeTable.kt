package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ActivationCodeTable : IdTable<String>("\"ActivationCode\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val codeHash = text("codeHash").uniqueIndex("\"ActivationCode_codeHash_key\"")
    val codeHint = text("codeHint")
    val organizationId = text("organizationId")
    val organizationName = text("organizationName")
    val email = text("email")
    val planType = text("planType")
    val maxMembers = integer("maxMembers")
    val maxMembersLite = integer("maxMembersLite").nullable().default(0)
    val liteSeatsUnlimited = bool("liteSeatsUnlimited").default(false)
    val maxMembersDeveloper = integer("maxMembersDeveloper").nullable()
    val licenseTermDays = integer("licenseTermDays")
    val services = array("services", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val expiresAt = pgTimestamp("expiresAt", 3)
    val reusable = bool("reusable").default(false)
    val redeemedAt = pgTimestamp("redeemedAt", 3).nullable()
    val redeemedByInstanceId = text("redeemedByInstanceId").nullable()
    val issuedLicenseId = text("issuedLicenseId").nullable()
    val redemptionCount = integer("redemptionCount").default(0)
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revokedById = text("revokedById").nullable()
    val createdById = text("createdById").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ActivationCode_pkey\"")

    init {
        index("\"ActivationCode_organizationId_idx\"", false, organizationId)
        index("\"ActivationCode_expiresAt_idx\"", false, expiresAt)
    }
}
