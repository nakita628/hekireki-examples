defmodule LangWatch.SystemMigrationTenantState do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          migration_name: String.t(),
          tenant_id: String.t(),
          status: String.t(),
          report: map() | nil,
          occurred_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SystemMigrationTenantState" do
    field(:migration_name, :string, primary_key: true, source: :migrationName)
    field(:tenant_id, :string, primary_key: true, source: :tenantId)
    field(:status, :string)
    field(:report, :map)
    field(:occurred_at, LangWatch.PrismaDateTime, autogenerate: true, source: :occurredAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end