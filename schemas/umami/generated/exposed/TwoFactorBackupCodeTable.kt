package umami.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit

object TwoFactorBackupCodeTable : IdTable<String>("two_factor_backup_code") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId = javaUUID("user_id")
    val codeHash = text("code_hash")
    val used = bool("used").default(false)
    val createdAt = pgTimestamptz("created_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "two_factor_backup_code_pkey")

    init {
        index("two_factor_backup_code_user_id_idx", false, userId)
    }
}
