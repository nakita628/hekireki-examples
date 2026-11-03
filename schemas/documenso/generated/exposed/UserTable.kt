package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<Int>("\"User\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name").nullable()
    val email = text("email").uniqueIndex("\"User_email_key\"")
    val emailVerified = pgTimestamp("emailVerified", 3).nullable()
    val password = text("password").nullable()
    val source1 = text("source").nullable()
    val signature = text("signature").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastSignedIn = pgTimestamp("lastSignedIn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val roles =
        array("roles", PgEnumColumnType("\"Role\"", Role.entries, Role::dbName))
            .nullable()
            .databaseDefault("'{\"USER\"}'") { listOf(Role.USER) }
    val identityProvider =
        pgEnum("identityProvider", "\"IdentityProvider\"", IdentityProvider.entries, IdentityProvider::dbName).default(
            IdentityProvider.DOCUMENSO,
        )
    val avatarImageId =
        optReference(
            "avatarImageId",
            AvatarImageTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_avatarImageId_fkey\"",
        )
    val disabled = bool("disabled").default(false)
    val twoFactorSecret = text("twoFactorSecret").nullable()
    val twoFactorEnabled = bool("twoFactorEnabled").default(false)
    val twoFactorBackupCodes = text("twoFactorBackupCodes").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")

    init {
        index("\"User_email_idx\"", false, email)
    }
}
