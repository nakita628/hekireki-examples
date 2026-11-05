package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OauthClientAssertionTable : IdTable<String>("\"OauthClientAssertion\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"OauthClientAssertion_pkey\"")
}
