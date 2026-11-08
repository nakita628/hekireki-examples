package workoutcool.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AccountTable : IdTable<String>("account") {
    override val id = text("id").entityId()
    val accountId = text("accountId")
    val providerId = text("providerId")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"account_userId_fkey\"",
        )
    val accessToken = text("accessToken").nullable()
    val refreshToken = text("refreshToken").nullable()
    val idToken = text("idToken").nullable()
    val accessTokenExpiresAt = pgTimestamp("accessTokenExpiresAt", 3).nullable()
    val refreshTokenExpiresAt = pgTimestamp("refreshTokenExpiresAt", 3).nullable()
    val scope = text("scope").nullable()
    val password = text("password").nullable()
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "account_pkey")
}
