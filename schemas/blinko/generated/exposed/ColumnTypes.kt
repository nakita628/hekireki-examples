package blinko.models

import org.jetbrains.exposed.v1.core.Column
import org.jetbrains.exposed.v1.core.ColumnType
import org.jetbrains.exposed.v1.core.Function
import org.jetbrains.exposed.v1.core.IColumnType
import org.jetbrains.exposed.v1.core.QueryBuilder
import org.jetbrains.exposed.v1.core.Table
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.statements.api.RowApi
import org.jetbrains.exposed.v1.dao.Entity
import java.math.BigDecimal
import java.time.Instant
import java.time.LocalDateTime
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

internal class PgTimestampColumnType(
    val precision: Int? = null,
) : ColumnType<Instant>() {
    override fun sqlType(): String = if (precision == null) "TIMESTAMP" else "TIMESTAMP($precision)"

    override fun valueFromDB(value: Any): Instant =
        when (value) {
            is Instant -> value
            is LocalDateTime -> value.toInstant(ZoneOffset.UTC)
            else -> error("Unexpected value of type ${value::class.qualifiedName}: $value")
        }.truncatedTo(ChronoUnit.MILLIS)

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getObject(index, LocalDateTime::class.java)

    override fun notNullValueToDB(value: Instant): Any = LocalDateTime.ofInstant(value.truncatedTo(ChronoUnit.MILLIS), ZoneOffset.UTC)

    override fun nonNullValueToString(value: Instant): String = "'${notNullValueToDB(value)}'"
}

internal fun Table.pgTimestamp(
    name: String,
    precision: Int? = null,
): Column<Instant> = registerColumn(name, PgTimestampColumnType(precision))

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

internal class PgStringColumnType(
    val typeName: String,
    val castName: String,
) : TextColumnType() {
    override fun sqlType(): String = typeName

    override fun valueFromDB(value: Any): String =
        when (value) {
            is Boolean -> if (value) "1" else "0"
            else -> value.toString()
        }

    override fun readObject(
        rs: RowApi,
        index: Int,
    ): Any? = rs.getString(index)

    override fun nonNullValueToString(value: String): String = "'${value.replace("'", "''")}'"

    override fun parameterMarker(value: String?): String = "?::$castName"
}

internal fun Table.pgVarchar(name: String): Column<String> = registerColumn(name, PgStringColumnType("VARCHAR", "varchar"))

internal class PgNumericColumnType : ColumnType<BigDecimal>() {
    override fun sqlType(): String = "DECIMAL"

    override fun valueFromDB(value: Any): BigDecimal =
        when (value) {
            is BigDecimal -> value
            else -> BigDecimal(value.toString())
        }
}

internal fun Table.pgNumeric(name: String): Column<BigDecimal> = registerColumn(name, PgNumericColumnType())

internal fun Entity<*>.isWritten(column: Column<*>): Boolean = writeValues.keys.any { it == column }
