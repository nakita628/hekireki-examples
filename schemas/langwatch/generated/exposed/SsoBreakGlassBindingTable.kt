package langwatch.models

import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SsoBreakGlassBindingTable : IdTable<String>("\"SsoBreakGlassBinding\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val userId = text("userId")
    val grantedByUserId = text("grantedByUserId")
    val grantedAt = pgTimestamp("grantedAt", 3)
    val expiresAt = pgTimestamp("expiresAt", 3)
    val supersededAt = pgTimestamp("supersededAt", 3).nullable()
    val renewedFromId = text("renewedFromId").nullable()
    val warnedDays = array("warnedDays", IntegerColumnType()).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SsoBreakGlassBinding_pkey\"")

    init {
        index("\"SsoBreakGlassBinding_organizationId_expiresAt_idx\"", false, organizationId, expiresAt)
        index("\"SsoBreakGlassBinding_userId_idx\"", false, userId)
    }
}
