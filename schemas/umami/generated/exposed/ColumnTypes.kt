package umami.models

import org.jetbrains.exposed.v1.core.Column
import org.jetbrains.exposed.v1.core.ColumnType
import org.jetbrains.exposed.v1.core.Function
import org.jetbrains.exposed.v1.core.IColumnType
import org.jetbrains.exposed.v1.core.QueryBuilder
import org.jetbrains.exposed.v1.core.Table
import org.jetbrains.exposed.v1.core.statements.api.RowApi
import org.jetbrains.exposed.v1.dao.Entity
import java.time.Instant
import java.time.OffsetDateTime
import java.time.ZoneOffset
import java.time.temporal.ChronoUnit

internal class SqlExpression<T>(
    private val sql: String,
    columnType: IColumnType<T & Any>,
) : Function<T>(columnType) {
    override fun toQueryBuilder(queryBuilder: QueryBuilder) {
        queryBuilder.append(sql)
    }
}

internal fun <T> Column<T>.databaseDefault(
    sql: String,
    value: (() -> T)? = null,
): Column<T> =
    with(table) {
        if (value == null) defaultExpression(SqlExpression(sql, columnType)) else clientDefault(value).withDefinition("DEFAULT $sql")
    }

internal class PgTimestamptzColumnType(
    val precision: Int? = null,
) : ColumnType<Instant>() {
    override fun sqlType(): String = if (precision == null) "TIMESTAMPTZ" else "TIMESTAMPTZ($precision)"

    override fun valueFromDB(value: Any): Instant =
        when (value) {
            is Instant -> value
            is OffsetDateTime -> value.toInstant()
            else -> error("Unexpected value of type ${value::class.qualifiedName}: $value")
        }.truncatedTo(ChronoUnit.MILLIS)

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getObject(index, OffsetDateTime::class.java)

    override fun notNullValueToDB(value: Instant): Any = value.truncatedTo(ChronoUnit.MILLIS).atOffset(ZoneOffset.UTC)

    override fun nonNullValueToString(value: Instant): String = "'${notNullValueToDB(value)}'"
}

internal fun Table.pgTimestamptz(
    name: String,
    precision: Int? = null,
): Column<Instant> = registerColumn(name, PgTimestamptzColumnType(precision))

internal fun Entity<*>.isWritten(column: Column<*>): Boolean = writeValues.keys.any { it == column }
