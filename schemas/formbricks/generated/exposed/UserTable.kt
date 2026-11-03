package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val email = text("email").uniqueIndex("\"User_email_key\"")
    val emailVerified = bool("email_verified").default(false)
    val twoFactorSecret = text("twoFactorSecret").nullable()
    val twoFactorEnabled = bool("twoFactorEnabled").default(false)
    val backupCodes = text("backupCodes").nullable()
    val password = text("password").nullable()
    val identityProvider =
        pgEnum("identityProvider", "\"IdentityProvider\"", IdentityProvider.entries, IdentityProvider::dbName).default(
            IdentityProvider.EMAIL,
        )
    val identityProviderAccountId = text("identityProviderAccountId").nullable()
    val groupId = text("groupId").nullable()
    val notificationSettings = jsonb<String>("notificationSettings", { it }, { it }).default("{}")
    val locale = text("locale").default("en-US")
    val lastLoginAt = pgTimestamp("lastLoginAt", 3).nullable()
    val isActive = bool("isActive").default(true)
    val reactivatedAt = pgTimestamp("reactivatedAt", 3).nullable()
    val lastActiveAt = pgTimestamp("lastActiveAt", 3).nullable()
    val isBootstrapAdmin =
        pgEnum("isBootstrapAdmin", "\"BootstrapAdminMarker\"", BootstrapAdminMarker.entries, BootstrapAdminMarker::dbName)
            .nullable()
            .uniqueIndex("\"User_isBootstrapAdmin_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
