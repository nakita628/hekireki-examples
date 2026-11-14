package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebPushSubscriptionTable : IdTable<String>("\"WebPushSubscription\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val userId = text("userId")
    val endpoint = text("endpoint").uniqueIndex("\"WebPushSubscription_endpoint_key\"")
    val p256dh = text("p256dh")
    val auth = text("auth")
    val userAgent = text("userAgent").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastSuccessAt = pgTimestamp("lastSuccessAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"WebPushSubscription_pkey\"")

    init {
        index("\"WebPushSubscription_userId_idx\"", false, userId)
    }
}
