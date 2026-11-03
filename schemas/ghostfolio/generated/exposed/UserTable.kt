package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserTable : IdTable<String>("\"User\"") {
    val accessToken = text("accessToken").nullable()
    val authChallenge = text("authChallenge").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val provider = pgEnum("provider", "\"Provider\"", Provider.entries, Provider::dbName).default(Provider.ANONYMOUS)
    val role = pgEnum("role", "\"Role\"", Role.entries, Role::dbName).default(Role.USER)
    val thirdPartyId = text("thirdPartyId").nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")

    init {
        index("\"User_accessToken_idx\"", false, accessToken)
        index("\"User_createdAt_idx\"", false, createdAt)
        index("\"User_provider_idx\"", false, provider)
        index("\"User_role_idx\"", false, role)
        index("\"User_thirdPartyId_idx\"", false, thirdPartyId)
    }
}
