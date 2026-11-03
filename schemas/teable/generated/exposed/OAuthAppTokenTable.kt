package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthAppTokenTable : IdTable<String>("oauth_app_token") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId =
        text("client_id").references(
            OAuthAppTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "oauth_app_token_client_id_fkey",
        )
    val appSecretId =
        optReference(
            "app_secret_id",
            OAuthAppSecretTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "oauth_app_token_app_secret_id_fkey",
        )
    val refreshTokenSign = text("refresh_token_sign").uniqueIndex("oauth_app_token_refresh_token_sign_key")
    val expiredTime = pgTimestamp("expired_time", 3)
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    override val primaryKey = PrimaryKey(id, name = "oauth_app_token_pkey")

    init {
        index("oauth_app_token_client_id_idx", false, clientId)
    }
}
