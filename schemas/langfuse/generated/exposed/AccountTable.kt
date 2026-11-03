package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AccountTable : IdTable<String>("\"Account\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_user_id_fkey\"",
        )
    val type = text("type")
    val provider = text("provider")
    val providerAccountId = text("providerAccountId")
    val refreshToken = text("refresh_token").nullable()
    val accessToken = text("access_token").nullable()
    val expiresAt = integer("expires_at").nullable()
    val expiresIn = integer("expires_in").nullable()
    val extExpiresIn = integer("ext_expires_in").nullable()
    val tokenType = text("token_type").nullable()
    val scope = text("scope").nullable()
    val idToken = text("id_token").nullable()
    val sessionState = text("session_state").nullable()
    val refreshTokenExpiresIn = integer("refresh_token_expires_in").nullable()
    val createdAt = integer("created_at").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Account_pkey\"")

    init {
        index("\"Account_user_id_idx\"", false, userId)
        uniqueIndex("\"Account_provider_providerAccountId_key\"", provider, providerAccountId)
    }
}
