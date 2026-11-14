package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AuthzUserStandingTable : IdTable<String>("\"AuthzUserStanding\"") {
    override val id = text("userId").entityId()
    val deactivatedAt = pgTimestamp("deactivatedAt", 3).nullable()
    val erasedAt = pgTimestamp("erasedAt", 3).nullable()
    val standingChangedAt = pgTimestamp("standingChangedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"AuthzUserStanding_pkey\"")
}
