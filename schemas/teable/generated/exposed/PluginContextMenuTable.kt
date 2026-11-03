package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PluginContextMenuTable : IdTable<String>("plugin_context_menu") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val tableId =
        reference(
            "table_id",
            TableMetaTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "plugin_context_menu_table_id_fkey",
        )
    val pluginInstallId = text("plugin_install_id").uniqueIndex("plugin_context_menu_plugin_install_id_key")
    val order = double("order")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "plugin_context_menu_pkey")
}
