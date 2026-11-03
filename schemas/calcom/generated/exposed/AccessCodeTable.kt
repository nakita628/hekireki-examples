package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AccessCodeTable : IdTable<Int>("\"AccessCode\"") {
    override val id = integer("id").autoIncrement().entityId()
    val code = text("code")
    val clientId =
        optReference(
            "clientId",
            OAuthClientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AccessCode_clientId_fkey\"",
        )
    val expiresAt = pgTimestamp("expiresAt", 3)
    val scopes = array("scopes", PgEnumColumnType("\"AccessScope\"", AccessScope.entries, AccessScope::dbName)).nullable()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AccessCode_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AccessCode_teamId_fkey\"",
        )
    val codeChallenge = text("codeChallenge").nullable()
    val codeChallengeMethod = text("codeChallengeMethod").nullable().default("S256")
    override val primaryKey = PrimaryKey(id, name = "\"AccessCode_pkey\"")
}
