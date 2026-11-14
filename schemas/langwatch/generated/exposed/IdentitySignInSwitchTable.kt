package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IdentitySignInSwitchTable : IdTable<String>("\"IdentitySignInSwitch\"") {
    override val id = text("userId").entityId()
    val onIdentifiers = bool("onIdentifiers")
    val pinned = bool("pinned").default(false)
    val switchedAt = pgTimestamp("switchedAt", 3)
    val lastAppliedEventId = text("lastAppliedEventId").nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IdentitySignInSwitch_pkey\"")
}
