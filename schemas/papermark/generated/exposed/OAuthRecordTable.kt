package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthRecordTable : IdTable<String>("\"OAuthRecord\"") {
    override val id = text("id").entityId()
    val kind = text("kind")
    val payload = jsonb<String>("payload", { it }, { it })
    val grantId = text("grantId").nullable()
    val userCode = text("userCode").nullable()
    val uid = text("uid").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val consumedAt = pgTimestamp("consumedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OAuthRecord_pkey\"")

    init {
        index("\"OAuthRecord_kind_idx\"", false, kind)
        index("\"OAuthRecord_grantId_idx\"", false, grantId)
        index("\"OAuthRecord_userCode_idx\"", false, userCode)
        index("\"OAuthRecord_uid_idx\"", false, uid)
        index("\"OAuthRecord_expiresAt_idx\"", false, expiresAt)
    }
}
