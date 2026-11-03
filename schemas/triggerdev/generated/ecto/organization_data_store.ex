defmodule Trigger.OrganizationDataStore do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          organization_ids: [String.t()],
          kind: atom(),
          config: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "OrganizationDataStore" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:organization_ids, {:array, :string}, source: :organizationIds)
    field(:kind, Ecto.Enum, values: [:CLICKHOUSE])
    field(:config, :map)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end