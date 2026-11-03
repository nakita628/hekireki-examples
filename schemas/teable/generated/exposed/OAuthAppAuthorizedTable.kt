package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OAuthAppAuthorizedTable : IdTable<String>("oauth_app_authorized") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId =
        text("client_id").references(
            OAuthAppTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "oauth_app_authorized_client_id_fkey",
        )
    val userId = text("user_id")
    val authorizedTime = pgTimestamp("authorized_time", 3)
    override val primaryKey = PrimaryKey(id, name = "oauth_app_authorized_pkey")

    init {
        uniqueIndex("oauth_app_authorized_client_id_user_id_key", clientId, userId)
    }
}
