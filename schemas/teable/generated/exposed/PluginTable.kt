package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PluginTable : IdTable<String>("plugin") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val detailDesc = text("detail_desc").nullable()
    val logo = text("logo")
    val helpUrl = text("help_url").nullable()
    val status = text("status")
    val positions = text("positions")
    val url = text("url").nullable()
    val secret = text("secret").uniqueIndex("plugin_secret_key")
    val maskedSecret = text("masked_secret")
    val i18n = text("i18n").nullable()
    val config = text("config").nullable()
    val pluginUser = text("plugin_user").nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "plugin_pkey")
}
