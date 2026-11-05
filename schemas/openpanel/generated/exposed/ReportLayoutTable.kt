package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ReportLayoutTable : IdTable<UUID>("report_layouts") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val reportId =
        reference(
            "reportId",
            ReportTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"report_layouts_reportId_fkey\"",
        ).uniqueIndex("\"report_layouts_reportId_key\"")
    val gridColumns = integer("gridColumns").default(12)
    val x = integer("x").default(0)
    val y = integer("y").default(0)
    val w = integer("w").default(8)
    val h = integer("h").default(6)
    val minW = integer("minW").nullable().default(4)
    val minH = integer("minH").nullable().default(4)
    val maxW = integer("maxW").nullable()
    val maxH = integer("maxH").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "report_layouts_pkey")
}
