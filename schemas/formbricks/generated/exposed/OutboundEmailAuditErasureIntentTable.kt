package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OutboundEmailAuditErasureIntentTable : IdTable<String>("\"OutboundEmailAuditErasureIntent\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId = text("userId").nullable()
    val userHash = text("userHash")
    val recipientMac = text("recipientMac")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val activatedAt = pgTimestamp("activatedAt", 3).nullable()
    val checkedAt = pgTimestamp("checkedAt", 3).nullable()
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"OutboundEmailAuditErasureIntent_pkey\"")

    init {
        index("\"OutboundEmailAuditErasureIntent_userId_idx\"", false, userId)
        index("\"OutboundEmailAuditErasureIntent_recipientMac_activatedAt_idx\"", false, recipientMac, activatedAt)
        index("\"OutboundEmailAuditErasureIntent_userHash_activatedAt_idx\"", false, userHash, activatedAt)
        index("\"OutboundEmailAuditErasureIntent_checkedAt_id_idx\"", false, checkedAt, id)
        index("\"OutboundEmailAuditErasureIntent_expiresAt_idx\"", false, expiresAt)
    }
}
