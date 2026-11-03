package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OauthClientAssertionTable : IdTable<String>("\"oauthClientAssertion\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"oauthClientAssertion_pkey\"")

    init {
        index("\"oauthClientAssertion_expiresAt_idx\"", false, expiresAt)
    }
}
