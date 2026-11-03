package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AccountTable : IdTable<String>("\"Account\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_userId_fkey\"",
        )
    val type = text("type")
    val provider = text("provider")
    val providerAccountId = text("providerAccountId")
    val refreshToken = text("refresh_token").nullable()
    val accessToken = text("access_token").nullable()
    val expiresAt = integer("expires_at").nullable()
    val tokenType = text("token_type").nullable()
    val scope = text("scope").nullable()
    val idToken = text("id_token").nullable()
    val sessionState = text("session_state").nullable()
    val oauthTokenSecret = text("oauth_token_secret").nullable()
    val oauthToken = text("oauth_token").nullable()
    val refreshTokenExpiresIn = integer("refresh_token_expires_in").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Account_pkey\"")

    init {
        uniqueIndex("\"Account_provider_providerAccountId_key\"", provider, providerAccountId)
    }
}
