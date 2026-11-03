package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthAppSecretTable : IdTable<String>("oauth_app_secret") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId =
        text("client_id").references(
            OAuthAppTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "oauth_app_secret_client_id_fkey",
        )
    val secret = text("secret").uniqueIndex("oauth_app_secret_secret_key")
    val maskedSecret = text("masked_secret")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastUsedTime = pgTimestamp("last_used_time", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "oauth_app_secret_pkey")
}
