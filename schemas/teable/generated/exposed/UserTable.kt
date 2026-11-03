package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("users") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val password = text("password").nullable()
    val salt = text("salt").nullable()
    val phone = text("phone").nullable().uniqueIndex("users_phone_key")
    val email = text("email").uniqueIndex("users_email_key")
    val avatar = text("avatar").nullable()
    val isSystem = bool("is_system").nullable()
    val isAdmin = bool("is_admin").nullable()
    val isTrialUsed = bool("is_trial_used").nullable()
    val lang = text("lang").nullable()
    val notifyMeta = text("notify_meta").nullable()
    val lastSignTime = pgTimestamp("last_sign_time", 3).nullable()
    val deactivatedTime = pgTimestamp("deactivated_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedTime = pgTimestamp("deleted_time", 3).nullable()
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val permanentDeletedTime = pgTimestamp("permanent_deleted_time", 3).nullable()
    val refMeta = text("ref_meta").nullable()
    override val primaryKey = PrimaryKey(id, name = "users_pkey")
}
