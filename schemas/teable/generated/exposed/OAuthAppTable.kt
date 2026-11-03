package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthAppTable : IdTable<String>("oauth_app") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val logo = text("logo").nullable()
    val homepage = text("homepage")
    val description = text("description").nullable()
    val clientId = text("client_id").uniqueIndex("oauth_app_client_id_key")
    val redirectUris = text("redirect_uris").nullable()
    val scopes = text("scopes").nullable()
    val allowDeviceFlow = bool("allow_device_flow").default(false)
    val dynamicRegistration = bool("dynamic_registration").default(false)
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    override val primaryKey = PrimaryKey(id, name = "oauth_app_pkey")
}
