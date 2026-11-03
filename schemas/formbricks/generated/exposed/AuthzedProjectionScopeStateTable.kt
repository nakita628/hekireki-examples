package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuthzedProjectionScopeStateTable : IdTable<String>("\"AuthzedProjectionScopeState\"") {
    override val id = text("scope").entityId()
    val readyAt = pgTimestamp("readyAt", 3).nullable()
    val readyBy = text("readyBy").nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AuthzedProjectionScopeState_pkey\"")
}
