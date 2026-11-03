package umami.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID

object TwoFactorOtpUsedTable : IdTable<String>("two_factor_otp_used") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId = javaUUID("user_id")
    val otp = text("otp")
    val expiresAt = pgTimestamptz("expires_at", 6)
    override val primaryKey = PrimaryKey(id, name = "two_factor_otp_used_pkey")

    init {
        uniqueIndex("two_factor_otp_used_user_id_otp_key", userId, otp)
    }
}
