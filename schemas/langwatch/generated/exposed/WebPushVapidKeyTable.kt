package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebPushVapidKeyTable : IdTable<String>("\"WebPushVapidKey\"") {
    override val id = text("id").default("self").entityId()
    val publicKey = text("publicKey")
    val privateKeyEncrypted = text("privateKeyEncrypted")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WebPushVapidKey_pkey\"")
}
