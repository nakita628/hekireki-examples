package umami.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit

object TwoFactorAuthTable : IdTable<String>("two_factor_auth") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId = javaUUID("user_id").uniqueIndex("two_factor_auth_user_id_key")
    val secret = text("secret")
    val isEnabled = bool("is_enabled").default(false)
    val createdAt = pgTimestamptz("created_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "two_factor_auth_pkey")
}
