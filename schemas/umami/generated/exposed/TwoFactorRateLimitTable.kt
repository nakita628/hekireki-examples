package umami.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit

object TwoFactorRateLimitTable : IdTable<String>("two_factor_rate_limit") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId = javaUUID("user_id").uniqueIndex("two_factor_rate_limit_user_id_key")
    val attempts = integer("attempts").default(0)
    val lockedUntil = pgTimestamptz("locked_until", 6).nullable()
    val updatedAt = pgTimestamptz("updated_at", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "two_factor_rate_limit_pkey")
}
