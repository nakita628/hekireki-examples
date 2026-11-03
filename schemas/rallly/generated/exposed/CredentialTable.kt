package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CredentialTable : IdTable<String>("credentials") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "credentials_user_id_fkey",
        )
    val provider = text("provider")
    val providerAccountId = text("provider_account_id")
    val type = pgEnum("type", "\"CredentialType\"", CredentialType.entries, CredentialType::dbName)
    val secret = text("secret")
    val scopes = array("scopes", TextColumnType()).nullable()
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "credentials_pkey")

    init {
        index("credential_expiry_idx", false, expiresAt)
        uniqueIndex("credentials_user_id_provider_provider_account_id_key", userId, provider, providerAccountId)
    }
}
