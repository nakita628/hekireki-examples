package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TemplateTable : IdTable<String>("template") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val baseId = text("base_id").nullable().uniqueIndex("template_base_id_key")
    val cover = text("cover").nullable()
    val name = text("name").nullable()
    val description = text("description").nullable()
    val markdownDescription = text("markdown_description").nullable()
    val categoryId = array("category_id", TextColumnType()).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    val isSystem = bool("is_system").nullable()
    val isPublished = bool("is_published").nullable()
    val featured = bool("featured").nullable()
    val snapshot = text("snapshot").nullable()
    val order = double("order")
    val usageCount = integer("usage_count").default(0)
    val publishInfo = jsonb<String>("publish_info", { it }, { it }).nullable()
    val visitCount = integer("visit_count").default(0)
    val kind = text("kind").nullable()
    override val primaryKey = PrimaryKey(id, name = "template_pkey")
}
